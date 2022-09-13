﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;
using SystemProcess = System.Diagnostics.Process;

namespace RaspberryPi.Process
{
    public class ProcessRunner : IProcessRunner
    {
        private readonly ILogger<ProcessRunner> logger;

        public ProcessRunner(ILogger<ProcessRunner> logger)
        {
            this.logger = logger;
        }

        public CommandLineResult TryExecuteCommand(string commandLine, CancellationToken cancellationToken = default)
        {
            var commandLineInvocation = new CommandLineInvocation(commandLine);
            return this.TryExecuteCommand(commandLineInvocation);
        }

        public CommandLineResult TryExecuteCommand(CommandLineInvocation invocation, CancellationToken cancellationToken = default)
        {
            var cmdResult = this.ExecuteCommandInternal(invocation, cancellationToken);
            return cmdResult;
        }

        public CommandLineResult ExecuteCommand(string commandLine, CancellationToken cancellationToken = default)
        {
            var commandLineInvocation = new CommandLineInvocation(commandLine);
            return this.ExecuteCommand(commandLineInvocation);
        }

        public CommandLineResult ExecuteCommand(CommandLineInvocation invocation, CancellationToken cancellationToken = default)
        {
            var cmdResult = this.ExecuteCommandInternal(invocation, cancellationToken);
            cmdResult.EnsureSuccessExitCode();
            return cmdResult;
        }

        private CommandLineResult ExecuteCommandInternal(CommandLineInvocation invocation, CancellationToken cancellationToken = default)
        {
            var debugs = new List<string>();
            var infos = new StringBuilder();
            var errors = new StringBuilder();

            var exitCode = this.ExecuteCommand(
                invocation.Executable,
                invocation.Arguments,
                invocation.WorkingDirectory,
                x => infos.AppendLine(x),
                x => errors.AppendLine(x),
                cancellationToken
            );

            return new CommandLineResult(exitCode, infos.ToString(), errors.ToString());
        }

        private int ExecuteCommand(
            string executable,
            string arguments,
            string workingDirectory,
            Action<string> infoAction,
            Action<string> errorAction,
            CancellationToken cancellationToken = default)
        {
            if (executable == null)
            {
                throw new ArgumentNullException(nameof(executable));
            }

            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            if (workingDirectory == null)
            {
                throw new ArgumentNullException(nameof(workingDirectory));
            }

            if (infoAction == null)
            {
                throw new ArgumentNullException(nameof(infoAction));
            }

            if (errorAction == null)
            {
                throw new ArgumentNullException(nameof(errorAction));
            }

            void WriteData(Action<string> action, ManualResetEventSlim resetEvent, DataReceivedEventArgs e)
            {
                try
                {
                    if (e.Data == null)
                    {
                        resetEvent.Set();
                        return;
                    }

                    action(e.Data);
                }
                catch (Exception ex)
                {
                    try
                    {
                        errorAction($"Error occurred handling message: {ex}");
                    }
                    catch
                    {
                        // Ignore
                    }
                }
            }

            try
            {
                // We need to be careful to make sure the message is accurate otherwise people could wrongly assume the exe is in the working directory when it could be somewhere completely different!
                var executableDirectoryName = Path.GetDirectoryName(executable);
                this.logger.LogDebug($"Executable directory is {executableDirectoryName}");

                var exeInSamePathAsWorkingDirectory = string.Equals(executableDirectoryName?.TrimEnd('\\', '/'), workingDirectory.TrimEnd('\\', '/'), StringComparison.OrdinalIgnoreCase);
                var exeFileNameOrFullPath = exeInSamePathAsWorkingDirectory ? Path.GetFileName(executable) : executable;
                this.logger.LogDebug($"Executable name or full path: {exeFileNameOrFullPath}");

                this.logger.LogDebug($"Starting {exeFileNameOrFullPath} in working directory '{workingDirectory}'");

                using (var outputResetEvent = new ManualResetEventSlim(false))
                using (var errorResetEvent = new ManualResetEventSlim(false))
                using (var process = new SystemProcess())
                {
                    process.StartInfo.FileName = executable;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.WorkingDirectory = workingDirectory;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    //if (PlatformDetection.IsRunningOnWindows)
                    //{
                    //    process.StartInfo.StandardOutputEncoding = encoding;
                    //    process.StartInfo.StandardErrorEncoding = encoding;
                    //}

                    process.OutputDataReceived += (sender, e) =>
                    {
                        WriteData(infoAction, outputResetEvent, e);
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        WriteData(errorAction, errorResetEvent, e);
                    };

                    try
                    {
                        process.Start();
                    }
                    catch (Exception)
                    {
                        // TODO: Use CommandLineException
                        throw;
                    }

                    var running = true;

                    using (cancellationToken.Register(() =>
                    {
                        if (running)
                        {
                            this.KillProcess(process);
                        }
                    }))
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            this.KillProcess(process);
                        }

                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();

                        process.WaitForExit();

                        this.SafelyCancelRead(process.CancelErrorRead);
                        this.SafelyCancelRead(process.CancelOutputRead);

                        this.SafelyWaitForAllOutput(outputResetEvent, cancellationToken);
                        this.SafelyWaitForAllOutput(errorResetEvent, cancellationToken);

                        var exitCode = SafelyGetExitCode(process);
                        this.logger.LogDebug($"Process {exeFileNameOrFullPath} in {workingDirectory} exited with code {exitCode}");

                        running = false;
                        return exitCode;
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Use CommandLineException
                throw new Exception($"Error when attempting to execute {executable}: {ex.Message}", ex);
            }
        }

        private static int SafelyGetExitCode(SystemProcess process)
        {
            try
            {
                return process.ExitCode;
            }
            catch (InvalidOperationException ex)
                when (ex.Message is "No process is associated with this object." or
                        "Process was not started by this object, so requested information cannot be determined.")
            {
                return -1;
            }
        }

        private void SafelyWaitForAllOutput(ManualResetEventSlim outputResetEvent, CancellationToken cancel)
        {
            try
            {
                //5 seconds is a bit arbitrary, but the process should have already exited by now, so unwise to wait too long
                outputResetEvent.Wait(TimeSpan.FromSeconds(5), cancel);
            }
            catch (OperationCanceledException ex)
            {
                this.logger.LogError(ex, $"Swallowing {ex.GetType().Name} while waiting for last of the process output.");
            }
        }

        private void SafelyCancelRead(Action action)
        {
            try
            {
                action();
            }
            catch (InvalidOperationException ex)
            {
                this.logger.LogError(ex, $"Swallowing {ex.GetType().Name} calling {action.Method.Name}.");
            }
        }

        private void KillProcess(SystemProcess process)
        {
            try
            {
                process.Kill();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Failed to kill the launched process: {ex}");
            }
        }
    }
}
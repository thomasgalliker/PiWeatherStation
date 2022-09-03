using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Logging;
using RaspberryPi.Internals;

namespace RaspberryPi
{
    public class ProcessRunner : IProcessRunner
    {
        private readonly ILogger<ProcessRunner> logger;

        public ProcessRunner(ILogger<ProcessRunner> logger)
        {
            this.logger = logger;
        }

        public CmdResult ExecuteCommand(CommandLineInvocation invocation, CancellationToken cancellationToken = default)
        {
            return this.ExecuteCommand(invocation, Environment.CurrentDirectory, cancellationToken);
        }

        public CmdResult ExecuteCommand(CommandLineInvocation invocation, string workingDirectory, CancellationToken cancellationToken = default)
        {
            if (workingDirectory == null)
            {
                throw new ArgumentNullException(nameof(workingDirectory));
            }

            var arguments = $"{invocation.Arguments} {invocation.SystemArguments ?? string.Empty}";
            var debugs = new List<string>();
            var infos = new List<string>();
            var errors = new List<string>();

            var exitCode = this.ExecuteCommand(
                invocation.Executable,
                arguments,
                workingDirectory,
                debugs.Add,
                infos.Add,
                errors.Add,
                cancellationToken
            );

            return new CmdResult(exitCode, infos, errors);
        }

        public int ExecuteCommand(
            string executable,
            string arguments,
            string workingDirectory,
            Action<string> debug,
            Action<string> info,
            Action<string> error,
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

            if (debug == null)
            {
                throw new ArgumentNullException(nameof(debug));
            }

            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
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
                        error($"Error occurred handling message: {ex}");
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
                debug($"Executable directory is {executableDirectoryName}");

                var exeInSamePathAsWorkingDirectory = string.Equals(executableDirectoryName?.TrimEnd('\\', '/'), workingDirectory.TrimEnd('\\', '/'), StringComparison.OrdinalIgnoreCase);
                var exeFileNameOrFullPath = exeInSamePathAsWorkingDirectory ? Path.GetFileName(executable) : executable;
                debug($"Executable name or full path: {exeFileNameOrFullPath}");

                debug($"Starting {exeFileNameOrFullPath} in working directory '{workingDirectory}'");

                using (var outputResetEvent = new ManualResetEventSlim(false))
                using (var errorResetEvent = new ManualResetEventSlim(false))
                using (var process = new Process())
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
                        WriteData(info, outputResetEvent, e);
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        WriteData(error, errorResetEvent, e);
                    };

                    process.Start();

                    var running = true;

                    using (cancellationToken.Register(() =>
                    {
                        if (running)
                        {
                            KillProcess(process, error);
                        }
                    }))
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            KillProcess(process, error);
                        }

                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();

                        process.WaitForExit();

                        SafelyCancelRead(process.CancelErrorRead, debug);
                        SafelyCancelRead(process.CancelOutputRead, debug);

                        SafelyWaitForAllOutput(outputResetEvent, cancellationToken, debug);
                        SafelyWaitForAllOutput(errorResetEvent, cancellationToken, debug);

                        var exitCode = SafelyGetExitCode(process);
                        debug($"Process {exeFileNameOrFullPath} in {workingDirectory} exited with code {exitCode}");

                        running = false;
                        return exitCode;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when attempting to execute {executable}: {ex.Message}", ex);
            }
        }

        private static int SafelyGetExitCode(Process process)
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

        private static void SafelyWaitForAllOutput(ManualResetEventSlim outputResetEvent,
            CancellationToken cancel,
            Action<string> debug)
        {
            try
            {
                //5 seconds is a bit arbitrary, but the process should have already exited by now, so unwise to wait too long
                outputResetEvent.Wait(TimeSpan.FromSeconds(5), cancel);
            }
            catch (OperationCanceledException ex)
            {
                debug($"Swallowing {ex.GetType().Name} while waiting for last of the process output.");
            }
        }

        private static void SafelyCancelRead(Action action, Action<string> debug)
        {
            try
            {
                action();
            }
            catch (InvalidOperationException ex)
            {
                debug($"Swallowing {ex.GetType().Name} calling {action.Method.Name}.");
            }
        }

        private static void KillProcess(Process process, Action<string> error)
        {
            try
            {
                process.Kill();
            }
            catch (Exception ex)
            {
                error($"Failed to kill the launched process: {ex}");
            }
        }
    }
}
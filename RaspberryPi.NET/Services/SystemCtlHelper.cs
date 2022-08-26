using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using RaspberryPi.Internals;

namespace RaspberryPi.Services
{
    public class SystemCtlHelper : ISystemCtlHelper
    {
        private readonly ILogger logger;
        private readonly IProcessRunner processRunner;

        public SystemCtlHelper(
            ILogger<SystemCtlHelper> logger,
            IProcessRunner processRunner)
        {
            this.logger = logger;
            this.processRunner = processRunner;
        }

        public bool StartService(string serviceName)
        {
            return this.RunServiceCommand("start", serviceName);
        }

        public bool RestartService(string serviceName)
        {
            return this.RunServiceCommand("restart", serviceName);
        }

        public bool StopService(string serviceName)
        {
            return this.RunServiceCommand("stop", serviceName);
        }

        public bool EnableService(string serviceName)
        {
            return this.RunServiceCommand("enable", serviceName);
        }

        public bool DisableService(string serviceName)
        {
            return this.RunServiceCommand("disable", serviceName);
        }

        private bool RunServiceCommand(string command, string serviceName)
        {
            var systemctlCommand = $"systemctl {command} {serviceName}";
            this.logger.LogDebug($"RunServiceCommand: {systemctlCommand}");

            var commandLineInvocation = new CommandLineInvocation("/bin/bash", $"-c \"{systemctlCommand}\"");
            var result = this.processRunner.ExecuteCommand(commandLineInvocation);
            var success = result.ExitCode == 0;

            if (success)
            {
                this.logger.LogInformation(
                    $"RunServiceCommand: '{systemctlCommand}' finished successfully");
            }
            else
            {
                this.logger.LogInformation(
                    $"RunServiceCommand: '{systemctlCommand}' failed with exit code {result.ExitCode}{Environment.NewLine}" +
                    $"{string.Join(Environment.NewLine, result.Errors.Select(e => $"> Error: {e}"))}");
            }

            return success;
        }

        public bool ReloadDaemon()
        {
            var systemctlCommand = $"systemctl daemon-reload";
            this.logger.LogDebug($"ReloadDaemon: {systemctlCommand}");

            var commandLineInvocation = new CommandLineInvocation("sudo", systemctlCommand);
            var result = this.processRunner.ExecuteCommand(commandLineInvocation);
            var success = result.ExitCode == 0;

            if (success)
            {
                this.logger.LogInformation(
                    $"ReloadDaemon: '{systemctlCommand}' finished successfully");
            }
            else
            {
                this.logger.LogInformation(
                    $"ReloadDaemon: '{systemctlCommand}' failed with exit code {result.ExitCode}{Environment.NewLine}" +
                    $"{string.Join(Environment.NewLine, result.Errors.Select(e => $"> Error: {e}"))}");
            }

            return success;
        }
    }
}
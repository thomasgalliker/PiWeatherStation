using System;
using Microsoft.Extensions.Logging;
using RaspberryPi.Internals;
using RaspberryPi.Process;
using RaspberryPi.Storage;

namespace RaspberryPi.Services
{
    public class LinuxServiceConfigurator : IServiceConfigurator
    {
        private readonly ILogger logger;
        private readonly IFileSystem fileSystem;
        private readonly ISystemCtl systemCtl;
        private readonly IProcessRunner processRunner;

        public LinuxServiceConfigurator(
            ILogger<LinuxServiceConfigurator> logger,
            IFileSystem fileSystem,
            ISystemCtl systemCtl,
            IProcessRunner processRunner)
        {
            this.logger = logger;
            this.fileSystem = fileSystem;
            this.systemCtl = systemCtl;
            this.processRunner = processRunner;

            this.CheckSystemPrerequisites();
        }

        public void UninstallService(string serviceName)
        {
            try
            {
                this.logger.LogDebug($"Uninstalling systemd service \"{serviceName}\"...");
                var systemdUnitFilePath = GetServiceFilePath(serviceName);
                this.UninstallServiceInternal(serviceName, systemdUnitFilePath);
                this.logger.LogDebug($"Systemd service \"{serviceName}\" successfully uninstalled");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Failed to uninstall systemd service \"{serviceName}\"");
                throw;
            }
        }

        private void UninstallServiceInternal(string serviceName, string systemdUnitFilePath)
        {
            this.systemCtl.StopService(serviceName);
            this.systemCtl.DisableService(serviceName);
            this.fileSystem.File.Delete(systemdUnitFilePath);
        }

        public void InstallService(ServiceDefinition serviceDefinition)
        {
            try
            {
                this.logger.LogDebug($"Installing systemd service \"{serviceDefinition.ServiceName}\"...");
                var systemdUnitFilePath = GetServiceFilePath(serviceDefinition.ServiceName);
                this.InstallServiceInternal(systemdUnitFilePath, serviceDefinition);
                this.logger.LogDebug($"Systemd service \"{serviceDefinition.ServiceName}\" successfully installed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Failed to install systemd service \"{serviceDefinition.ServiceName}\"");
                throw;
            }
        }

        private void InstallServiceInternal(string systemdUnitFilePath, ServiceDefinition serviceDefinition)
        {
            var serviceFileContent = serviceDefinition.GetSystemdUnitFile();
            this.WriteUnitFile(systemdUnitFilePath, serviceFileContent);
            this.systemCtl.EnableService(serviceDefinition.ServiceName);
        }

        public void ReinstallService(ServiceDefinition serviceDefinition)
        {
            try
            {
                this.logger.LogDebug($"Reinstalling systemd service \"{serviceDefinition.ServiceName}\"...");
                var systemdUnitFilePath = GetServiceFilePath(serviceDefinition.ServiceName);
                this.UninstallServiceInternal(serviceDefinition.ServiceName, systemdUnitFilePath);
                this.InstallServiceInternal(systemdUnitFilePath, serviceDefinition);
                this.logger.LogDebug($"Systemd service \"{serviceDefinition.ServiceName}\" successfully reinstalled");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Failed to reinstall systemd service \"{serviceDefinition.ServiceName}\"");
                throw;
            }
        }

        private void WriteUnitFile(string path, string contents)
        {
            this.fileSystem.File.WriteAllText(path, contents);

            var commandLineInvocation = new CommandLineInvocation("/bin/bash", $"-c \"chmod 644 {path}\"");
            this.processRunner.ExecuteCommand(commandLineInvocation);
        }

        private void CheckSystemPrerequisites()
        {
            if (!this.fileSystem.File.Exists("/bin/bash"))
            {
                throw new Exception(
                    "Could not detect bash. Bash is required to run tentacle.");
            }

            if (!this.HaveSudoPrivileges())
            {
                throw new Exception(
                    "Requires elevated privileges. Please run command as sudo.");
            }

            if (!this.IsSystemdInstalled())
            {
                throw new Exception(
                    "Could not detect systemd. systemd is required to run Tentacle as a service");
            }
        }

        private bool IsSystemdInstalled()
        {
            var commandLineInvocation = new CommandLineInvocation("/bin/bash", "-c \"command -v systemctl >/dev/null\"");
            var result = this.processRunner.ExecuteCommand(commandLineInvocation);
            return result.ExitCode == 0;
        }

        private bool HaveSudoPrivileges()
        {
            var commandLineInvocation = new CommandLineInvocation("/bin/bash", "-c \"sudo -vn 2> /dev/null\"");
            var result = this.processRunner.ExecuteCommand(commandLineInvocation);
            return result.ExitCode == 0;
        }

        private static string GetServiceFilePath(string serviceName)
        {
            return $"/etc/systemd/system/{serviceName}.service";
        }
    }
}
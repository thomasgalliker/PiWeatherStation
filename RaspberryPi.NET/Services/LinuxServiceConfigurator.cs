using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using RaspberryPi.Internals;
using RaspberryPi.Storage;

namespace RaspberryPi.Services
{
    public class LinuxServiceConfigurator : IServiceConfigurator
    {
        private readonly ILogger logger;
        private readonly IFileSystem fileSystem;
        private readonly ISystemCtlHelper systemCtlHelper;
        private readonly IProcessRunner processRunner;

        public LinuxServiceConfigurator(
            ILogger<LinuxServiceConfigurator> logger,
            IFileSystem fileSystem,
            ISystemCtlHelper systemCtlHelper,
            IProcessRunner processRunner)
        {
            this.logger = logger;
            this.fileSystem = fileSystem;
            this.systemCtlHelper = systemCtlHelper;
            this.processRunner = processRunner;
        }

        public void ConfigureServiceByInstanceName(
            string serviceName,
            string execStart,
            string serviceDescription,
            ServiceConfigurationState serviceConfigurationState)
        {
            this.ConfigureService(
                serviceName,
                execStart,
                serviceDescription,
                serviceConfigurationState);
        }

        public void ConfigureServiceByConfigPath(
            string serviceName,
            string execStart,
            string serviceDescription,
            ServiceConfigurationState serviceConfigurationState)
        {
            this.ConfigureService(
                serviceName,
                execStart,
                serviceDescription,
                serviceConfigurationState);
        }

        private void ConfigureService(string serviceName, string execStart, string serviceDescription, ServiceConfigurationState serviceConfigurationState)
        {
            // Check if system has bash and systemd
            this.CheckSystemPrerequisites();

            serviceName = SanitizeString(serviceName);

            if (serviceConfigurationState.Uninstall)
            {
                this.UninstallService(serviceName);
            }

            var serviceDependencies = serviceConfigurationState.ServiceDependencies;

            var userName = serviceConfigurationState.Username ?? "root";
            if (serviceConfigurationState.Install)
            {
                this.InstallService(
                    serviceName,
                    execStart,
                    serviceDescription,
                    userName,
                    serviceDependencies);
            }

            if (serviceConfigurationState.Reconfigure)
            {
                this.ReconfigureService(
                    serviceName,
                    execStart,
                    serviceDescription,
                    userName,
                    serviceDependencies);
            }
        }

        private static string GetServiceFilePath(string cleanedInstanceName)
        {
            return $"/etc/systemd/system/{cleanedInstanceName}.service";
        }

        public void UninstallService(string serviceName)
        {
            serviceName = SanitizeString(serviceName);
            this.logger.LogDebug($"Removing systemd service: {serviceName}");

            try
            {
                var systemdUnitFilePath = GetServiceFilePath(serviceName);

                this.systemCtlHelper.StopService(serviceName);
                this.systemCtlHelper.DisableService(serviceName);
                this.fileSystem.Delete(systemdUnitFilePath);
                this.logger.LogDebug("Service uninstalled");
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"Could not remove the systemd service: {serviceName}");
                throw;
            }
        }

        public void InstallService(
            string serviceName,
            string execStart,
            string serviceDescription,
            string userName,
            IEnumerable<string> serviceDependencies)
        {
            serviceName = SanitizeString(serviceName);
            this.logger.LogDebug($"Installing systemd service: {serviceName}");

            try
            {
                var systemdUnitFilePath = GetServiceFilePath(serviceName);

                var serviceFileContent = GenerateSystemdUnitFile(serviceDescription, execStart, userName, serviceDependencies);
                this.WriteUnitFile(systemdUnitFilePath, serviceFileContent);
                this.systemCtlHelper.EnableService(serviceName);
                this.logger.LogDebug($"Service installed: {serviceName}");
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"Could not install the systemd service: {serviceName}");
                throw;
            }
        }

        public void ReconfigureService(
            string serviceName,
            string execStart,
            string serviceDescription,
            string userName,
            IEnumerable<string> serviceDependencies)
        {
            serviceName = SanitizeString(serviceName);
            this.logger.LogDebug($"Reconfiguring systemd service: {serviceName}");

            try
            {
                var systemdUnitFilePath = GetServiceFilePath(serviceName);

                this.logger.LogDebug($"Attempting to remove old service: {serviceName}");

                //remove service
                this.systemCtlHelper.StopService(serviceName);
                this.systemCtlHelper.DisableService(serviceName);
                this.fileSystem.Delete(systemdUnitFilePath);

                //re-add service
                var serviceFileContent = GenerateSystemdUnitFile(serviceDescription, execStart, userName, serviceDependencies);
                this.WriteUnitFile(systemdUnitFilePath, serviceFileContent);
                this.systemCtlHelper.EnableService(serviceName);
                this.logger.LogDebug($"Service installed: {serviceName}");
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"Could not reconfigure the systemd service: {serviceName}");
                throw;
            }
        }

        private void WriteUnitFile(string path, string contents)
        {
            this.fileSystem.WriteAllText(path, contents);

            var commandLineInvocation = new CommandLineInvocation("/bin/bash", $"-c \"chmod 644 {path}\"");
            var result = this.processRunner.ExecuteCommand(commandLineInvocation);
            if (result.ExitCode != 0)
            {
                throw new CommandLineException(result.ExitCode, result.Errors);
            }
        }

        private void CheckSystemPrerequisites()
        {
            if (!this.fileSystem.Exists("/bin/bash"))
            {
                throw new Exception(
                    "Could not detect bash. bash is required to run tentacle.");
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

        private static string GenerateSystemdUnitFile(
            string serviceDescription,
            string execStart,
            string userName,
            IEnumerable<string> serviceDependencies)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("[Unit]");
            stringBuilder.AppendLine($"Description={serviceDescription}");
            stringBuilder.AppendLine($"After={string.Join(" ", serviceDependencies)}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("[Service]");
            stringBuilder.AppendLine("Type=simple");
            stringBuilder.AppendLine($"User={userName}");
            stringBuilder.AppendLine($"ExecStart={execStart}");
            stringBuilder.AppendLine(" --noninteractive");
            stringBuilder.AppendLine("Restart=always");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("[Install]");
            stringBuilder.AppendLine("WantedBy=multi-user.target");

            return stringBuilder.ToString();
        }

        private static string SanitizeString(string str)
        {
            return Regex.Replace(str.Replace("/", ""), @"\s+", "-");
        }
    }
}
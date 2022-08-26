using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace RaspberryPi
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
            string exePath,
            string instance,
            string serviceDescription,
            ServiceConfigurationState serviceConfigurationState)
        {
            this.ConfigureService(
                serviceName,
                exePath,
                instance,
                null,
                serviceDescription,
                serviceConfigurationState);
        }

        public void ConfigureServiceByConfigPath(
            string serviceName,
            string exePath,
            string configPath,
            string serviceDescription,
            ServiceConfigurationState serviceConfigurationState)
        {
            this.ConfigureService(
                serviceName,
                exePath,
                null,
                configPath,
                serviceDescription,
                serviceConfigurationState);
        }

        private void ConfigureService(string thisServiceName, string exePath, string instance, string configPath, string serviceDescription, ServiceConfigurationState serviceConfigurationState)
        {
            //Check if system has bash and systemd
            this.CheckSystemPrerequisites();

            var cleanedInstanceName = SanitizeString(instance ?? thisServiceName);
            var systemdUnitFilePath = $"/etc/systemd/system/{cleanedInstanceName}.service";

            if (serviceConfigurationState.Restart)
            {
                this.RestartService(cleanedInstanceName);
            }

            if (serviceConfigurationState.Stop)
            {
                this.StopService(cleanedInstanceName);
            }

            if (serviceConfigurationState.Uninstall)
            {
                this.UninstallService(cleanedInstanceName, systemdUnitFilePath);
            }

            var serviceDependencies = serviceConfigurationState.ServiceDependencies;

            var userName = serviceConfigurationState.Username ?? "root";
            if (serviceConfigurationState.Install)
            {
                this.InstallService(cleanedInstanceName,
                    instance,
                    configPath,
                    exePath,
                    serviceDescription,
                    systemdUnitFilePath,
                    userName,
                    serviceDependencies);
            }

            if (serviceConfigurationState.Reconfigure)
            {
                this.ReconfigureService(cleanedInstanceName,
                    instance,
                    configPath,
                    exePath,
                    serviceDescription,
                    systemdUnitFilePath,
                    userName,
                    serviceDependencies);
            }

            if (serviceConfigurationState.Start)
            {
                this.StartService(cleanedInstanceName);
            }
        }

        private void RestartService(string serviceName)
        {
            this.logger.LogDebug($"Restarting service: {serviceName}");
            var success = this.systemCtlHelper.RestartService(serviceName);
            if (success)
            {
                this.logger.LogDebug($"Service {serviceName} has been restarted");
            }
            else
            {
                this.logger.LogError($"The service  {serviceName} could not be restarted");
            }
        }

        private void StopService(string serviceName)
        {
            this.logger.LogDebug($"Stopping service: {serviceName}");
            if (this.systemCtlHelper.StopService(serviceName))
            {
                this.logger.LogDebug("Service stopped");
            }
            else
            {
                this.logger.LogError("The service could not be stopped");
            }
        }

        private void StartService(string serviceName)
        {
            if (this.systemCtlHelper.StartService(serviceName))
            {
                this.logger.LogDebug($"Service started: {serviceName}");
            }
            else
            {
                this.logger.LogError($"Could not start the systemd service: {serviceName}");
            }
        }

        private void UninstallService(string instance, string systemdUnitFilePath)
        {
            this.logger.LogDebug($"Removing systemd service: {instance}");
            try
            {
                this.systemCtlHelper.StopService(instance);
                this.systemCtlHelper.DisableService(instance);
                this.fileSystem.Delete(systemdUnitFilePath);
                this.logger.LogDebug("Service uninstalled");
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"Could not remove the systemd service: {instance}");
                throw;
            }
        }

        private void InstallService(string serviceName,
            string instance,
            string configPath,
            string exePath,
            string serviceDescription,
            string systemdUnitFilePath,
            string userName,
            IEnumerable<string> serviceDependencies)
        {
            try
            {
                this.WriteUnitFile(systemdUnitFilePath, GenerateSystemdUnitFile(instance, configPath, serviceDescription, exePath, userName, serviceDependencies));
                this.systemCtlHelper.EnableService(serviceName);
                this.logger.LogDebug($"Service installed: {serviceName}");
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"Could not install the systemd service: {serviceName}");
                throw;
            }
        }

        private void ReconfigureService(string serviceName,
            string instance,
            string configPath,
            string exePath,
            string serviceDescription,
            string systemdUnitFilePath,
            string userName,
            IEnumerable<string> serviceDependencies)
        {
            try
            {
                this.logger.LogDebug($"Attempting to remove old service: {serviceName}");
                //remove service
                this.systemCtlHelper.StopService(serviceName);
                this.systemCtlHelper.DisableService(serviceName);
                this.fileSystem.Delete(systemdUnitFilePath);

                //re-add service
                this.WriteUnitFile(systemdUnitFilePath, GenerateSystemdUnitFile(instance, configPath, serviceDescription, exePath, userName, serviceDependencies));
                this.systemCtlHelper.EnableService(serviceName);
                this.logger.LogDebug($"Service installed: {serviceName}");
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"Could not reconfigure the systemd service: {instance}");
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
            string instance,
            string configPath,
            string serviceDescription, string exePath, string userName, IEnumerable<string> serviceDependencies)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("[Unit]");
            stringBuilder.AppendLine($"Description={serviceDescription}");
            stringBuilder.AppendLine($"After={string.Join(" ", serviceDependencies)}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("[Service]");
            stringBuilder.AppendLine("Type=simple");
            stringBuilder.AppendLine($"User={userName}");
            stringBuilder.Append($"ExecStart={exePath} run");
            _ = !string.IsNullOrEmpty(instance)
                ? stringBuilder.Append($" --instance={instance}")
                : !string.IsNullOrEmpty(configPath)
                    ? stringBuilder.Append($" --config={configPath}")
                    : throw new Exception("Either the instance name of configuration path must be provided to configure a service");
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
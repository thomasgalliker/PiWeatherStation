using System.Collections.Generic;

namespace RaspberryPi.Services
{
    public interface IServiceConfigurator
    {
        void ConfigureServiceByConfigPath(string serviceName, string exePath, string serviceDescription, ServiceConfigurationState serviceConfigurationState);

        void ConfigureServiceByInstanceName(string serviceName, string exePath, string serviceDescription, ServiceConfigurationState serviceConfigurationState);
        void InstallService(string serviceName, string execStart, string serviceDescription, string userName, IEnumerable<string> serviceDependencies);
        void ReconfigureService(string serviceName, string execStart, string serviceDescription, string userName, IEnumerable<string> serviceDependencies);
        void UninstallService(string serviceName);
    }
}
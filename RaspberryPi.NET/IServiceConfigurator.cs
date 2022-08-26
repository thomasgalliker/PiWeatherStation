namespace RaspberryPi
{
    public interface IServiceConfigurator
    {
        void ConfigureServiceByConfigPath(string serviceName, string exePath, string configPath, string serviceDescription, ServiceConfigurationState serviceConfigurationState);

        void ConfigureServiceByInstanceName(string serviceName, string exePath, string instance, string serviceDescription, ServiceConfigurationState serviceConfigurationState);
    }
}
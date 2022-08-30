namespace RaspberryPi.Services
{
    public interface IServiceConfigurator
    {
        void InstallService(ServiceDefinition serviceDefinition);

        void ReinstallService(ServiceDefinition serviceDefinition);

        void UninstallService(string serviceName);
    }
}
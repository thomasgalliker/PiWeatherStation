namespace RaspberryPi.Services
{
    public interface ISystemCtl
    {
        bool IsEnabled(string serviceName);

        bool IsActive(string serviceName);

        bool DisableService(string serviceName);

        bool EnableService(string serviceName);

        bool RestartService(string serviceName);

        bool StartService(string serviceName);

        bool StopService(string serviceName);

        bool ReloadDaemon();
    }
}
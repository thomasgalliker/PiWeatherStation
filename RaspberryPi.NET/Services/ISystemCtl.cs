namespace RaspberryPi.Services
{
    public interface ISystemCtl
    {
        bool DisableService(string serviceName);

        bool EnableService(string serviceName);

        bool RestartService(string serviceName);

        bool StartService(string serviceName);

        bool StopService(string serviceName);

        bool ReloadDaemon();
    }
}
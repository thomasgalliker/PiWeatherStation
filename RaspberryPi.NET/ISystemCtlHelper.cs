namespace RaspberryPi
{
    public interface ISystemCtlHelper
    {
        bool DisableService(string serviceName);

        bool EnableService(string serviceName);

        bool RestartService(string serviceName);

        bool StartService(string serviceName);

        bool StopService(string serviceName);
        
        bool ReloadDaemon();
    }
}
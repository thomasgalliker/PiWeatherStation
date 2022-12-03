using RaspberryPi.Services;

namespace WeatherDisplay.Api.Services
{
    public class WeatherDisplayServiceConfigurator : IWeatherDisplayServiceConfigurator
    {
        private static readonly ServiceDefinition ServiceDefinition = new ServiceDefinition("weatherdisplay.api")
        {
            Description = "WeatherDisplay.Api",
            Type = ServiceType.Notify,
            WorkingDirectory = "/home/pi/WeatherDisplay.Api",
            ExecStart = "/home/pi/WeatherDisplay.Api/WeatherDisplay.Api",
            ExecStop = "/bin/kill ${MAINPID}",
            KillSignal = "SIGTERM",
            KillMode = KillMode.Process,
            Restart = ServiceRestart.No,
            UserName = "pi",
            GroupName = "pi",
            AfterServices = new[]
            {
                "network-online.target",
                "firewalld.service"
            },
            Wants = new[]
            {
                "network-online.target"
            },
            Environments = new[]
            {
                "ASPNETCORE_ENVIRONMENT=Production",
                "DOTNET_PRINT_TELEMETRY_MESSAGE=false",
                "DOTNET_ROOT=/home/pi/.dotnet"
            },
            WantedBy = new[]
            {
                "multi-user.target"
            }
        };

        private readonly ISystemCtl systemCtl;
        private readonly IServiceConfigurator serviceConfigurator;

        public WeatherDisplayServiceConfigurator(
            ISystemCtl systemCtl,
            IServiceConfigurator serviceConfigurator)
        {
            this.systemCtl = systemCtl;
            this.serviceConfigurator = serviceConfigurator;
        }

        public void RestartService()
        {
            this.systemCtl.RestartService(ServiceDefinition.ServiceName);
        }

        public void StartService()
        {
            this.systemCtl.StartService(ServiceDefinition.ServiceName);
        }

        public void StopService()
        {
            this.systemCtl.StopService(ServiceDefinition.ServiceName);
        }

        public void InstallService()
        {
            this.serviceConfigurator.InstallService(ServiceDefinition);
        }

        public void ReinstallService()
        {
            this.serviceConfigurator.ReinstallService(ServiceDefinition);
        }

        public void UninstallService()
        {
            this.serviceConfigurator.UninstallService(ServiceDefinition.ServiceName);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Services;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/system/service")]
    public class ServiceController : ControllerBase
    {
        private static readonly ServiceDefinition ServiceDefinition = new ServiceDefinition("weatherdisplay.api")
        {
            ServiceDescription = "WeatherDisplay.Api Service",
            ServiceType = ServiceType.Notify,
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
            WantsServices = new[]
            {
                "network-online.target"
            },
            Environments = new[]
            {
                "ASPNETCORE_ENVIRONMENT=Production",
                "DOTNET_PRINT_TELEMETRY_MESSAGE=false",
                "DOTNET_ROOT=/home/pi/.dotnet"
            }
        };

        private readonly ISystemCtl systemCtl;
        private readonly IServiceConfigurator serviceConfigurator;

        public ServiceController(
            ISystemCtl systemCtl,
            IServiceConfigurator serviceConfigurator)
        {
            this.systemCtl = systemCtl;
            this.serviceConfigurator = serviceConfigurator;
        }

        [HttpGet("start")]
        public void Start()
        {
            this.systemCtl.StartService(ServiceDefinition.ServiceName);
        }

        [HttpGet("stop")]
        public void Stop()
        {
            this.systemCtl.StopService(ServiceDefinition.ServiceName);
        }

        [HttpGet("restart")]
        public void Restart()
        {
            this.systemCtl.RestartService(ServiceDefinition.ServiceName);
        }

        [HttpGet("install")]
        public void Install()
        {
            this.serviceConfigurator.InstallService(ServiceDefinition);
        }

        [HttpGet("reinstall")]
        public void Reistall()
        {
            this.serviceConfigurator.ReinstallService(ServiceDefinition);
        }

        [HttpGet("uninstall")]
        public void Uninstall()
        {
            this.serviceConfigurator.UninstallService(ServiceDefinition.ServiceName);
        }
    }
}
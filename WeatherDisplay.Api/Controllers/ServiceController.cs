using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Services;
using WeatherDisplay.Api.Updater.Services;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/system/service")]
    public class ServiceController : ControllerBase
    {
        private const string ServiceName = "weatherdisplay.api";
        private const string ExecStart = "/home/pi/WeatherDisplay.Api/WeatherDisplay.Api";
        private const string Username = "pi";
        private static readonly string[] ServiceDependencies = new string[]
        {
            "network-online.target",
            "firewalld.service"
        };

        private readonly ILogger logger;
        private readonly ISystemCtl systemCtl;
        private readonly IServiceConfigurator serviceConfigurator;
        private readonly IAutoUpdateService autoUpdateService;

        public ServiceController(
            ILogger<ServiceController> logger,
            ISystemCtl systemCtl,
            IServiceConfigurator serviceConfigurator,
            IAutoUpdateService autoUpdateService)
        {
            this.logger = logger;
            this.systemCtl = systemCtl;
            this.serviceConfigurator = serviceConfigurator;
            this.autoUpdateService = autoUpdateService;
        }

        [HttpGet("start")]
        public void Start()
        {
            this.systemCtl.StartService(ServiceName);
        }
        
        [HttpGet("stop")]
        public void Stop()
        {
            this.systemCtl.StopService(ServiceName);
        }
        
        [HttpGet("restart")]
        public void Restart()
        {
            this.systemCtl.RestartService(ServiceName);
        }

        [HttpGet("install")]
        public void Install()
        {
            this.serviceConfigurator.InstallService(
                ServiceName,
                ExecStart,
                null,
                Username,
                ServiceDependencies);
        }

        [HttpGet("reinstall")]
        public void Reistall()
        {
            this.serviceConfigurator.ReinstallService(
                ServiceName,
                ExecStart,
                null,
                Username,
                ServiceDependencies);
        }

        [HttpGet("uninstall")]
        public void Uninstall()
        {
            this.serviceConfigurator.UninstallService(ServiceName);
        }
    }
}
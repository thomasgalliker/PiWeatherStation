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
        private static readonly string[] ServiceDependencies = new string[]
        { 
            "network-online.target",
            "firewalld.service" 
        };

        private readonly ILogger logger;
        private readonly ISystemCtlHelper systemCtlHelper;
        private readonly IServiceConfigurator serviceConfigurator;
        private readonly IAutoUpdateService autoUpdateService;

        public ServiceController(
            ILogger<ServiceController> logger,
            ISystemCtlHelper systemCtlHelper,
            IServiceConfigurator serviceConfigurator,
            IAutoUpdateService autoUpdateService)
        {
            this.logger = logger;
            this.systemCtlHelper = systemCtlHelper;
            this.serviceConfigurator = serviceConfigurator;
            this.autoUpdateService = autoUpdateService;
        }

        [HttpGet("restart")]
        public void Restart()
        {
            this.systemCtlHelper.RestartService(ServiceName);
        }

        [HttpGet("install")]
        public void Install()
        {
            this.serviceConfigurator.InstallService(ServiceName, "dotnet", "service description", "pi", ServiceDependencies);
        }
    }
}
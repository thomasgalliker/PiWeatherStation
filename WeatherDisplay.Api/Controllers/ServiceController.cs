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
            var serviceConfigurationState = new ServiceConfigurationState
            {
                ServiceDependencies = new List<string> { "network-online.target", "firewalld.service" },
                Install = true,
            };

            this.serviceConfigurator.ConfigureServiceByInstanceName(ServiceName, "dotnet", "service description", serviceConfigurationState);
        }
    }
}
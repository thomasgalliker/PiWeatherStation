using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Network;
using RaspberryPi.Services;
using WeatherDisplay.Api.Services;
using WeatherDisplay.Api.Updater.Services;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/system")]
    public class SystemController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IAccessPoint systemService;
        private readonly IServiceConfigurator serviceConfigurator;
        private readonly IAutoUpdateService autoUpdateService;

        public SystemController(
            ILogger<SystemController> logger,
            IAccessPoint systemService,
            IServiceConfigurator serviceConfigurator,
            IAutoUpdateService autoUpdateService)
        {
            this.logger = logger;
            this.systemService = systemService;
            this.serviceConfigurator = serviceConfigurator;
            this.autoUpdateService = autoUpdateService;
        }

        [HttpGet("update")]
        public async Task CheckForUpdateAsync(bool force = false)
        {
            var result = await this.autoUpdateService.CheckForUpdateAsync(force);
            this.logger.LogDebug(
                $"CheckForUpdateAsync returned{Environment.NewLine}" +
                $"{ObjectDumper.Dump(result)}");

            if (result.HasUpdate)
            {
                var updateRequest = UpdateRequestFactory.Create(result.UpdateVersion, result.UpdateVersionSource);
                this.autoUpdateService.StartUpdate(updateRequest);
            }
        }
    }
}
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RaspberryPi;
using RaspberryPi.Internals;
using RaspberryPi.Network;
using RaspberryPi.Process;
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
        private readonly ISystemInfoService systemInfoService;
        private readonly IAccessPoint systemService;
        private readonly IProcessRunner processRunner;
        private readonly IServiceConfigurator serviceConfigurator;
        private readonly IAutoUpdateService autoUpdateService;

        public SystemController(
            ILogger<SystemController> logger,
            ISystemInfoService systemInfoService,
            IAccessPoint systemService,
            IProcessRunner processRunner,
            IServiceConfigurator serviceConfigurator,
            IAutoUpdateService autoUpdateService)
        {
            this.logger = logger;
            this.systemInfoService = systemInfoService;
            this.systemService = systemService;
            this.processRunner = processRunner;
            this.serviceConfigurator = serviceConfigurator;
            this.autoUpdateService = autoUpdateService;
        }

        [HttpGet("info")]
        public async Task<CPUInfo> GetSystemInfoAsync()
        {
            var cpuInfo = await this.systemInfoService.GetCPUInfoAsync();
            return cpuInfo;
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

    public class SystemInfoDto
    {
        public string Test { get; internal set; }
    }
}
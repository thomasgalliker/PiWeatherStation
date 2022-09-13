using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IAccessPoint systemService;
        private readonly IProcessRunner processRunner;
        private readonly IServiceConfigurator serviceConfigurator;
        private readonly IAutoUpdateService autoUpdateService;

        public SystemController(
            ILogger<SystemController> logger,
            IAccessPoint systemService,
            IProcessRunner processRunner,
            IServiceConfigurator serviceConfigurator,
            IAutoUpdateService autoUpdateService)
        {
            this.logger = logger;
            this.systemService = systemService;
            this.processRunner = processRunner;
            this.serviceConfigurator = serviceConfigurator;
            this.autoUpdateService = autoUpdateService;
        }

        [HttpGet("info")]
        public async Task<SystemInfoDto> GetSystemInfoAsync()
        {
            var commandLineInvocation = new CommandLineInvocation("cat", $"/proc/cpuinfo");
            var result = this.processRunner.ExecuteCommand(commandLineInvocation);

            //Hardware        : BCM2835
            //Revision        : 902120
            //Serial          : 0000000053a77f3d
            //Model           : Raspberry Pi Zero 2 W Rev 1.0

            return new SystemInfoDto
            {
                Test = result.OutputData
            };
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
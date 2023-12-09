using Microsoft.AspNetCore.Mvc;
using RaspberryPi;
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
        private readonly IProcessRunner processRunner;
        private readonly IAutoUpdateService autoUpdateService;
        private readonly IShutdownService shutdownService;

        public SystemController(
            ILogger<SystemController> logger,
            ISystemInfoService systemInfoService,
            IProcessRunner processRunner,
            IAutoUpdateService autoUpdateService,
            IShutdownService shutdownService)
        {
            this.logger = logger;
            this.systemInfoService = systemInfoService;
            this.processRunner = processRunner;
            this.autoUpdateService = autoUpdateService;
            this.shutdownService = shutdownService;
        }

        [HttpGet("sudo")]
        public bool HaveSudoPrivileges()
        {
            var haveSudoPrivileges = this.processRunner.HaveSudoPrivileges();
            return haveSudoPrivileges;
        }
        
        [HttpGet("cpuinfo")]
        public async Task<CpuInfo> GetCpuInfoAsync()
        {
            var cpuInfo = await this.systemInfoService.GetCpuInfoAsync();
            return cpuInfo;
        }

        [HttpGet("cpusensors")]
        public CpuSensorsStatus GetCpuSensorsStatus()
        {
            var cpuSensorsStatus = this.systemInfoService.GetCpuSensorsStatus();
            return cpuSensorsStatus;
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

        [HttpGet("shutdown")]
        public void Shutdown()
        {
            this.shutdownService.Shutdown();
        }
        
        [HttpGet("reboot")]
        public void Reboot()
        {
            this.shutdownService.Reboot();
        }
    }
}
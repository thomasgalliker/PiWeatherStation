using DisplayService.Services;
using NCrontab;
using NCrontab.Scheduler;
using NLog;
using WeatherDisplay.Api.Updater.Services;
using WeatherDisplay.Compilations;
using WeatherDisplay.Model;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WeatherDisplay.Api.Services
{
    public class AutoStartupBackgroundService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IAppSettings appSettings;
        private readonly IAutoUpdateService autoUpdateService;
        private readonly IDisplayCompilationService displayCompilationService;
        private readonly IWeatherDisplayHardwareCoordinator weatherDisplayHardwareCoordinator;
        private readonly IScheduler scheduler;
        private readonly IDisplayManager displayManager;

        public AutoStartupBackgroundService(
            ILogger<AutoStartupBackgroundService> logger,
            IAppSettings appSettings,
            IAutoUpdateService autoUpdateService,
            IDisplayCompilationService displayCompilationService,
            IWeatherDisplayHardwareCoordinator weatherDisplayHardwareCoordinator,
            IScheduler scheduler,
            IDisplayManager displayManager)
        {
            this.logger = logger;
            this.appSettings = appSettings;
            this.autoUpdateService = autoUpdateService;
            this.displayCompilationService = displayCompilationService;
            this.weatherDisplayHardwareCoordinator = weatherDisplayHardwareCoordinator;
            this.scheduler = scheduler;
            this.displayManager = displayManager;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StartAsync");

            try
            {
                var runSetup = this.appSettings.RunSetup;
                if (runSetup)
                {
                    await this.displayCompilationService.SelectDisplayCompilationAsync("SetupDisplayCompilation");
                }
                else
                {
                    var result = await this.CheckAndStartUpdate();
                    if (!result.HasUpdate)
                    {
                        // Schedule automatic update check for "Daily, 4:50 at night"
                        //this.scheduler.AddTask(CrontabSchedule.Parse("50 4 * * *"), async c => { await this.CheckAndStartUpdate(); });
                        // Schedule automatic update check every hour at minute 50
                        this.scheduler.AddTask(CrontabSchedule.Parse("50 * * * *"), async c => { await this.CheckAndStartUpdate(); });

                        // Add rendering actions + start display manager
                        await this.weatherDisplayHardwareCoordinator.HandleButtonPress(1);
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "StartAsync failed with exception");
                await this.displayCompilationService.SelectDisplayCompilationAsync("ErrorDisplayCompilation");
            }
        }

        private async Task<UpdateCheckResult> CheckAndStartUpdate()
        {
            var result = await this.autoUpdateService.CheckForUpdateAsync();
            if (result.HasUpdate)
            {
                var updateRequest = UpdateRequestFactory.Create(result.UpdateVersion, result.UpdateVersionSource);
                this.autoUpdateService.StartUpdate(updateRequest);
            }

            return result;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StopAsync");

            await this.displayManager.ResetAsync();

            LogManager.Shutdown();
        }
    }
}
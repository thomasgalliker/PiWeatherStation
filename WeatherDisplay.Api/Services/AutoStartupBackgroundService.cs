using DisplayService.Services;
using NCrontab;
using NCrontab.Scheduler;
using NLog;
using WeatherDisplay.Api.Updater.Services;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model;
using WeatherDisplay.Pages.SystemInfo;
using WeatherDisplay.Services.Hardware;
using WeatherDisplay.Services.Navigation;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WeatherDisplay.Api.Services
{
    public class AutoStartupBackgroundService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IAppSettings appSettings;
        private readonly IAutoUpdateService autoUpdateService;
        private readonly INavigationService navigationService;
        private readonly IButtonsAccessService buttonsAccessService;
        private readonly ISensorAccessService sensorAccessService;
        private readonly IScheduler scheduler;
        private readonly IDisplayManager displayManager;

        public AutoStartupBackgroundService(
            ILogger<AutoStartupBackgroundService> logger,
            IAppSettings appSettings,
            IAutoUpdateService autoUpdateService,
            INavigationService navigationService,
            IButtonsAccessService buttonsAccessService,
            ISensorAccessService sensorAccessService,
            IScheduler scheduler,
            IDisplayManager displayManager)
        {
            this.logger = logger;
            this.appSettings = appSettings;
            this.autoUpdateService = autoUpdateService;
            this.navigationService = navigationService;
            this.buttonsAccessService = buttonsAccessService;
            this.sensorAccessService = sensorAccessService;
            this.scheduler = scheduler;
            this.displayManager = displayManager;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StartAsync");

            try
            {
                this.buttonsAccessService.Initialize();
                this.sensorAccessService.Initialize();

                var runSetup = this.appSettings.RunSetup;
                if (runSetup)
                {
                    await this.navigationService.NavigateAsync(App.Pages.SetupPage);
                }
                else
                {
                    var updateInProgress = await this.TryInstallUpdateAsync();
                    if (!updateInProgress)
                    {
                        // Schedule automatic update check for "Daily, 4:50 at night"
                        // this.scheduler.AddTask(CrontabSchedule.Parse("50 4 * * *"), async c => { await this.CheckAndStartUpdate(); });
                        // Schedule automatic update check every hour at minute 50
                        this.scheduler.AddTask(CrontabSchedule.Parse("50 * * * *"), async c => { await this.TryInstallUpdateAsync(); });

                        var defaultButton = this.appSettings.ButtonMappings.GetDefaultButtonMapping();
                        await this.navigationService.NavigateAsync(defaultButton.Page);
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "StartAsync failed with exception");

                var navigationParameters = new ErrorPage.NavigationParameters { Exception = ex };
                await this.navigationService.NavigateAsync(App.Pages.ErrorPage, navigationParameters);
            }
        }

        private async Task<bool> TryInstallUpdateAsync()
        {
            try
            {
                var result = await this.autoUpdateService.CheckForUpdateAsync();
                if (result.HasUpdate)
                {
                    var updateRequest = UpdateRequestFactory.Create(result.UpdateVersion, result.UpdateVersionSource);
                    this.autoUpdateService.StartUpdate(updateRequest);
                }

                return result.HasUpdate;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "TryInstallUpdateAsync failed with exception");
                return false;
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StopAsync");

            await this.displayManager.ResetAsync();

            LogManager.Shutdown();
        }
    }
}
using DisplayService.Services;
using Microsoft.Extensions.Options;
using NCrontab;
using NCrontab.Scheduler;
using NLog;
using WeatherDisplay.Api.Services.Configuration;
using WeatherDisplay.Api.Updater.Services;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Pages.SystemInfo;
using WeatherDisplay.Services.Hardware;
using WeatherDisplay.Services.Navigation;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WeatherDisplay.Api.Services
{
    public class AutoStartupBackgroundService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IOptionsMonitor<AppSettings> appSettings;
        private readonly IWritableOptions<AppSettings> writableAppSettings;
        private readonly IAutoUpdateService autoUpdateService;
        private readonly INavigationService navigationService;
        private readonly IButtonsAccessService buttonsAccessService;
        private readonly ISensorAccessService sensorAccessService;
        private readonly IScheduler scheduler;
        private readonly IDisplayManager displayManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AutoStartupBackgroundService(
            ILogger<AutoStartupBackgroundService> logger,
            IOptionsMonitor<AppSettings> appSettings,
            IWritableOptions<AppSettings> writableAppSettings,
            IAutoUpdateService autoUpdateService,
            INavigationService navigationService,
            IButtonsAccessService buttonsAccessService,
            ISensorAccessService sensorAccessService,
            ISchedulerFactory schedulerFactory,
            IDisplayManager displayManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.appSettings = appSettings;
            this.writableAppSettings = writableAppSettings;
            this.autoUpdateService = autoUpdateService;
            this.navigationService = navigationService;
            this.buttonsAccessService = buttonsAccessService;
            this.sensorAccessService = sensorAccessService;
            this.scheduler = schedulerFactory.Create();
            this.displayManager = displayManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StartAsync");

            try
            {
                // Create appSettings.user.json (if not exists)
                var appSettingsUserFile = new FileInfo(Path.Combine(this.webHostEnvironment.ContentRootPath, Program.UserSpecificAppSettingsFileName));
                if (!appSettingsUserFile.Exists)
                {
                    this.writableAppSettings.Update(a => AppSettings.Default);
                }

                // Check if there are button mappings configured
                if (!this.appSettings.CurrentValue.ButtonMappings.Any())
                {
                    this.logger.LogDebug($"Creating initial ButtonMappings in {appSettingsUserFile.Name}");
                    this.writableAppSettings.UpdateProperty(a => a.ButtonMappings, AppSettings.Default.ButtonMappings);
                }

                // Check if a new accesspoint configuration file is present
                // and merge it into appSettings.User.json (if it exists)
                var accessPointConfigFile = new FileInfo(Path.Combine(this.webHostEnvironment.ContentRootPath, "accesspoint@wlan0.json"));
                if (accessPointConfigFile.Exists)
                {
                    var accessPointConfigurationRoot = new ConfigurationBuilder()
                       .AddJsonFile(accessPointConfigFile.FullName, optional: true)
                       .Build();

                    var accessPointSection = accessPointConfigurationRoot.GetSection("AccessPoint");
                    if (accessPointSection.Exists())
                    {
                        var accessPointSettings = new AccessPointSettings();
                        accessPointSection.Bind(accessPointSettings);

                        this.logger.LogDebug($"Merging access point config file {accessPointConfigFile.Name} into appSettings.User.json");
                        this.writableAppSettings.UpdateProperty(a => a.AccessPoint, accessPointSettings);

                        accessPointConfigFile.Delete();
                    }
                }

                this.buttonsAccessService.Initialize();
                this.sensorAccessService.Initialize();

                var updateInProgress = await this.TryInstallUpdateAsync();
                if (!updateInProgress)
                {
                    var runSetup = this.appSettings.CurrentValue.RunSetup;
                    if (runSetup)
                    {
                        await this.navigationService.NavigateAsync(App.Pages.SetupPage);
                    }
                    else
                    {
                        // Schedule automatic update check for "Daily, 4:50 at night"
                        // this.scheduler.AddTask(CrontabSchedule.Parse("50 4 * * *"), async c => { await this.CheckAndStartUpdate(); });
                        // Schedule automatic update check every hour at minute 50
                        var automaticUpdateTask = new AsyncScheduledTask(
                            "AutomaticUpdateTask",
                            CrontabSchedule.Parse("50 * * * *"),
                            c => this.TryInstallUpdateAsync());
                        this.scheduler.AddTask(automaticUpdateTask);

                        var defaultButton = this.appSettings.CurrentValue.ButtonMappings.GetDefaultButtonMapping();
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
                    // What about running "sudo sh update_weatherdisplay_api.sh --pre --no-reboot --debug" here instead of delegating the update steps to another service?
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

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StopAsync");

            this.displayManager.Reset();

            LogManager.Shutdown();

            return Task.CompletedTask;
        }
    }
}
using System.Reflection;
using DisplayService.Services;
using NCrontab;
using NCrontab.Scheduler;
using NLog;
using WeatherDisplay.Api.Updater.Models;
using WeatherDisplay.Api.Updater.Services;
using WeatherDisplay.Model;
using WeatherDisplay.Services;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WeatherDisplay.Api.Services
{
    public class AutoStartupBackgroundService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IAutoUpdateService autoUpdateService;
        private readonly IScheduler scheduler;
        private readonly IDisplayManager displayManager;
        private readonly IOpenWeatherMapService openWeatherMapService;
        private readonly ITranslationService translationService;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;

        public AutoStartupBackgroundService(
            ILogger<AutoStartupBackgroundService> logger,
            IAutoUpdateService autoUpdateService,
            IScheduler scheduler,
            IDisplayManager displayManager,
            IOpenWeatherMapService openWeatherMapService,
            ITranslationService translationService,
            IDateTime dateTime,
            IAppSettings appSettings)
        {
            this.logger = logger;
            this.autoUpdateService = autoUpdateService;
            this.scheduler = scheduler;
            this.displayManager = displayManager;
            this.openWeatherMapService = openWeatherMapService;
            this.translationService = translationService;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StartAsync");

            var result = await this.CheckAndStartUpdate();
            if (!result.HasUpdate)
            {
                // Schedule automatic update check for "Daily, 4:50 at night"
                //this.scheduler.AddTask(CrontabSchedule.Parse("50 4 * * *"), async c => { await this.CheckAndStartUpdate(); });
                // Schedule automatic update check every hour at minute 50
                this.scheduler.AddTask(CrontabSchedule.Parse("*50 * * * *"), async c => { await this.CheckAndStartUpdate(); });

                // Add rendering actions + start display manager
                this.displayManager.AddWeatherRenderActions(this.openWeatherMapService, this.translationService, this.dateTime, this.appSettings);
                await this.displayManager.StartAsync(cancellationToken);
            }
        }

        private async Task<UpdateCheckResult> CheckAndStartUpdate()
        {
            var result = await this.autoUpdateService.CheckForUpdateAsync();
            if (result.HasUpdate)
            {
                var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var updateRequest = new UpdateRequest
                {
                    CurrentDirectory = currentDirectory,
                    UpdateVersion = result.UpdateVersion,
                    ExecutorSteps = GetExecutorSteps(currentDirectory, result.UpdateVersionSource),
                };
                this.autoUpdateService.StartUpdate(updateRequest);
            }

            return result;
        }

        private static IExecutorStep[] GetExecutorSteps(string currentDirectory, IUpdateVersionSource versionSource)
        {
            var downloadHttpFileStep = versionSource.GetDownloadStep();

            return new IExecutorStep[]
            {
               downloadHttpFileStep,
                new ProcessStartExecutorStep
                {
                    FileName = "sudo",
                    Arguments = "systemctl stop weatherdisplay.api.service",
                    CreateNoWindow = true,
                },
                new ExtractZipStep
                {
                    SourceArchiveFileName = downloadHttpFileStep.DestinationFileName,
                    DestinationDirectoryName = currentDirectory,
                    OverwriteFiles = true,
                    DeleteSourceArchive = true,
                },
                new ProcessStartExecutorStep
                {
                    FileName = "sudo",
                    Arguments = "systemctl daemon-reload",
                    CreateNoWindow = true,
                },
                new ProcessStartExecutorStep
                {
                    FileName = "sudo",
                    Arguments = "systemctl start weatherdisplay.api.service",
                    CreateNoWindow = true,
                },
                //new ProcessStartExecutorStep
                //{
                //    FileName = "sudo",
                //    Arguments = "reboot",
                //    CreateNoWindow = true,
                //}
            };
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StopAsync");

            await this.displayManager.ResetAsync();

            LogManager.Shutdown();
        }
    }
}
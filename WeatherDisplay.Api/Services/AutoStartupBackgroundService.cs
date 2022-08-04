using System.Reflection;
using DisplayService.Services;
using NCrontab;
using NCrontab.Scheduler;
using NLog;
using WeatherDisplay.Api.Updater.Models;
using WeatherDisplay.Api.Updater.Services;
using WeatherDisplay.Compilations;
using WeatherDisplay.Model;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.OpenWeatherMap;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WeatherDisplay.Api.Services
{
    public class AutoStartupBackgroundService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IAutoUpdateService autoUpdateService;
        private readonly IDisplayCompilationService displayCompilationService;
        private readonly IScheduler scheduler;
        private readonly IDisplayManager displayManager;
        private readonly IWeatherDisplayHardwareCoordinator gpioButtonService;

        public AutoStartupBackgroundService(
            ILogger<AutoStartupBackgroundService> logger,
            IAutoUpdateService autoUpdateService,
            IDisplayCompilationService displayCompilationService,
            IWeatherDisplayHardwareCoordinator gpioButtonService,
            IScheduler scheduler,
            IDisplayManager displayManager)
        {
            this.logger = logger;
            this.autoUpdateService = autoUpdateService;
            this.displayCompilationService = displayCompilationService;
            this.gpioButtonService = gpioButtonService;
            this.scheduler = scheduler;
            this.displayManager = displayManager;
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
                this.scheduler.AddTask(CrontabSchedule.Parse("50 * * * *"), async c => { await this.CheckAndStartUpdate(); });

                // Add rendering actions + start display manager
                await this.displayCompilationService.SelectDisplayCompilationAsync("MainWeatherDisplayCompilation");
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
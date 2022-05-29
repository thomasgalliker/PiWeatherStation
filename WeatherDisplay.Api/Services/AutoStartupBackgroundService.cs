using System.Reflection;
using DisplayService.Services;
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
        private readonly IDisplayManager displayManager;
        private readonly IOpenWeatherMapService openWeatherMapService;
        private readonly ITranslationService translationService;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;

        public AutoStartupBackgroundService(
            ILogger<AutoStartupBackgroundService> logger,
            IAutoUpdateService autoUpdateService,
            IDisplayManager displayManager,
            IOpenWeatherMapService openWeatherMapService,
            ITranslationService translationService,
            IDateTime dateTime,
            IAppSettings appSettings)
        {
            this.logger = logger;
            this.autoUpdateService = autoUpdateService;
            this.displayManager = displayManager;
            this.openWeatherMapService = openWeatherMapService;
            this.translationService = translationService;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StartAsync");

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
            else
            {
                this.displayManager.AddWeatherRenderActions(this.openWeatherMapService, this.translationService, this.dateTime, this.appSettings);
                await this.displayManager.StartAsync(cancellationToken);
            }
        }

        private static IExecutorStep[] GetExecutorSteps(string currentDirectory, IVersionSource versionSource)
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
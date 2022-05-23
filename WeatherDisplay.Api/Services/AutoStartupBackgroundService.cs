using DisplayService.Services;
using NLog;
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
                this.autoUpdateService.StartUpdate(result.UpdateVersion);
            }
            else
            {
                this.displayManager.AddWeatherRenderActions(this.openWeatherMapService, this.translationService, this.dateTime, this.appSettings);
                await this.displayManager.StartAsync(cancellationToken);
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
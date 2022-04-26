using DisplayService.Services;
using NLog;
using WeatherDisplay.Model;
using WeatherDisplay.Services;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WeatherDisplay.Api.Services
{
    public class AutoStartupBackgroundService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IDisplayManager displayManager;

        public AutoStartupBackgroundService(
            ILogger<AutoStartupBackgroundService> logger,
            IDisplayManager displayManager,
            IOpenWeatherMapService openWeatherMapService,
            ITranslationService translationService,
            IDateTime dateTime,
            IAppSettings appSettings)
        {
            this.logger = logger;
            this.displayManager = displayManager;
            this.displayManager.AddWeatherRenderActions(openWeatherMapService, translationService, dateTime, appSettings);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StartAsync");

            await this.displayManager.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("StopAsync");

            await this.displayManager.ResetAsync();

            LogManager.Shutdown();
        }
    }
}
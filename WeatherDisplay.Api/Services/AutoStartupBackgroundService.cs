using DisplayService.Services;
using WeatherDisplay.Model;
using WeatherDisplay.Services;

namespace WeatherDisplay.Api.Services
{
    public class AutoStartupBackgroundService : BackgroundService
    {
        private readonly IDisplayManager displayManager;

        public AutoStartupBackgroundService(IDisplayManager displayManager, IOpenWeatherMapService openWeatherMapService, IAppSettings appSettings)
        {
            this.displayManager = displayManager;
            this.displayManager.AddWeatherRenderActions(openWeatherMapService, appSettings);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.displayManager.StartAsync();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.displayManager.Clear();
            return base.StopAsync(cancellationToken);
        }
    }
}
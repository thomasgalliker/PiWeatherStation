using DisplayService.Services;
using WeatherDisplay.Model;
using WeatherDisplay.Services;

namespace WeatherDisplay.Api.Services
{
    public class AutoStartupBackgroundService : IHostedService
    {
        private readonly IDisplayManager displayManager;

        public AutoStartupBackgroundService(
            IDisplayManager displayManager,
            IOpenWeatherMapService openWeatherMapService,
            IDateTime dateTime,
            IAppSettings appSettings)
        {
            this.displayManager = displayManager;
            this.displayManager.AddWeatherRenderActions(openWeatherMapService, dateTime, appSettings);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this.displayManager.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this.displayManager.ClearAsync();
        }
    }
}
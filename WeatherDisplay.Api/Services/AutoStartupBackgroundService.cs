using DisplayService.Services;
using WeatherDisplay.Model;
using WeatherDisplay.Services;

namespace WeatherDisplay.Api.Services
{
    public class AutoStartupBackgroundService : IHostedService
    {
        private readonly IDisplayManager displayManager;

        public AutoStartupBackgroundService(IDisplayManager displayManager, IOpenWeatherMapService openWeatherMapService, IAppSettings appSettings)
        {
            this.displayManager = displayManager;
            this.displayManager.AddWeatherRenderActions(openWeatherMapService, appSettings);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this.displayManager.StartAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.displayManager.Clear();
            return Task.CompletedTask;
        }
    }
}
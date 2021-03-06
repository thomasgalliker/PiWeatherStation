using DisplayService.Services;
using Microsoft.AspNetCore.Mvc;
using WeatherDisplay.Model;
using WeatherDisplay.Services;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/weatherdisplay")]
    public class WeatherDisplayController : ControllerBase
    {
        private readonly IDisplayManager displayManager;
        private readonly IOpenWeatherMapService openWeatherMapService;
        private readonly ITranslationService translationService;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;

        public WeatherDisplayController(
            IDisplayManager displayManager,
            IOpenWeatherMapService openWeatherMapService,
            ITranslationService translationService,
            IDateTime dateTime,
            IAppSettings appSettings)
        {
            this.displayManager = displayManager;
            this.openWeatherMapService = openWeatherMapService;
            this.translationService = translationService;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
        }

        [HttpGet("start")]
        public async Task StartAsync()
        {
            await this.displayManager.ResetAsync();
            this.displayManager.AddWeatherRenderActions(this.openWeatherMapService, this.translationService, this.dateTime, this.appSettings);
            await this.displayManager.StartAsync();
        }
    }
}
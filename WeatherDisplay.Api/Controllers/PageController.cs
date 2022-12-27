using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Process;
using WeatherDisplay.Api.Services.Configuration;
using WeatherDisplay.Model;
using WeatherDisplay.Pages;
using WeatherDisplay.Pages.MeteoSwiss;
using WeatherDisplay.Pages.OpenWeatherMap;
using WeatherDisplay.Pages.Wiewarm;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/pages")]
    public class PageController : ControllerBase
    {
        private readonly IWritableOptions<AppSettings> appSettings;
        private readonly IProcessRunner processRunner;

        public PageController(
            IWritableOptions<AppSettings> appSettings,
            IProcessRunner processRunner)
        {
            this.appSettings = appSettings;
            this.processRunner = processRunner;
        }

        [HttpPost(nameof(OpenWeatherMapPage))]
        public void ConfigureOpenWeatherMapPageOptions(
            [FromServices] IWritableOptions<OpenWeatherMapPageOptions> openWeatherMapPageOptions,
            [FromBody] OpenWeatherMapPageOptions options)
        {
            // TODO: Input validation!

            openWeatherMapPageOptions.Update((o) =>
            {
                return options;
            });

            //this.processRunner.ExecuteCommand("sudo reboot");
        }

        [HttpPost(nameof(TemperatureDiagramPage))]
        public void ConfigureTemperatureDiagramPageOptions(
            [FromServices] IWritableOptions<TemperatureDiagramPageOptions> temperatureDiagramPageOptions,
            [FromBody] TemperatureDiagramPageOptions options)
        {
            // TODO: Input validation!

            temperatureDiagramPageOptions.Update((o) =>
            {
                return options;
            });

            //this.processRunner.ExecuteCommand("sudo reboot");
        }

        [HttpPost(nameof(MeteoSwissWeatherPage))]
        public void ConfigureMeteoSwissWeatherPageOptions(
            [FromServices] IWritableOptions<MeteoSwissWeatherPageOptions> meteoSwissWeatherPageOptions,
            [FromBody] MeteoSwissWeatherPageOptions options)
        {
            // TODO: Input validation!

            meteoSwissWeatherPageOptions.Update((o) =>
            {
                return options;
            });

            //this.processRunner.ExecuteCommand("sudo reboot");
        }

        [HttpPost(nameof(WaterTemperaturePage))]
        public void ConfigureWaterTemperaturePageOptions(
            [FromServices] IWritableOptions<WaterTemperaturePageOptions> waterTemperaturePageOptions,
            [FromBody] WaterTemperaturePageOptions options)
        {
            // TODO: Input validation!

            waterTemperaturePageOptions.Update((o) =>
            {
                return options;
            });

            //this.processRunner.ExecuteCommand("sudo reboot");
        }
    }
}
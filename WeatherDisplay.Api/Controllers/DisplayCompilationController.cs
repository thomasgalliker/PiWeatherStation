using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Process;
using WeatherDisplay.Api.Services.Configuration;
using WeatherDisplay.Compilations;
using WeatherDisplay.Model;
using WeatherDisplay.Model.MeteoSwiss;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/displaycompilations")]
    public class DisplayCompilationController : ControllerBase
    {
        private readonly IWritableOptions<AppSettings> appSettings;
        private readonly IProcessRunner processRunner;

        public DisplayCompilationController(
            IWritableOptions<AppSettings> appSettings,
            IProcessRunner processRunner)
        {
            this.appSettings = appSettings;
            this.processRunner = processRunner;
        }

        [HttpPost(nameof(OpenWeatherDisplayCompilation))]
        public void ConfigureOpenWeatherDisplayCompilation(
            [FromServices] IWritableOptions<OpenWeatherDisplayCompilationOptions> openWeatherDisplayCompilationOptions,
            [FromBody] OpenWeatherDisplayCompilationOptions options)
        {
            // TODO: Input validation!

            openWeatherDisplayCompilationOptions.Update((o) =>
            {
                return options;
            });

            //this.processRunner.ExecuteCommand("sudo reboot");
        }

        [HttpPost(nameof(TemperatureWeatherDisplayCompilation))]
        public void TemperatureWeatherDisplayCompilationOptions(
            [FromServices] IWritableOptions<TemperatureWeatherDisplayCompilationOptions> temperatureWeatherDisplayCompilationOptions,
            [FromBody] TemperatureWeatherDisplayCompilationOptions options)
        {
            // TODO: Input validation!

            temperatureWeatherDisplayCompilationOptions.Update((o) =>
            {
                return options;
            });

            //this.processRunner.ExecuteCommand("sudo reboot");
        }

        [HttpPost(nameof(MeteoSwissWeatherDisplayCompilation))]
        public void MeteoSwissWeatherDisplayCompilationOptions(
            [FromServices] IWritableOptions<MeteoSwissWeatherDisplayCompilationOptions> meteoSwissWeatherDisplayCompilationOptions,
            [FromBody] MeteoSwissWeatherDisplayCompilationOptions options)
        {
            // TODO: Input validation!

            meteoSwissWeatherDisplayCompilationOptions.Update((o) =>
            {
                return options;
            });

            //this.processRunner.ExecuteCommand("sudo reboot");
        }

        [HttpPost(nameof(WaterTemperatureDisplayCompilation))]
        public void WaterTemperatureDisplayCompilationOptions(
            [FromServices] IWritableOptions<WaterTemperatureDisplayCompilationOptions> waterTemperatureDisplayCompilationOptions,
            [FromBody] WaterTemperatureDisplayCompilationOptions options)
        {
            // TODO: Input validation!

            waterTemperatureDisplayCompilationOptions.Update((o) =>
            {
                return options;
            });

            //this.processRunner.ExecuteCommand("sudo reboot");
        }
    }
}
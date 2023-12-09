using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Process;
using WeatherDisplay.Api.Services.Configuration;
using WeatherDisplay.Model;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Pages.MeteoSwiss;
using WeatherDisplay.Pages.OpenWeatherMap;
using WeatherDisplay.Pages.Wiewarm;
using INetworkManager = WeatherDisplay.Services.INetworkManager;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/system/setup")]
    public class SetupController : ControllerBase
    {
        private readonly INetworkManager networkManager;
        private readonly IWritableOptions<AppSettings> appSettings;
        private readonly IWritableOptions<OpenWeatherMapPageOptions> openWeatherMapPageOptions;
        private readonly IWritableOptions<TemperatureDiagramPageOptions> temperatureDiagramPageOptions;
        private readonly IWritableOptions<MeteoSwissWeatherPageOptions> meteoSwissWeatherPageOptions;
        private readonly IWritableOptions<WaterTemperaturePageOptions> waterTemperaturePageOptions;
        private readonly IProcessRunner processRunner;

        public SetupController(
            INetworkManager networkManager,
            IWritableOptions<AppSettings> appSettings,
            IWritableOptions<OpenWeatherMapPageOptions> openWeatherMapPageOptions,
            IWritableOptions<TemperatureDiagramPageOptions> temperatureDiagramPageOptions,
            IWritableOptions<MeteoSwissWeatherPageOptions> meteoSwissWeatherPageOptions,
            IWritableOptions<WaterTemperaturePageOptions> waterTemperaturePageOptions,
            IProcessRunner processRunner)
        {
            this.networkManager = networkManager;
            this.appSettings = appSettings;
            this.openWeatherMapPageOptions = openWeatherMapPageOptions;
            this.temperatureDiagramPageOptions = temperatureDiagramPageOptions;
            this.meteoSwissWeatherPageOptions = meteoSwissWeatherPageOptions;
            this.waterTemperaturePageOptions = waterTemperaturePageOptions;
            this.processRunner = processRunner;
        }

        [HttpGet("run")]
        public async Task RunAsync(string ssid, string psk, string place, double latitude, double longitude, int plz)
        {
            // TODO: Input validation!

            // Connect to wifi network
            await this.networkManager.ConnectToWifiAsync(ssid, psk);

            var placeObj = new Place
            {
                Name = place,
                Latitude = latitude,
                Longitude = longitude
            };

            this.openWeatherMapPageOptions.Update((o) =>
            {
                o.Places = new[]
                {
                    placeObj
                };
                return o;
            });

            this.temperatureDiagramPageOptions.Update((o) =>
            {
                o.Places = new[]
                {
                    placeObj
                };
                return o;
            });

            var meteoSwissPlace = new MeteoSwissPlace
            {
                Name = place,
                Plz = plz,
            };

            this.meteoSwissWeatherPageOptions.Update((o) =>
            {
                o.Places = new[]
                {
                    meteoSwissPlace
                };
                return o;
            });

            this.waterTemperaturePageOptions.Update((o) =>
            {
                o.Places = new[]
                {
                    place
                };
                return o;
            });

            // If everything succeeded, we set the RunSetup flag to false
            this.appSettings.UpdateProperty(a => a.RunSetup, false);

            this.processRunner.ExecuteCommand("sudo reboot");
        }
    }
}
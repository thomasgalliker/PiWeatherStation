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

        /// <summary>
        /// Connects to a wifi client network.
        /// </summary>
        /// <remarks>Use this step to connect to a wifi ssid.</remarks>
        /// <param name="ssid">The SSID (name) of the wifi.</param>
        /// <param name="psk">The pre-shared key (password) for the wifi network.</param>
        /// <response code="200">Successfully connected to the wifi network.</response>
        /// <response code="400">The wifi network does not exist or there is a failure to connect to the network.</response>
        [HttpGet("step1")]
        public async Task ConnectToWifiAsync(string ssid, string psk)
        {
            // TODO: Input validation!

            await this.networkManager.ConnectToWifiAsync(ssid, psk);
        }

        /// <summary>
        /// Configures the MeteoSwissWeatherPage.
        /// </summary>
        [HttpGet("step2")]
        public async Task ConfigureMeteoSwissWeatherPageAsync(string place, int plz)
        {
            // TODO: Input validation!

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

        }

        /// <summary>
        /// Configures the OpenWeatherMapPage.
        /// </summary>
        [HttpGet("step3")]
        public async Task ConfigureOpenWeatherMapAsync(string place, double latitude, double longitude)
        {
            // TODO: Input validation!

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
        }

        /// <summary>
        /// Configures the WaterTemperaturePage.
        /// </summary>
        [HttpGet("step4")]
        public async Task ConfigureWaterTemperatureAsync(string place, double latitude, double longitude)
        {
            // TODO: Input validation!

            this.waterTemperaturePageOptions.Update((o) =>
            {
                o.Places = new[]
                {
                    place
                };
                return o;
            });
        }

        /// <summary>
        /// Finishes the initial setup.
        /// </summary>
        /// <remarks>
        /// Marks the initial setup as finished (RunSetup=false)
        /// and restarts the system.
        /// </remarks>
        [HttpGet("finish")]
        public void FinishSetupAsync()
        {
            this.appSettings.UpdateProperty(a => a.RunSetup, false);

            this.processRunner.ExecuteCommand("sudo reboot");
        }
    }
}
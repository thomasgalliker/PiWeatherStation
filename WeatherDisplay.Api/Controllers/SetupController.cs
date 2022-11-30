using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Network;
using RaspberryPi.Process;
using WeatherDisplay.Api.Services.Configuration;
using WeatherDisplay.Compilations;
using WeatherDisplay.Model;
using WeatherDisplay.Model.MeteoSwiss;

namespace WeatherDisplay.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/system/setup")]
    public class SetupController : ControllerBase
    {
        private readonly INetworkInterfaceService networkInterfaceService;
        private readonly INetworkManager networkManager;
        private readonly IWritableOptions<AppSettings> appSettings;
        private readonly IWritableOptions<OpenWeatherDisplayCompilationOptions> openWeatherDisplayCompilationOptions;
        private readonly IWritableOptions<TemperatureWeatherDisplayCompilationOptions> temperatureWeatherDisplayCompilationOptions;
        private readonly IWritableOptions<MeteoSwissWeatherDisplayCompilationOptions> meteoSwissWeatherDisplayCompilationOptions;
        private readonly IWritableOptions<WaterTemperatureDisplayCompilationOptions> waterTemperatureDisplayCompilationOptions;
        private readonly IProcessRunner processRunner;

        public SetupController(
            INetworkInterfaceService networkInterfaceService,
            INetworkManager networkManager,
            IWritableOptions<AppSettings> appSettings,
            IWritableOptions<OpenWeatherDisplayCompilationOptions> openWeatherDisplayCompilationOptions,
            IWritableOptions<TemperatureWeatherDisplayCompilationOptions> temperatureWeatherDisplayCompilationOptions,
            IWritableOptions<MeteoSwissWeatherDisplayCompilationOptions> meteoSwissWeatherDisplayCompilationOptions,
            IWritableOptions<WaterTemperatureDisplayCompilationOptions> waterTemperatureDisplayCompilationOptions,
            IProcessRunner processRunner)
        {
            this.networkInterfaceService = networkInterfaceService;
            this.networkManager = networkManager;
            this.appSettings = appSettings;
            this.openWeatherDisplayCompilationOptions = openWeatherDisplayCompilationOptions;
            this.temperatureWeatherDisplayCompilationOptions = temperatureWeatherDisplayCompilationOptions;
            this.meteoSwissWeatherDisplayCompilationOptions = meteoSwissWeatherDisplayCompilationOptions;
            this.waterTemperatureDisplayCompilationOptions = waterTemperatureDisplayCompilationOptions;
            this.processRunner = processRunner;
        }

        [HttpGet("run")]
        public async Task RunAsync(string ssid, string psk, string place, double latitude, double longitude, int plz)
        {
            // TODO: Input validation!

            // Connect to wifi network
            var wlan0 = this.networkInterfaceService.GetByName("wlan0");

            var network = new WPASupplicantNetwork
            {
                SSID = ssid,
                PSK = psk,
            };
            await this.networkManager.SetupStationMode(wlan0, network);

            var placeObj = new Place
            {
                Name = place,
                Latitude = latitude,
                Longitude = longitude
            };

            this.openWeatherDisplayCompilationOptions.Update((o) =>
            {
                o.Places = new[]
                {
                    placeObj
                };
                return o;
            });
            
            this.temperatureWeatherDisplayCompilationOptions.Update((o) =>
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

            this.meteoSwissWeatherDisplayCompilationOptions.Update((o) =>
            {
                o.Places = new[]
                {
                    meteoSwissPlace
                };
                return o;
            });
            
            this.waterTemperatureDisplayCompilationOptions.Update((o) =>
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
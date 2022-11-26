using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Network;
using RaspberryPi.Process;
using WeatherDisplay.Api.Services.Configuration;
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
        private readonly IWritableOptions<AppSettings> woAppSettings;
        private readonly IWritableOptions<OpenWeatherDisplayCompilationOptions> woOpenWeatherDisplayCompilationOptions;
        private readonly IProcessRunner processRunner;

        public SetupController(
            INetworkInterfaceService networkInterfaceService,
            INetworkManager networkManager,
            IWritableOptions<AppSettings> woAppSettings,
            IWritableOptions<OpenWeatherDisplayCompilationOptions> woOpenWeatherDisplayCompilationOptions,
            IProcessRunner processRunner)
        {
            this.networkInterfaceService = networkInterfaceService;
            this.networkManager = networkManager;
            this.woAppSettings = woAppSettings;
            this.woOpenWeatherDisplayCompilationOptions = woOpenWeatherDisplayCompilationOptions;
            this.processRunner = processRunner;
        }

        [HttpGet("run")]
        public void Run(string ssid, string psk, string place, double latitude, double longitude)
        {
            // TODO: Input validation!

            var placeObj = new Place
            {
                Name = place,
                Latitude = latitude,
                Longitude = longitude
            };

            // Connect to wifi network
            //var wlan0 = this.networkInterfaceService.GetByName("wlan0");

            //var network = new WPASupplicantNetwork
            //{
            //    SSID = ssid,
            //    PSK = psk,
            //};
            //await this.networkManager.SetupStationMode(wlan0, network);

            this.woOpenWeatherDisplayCompilationOptions.Update((o) =>
            {
                o.Places = new[]
                {
                    placeObj
                };
                return o;
            });
            
            //this.writableAppSettings.UpdateProperty(a => a.WaterTemperatureDisplayCompilation, new WaterTemperatureDisplayCompilationOptions
            //{
            //    Places = new[]
            //    {
            //        placeObj
            //    },
            //});

            // If everything succeeded, we set the RunSetup flag to false
            this.woAppSettings.UpdateProperty(a => a.RunSetup, false);

            this.processRunner.ExecuteCommand("sudo reboot");
        }
    }
}
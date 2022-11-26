using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Network;
using RaspberryPi.Process;
using WeatherDisplay.Model;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/system/setup")]
    public class SetupController : ControllerBase
    {
        private readonly INetworkInterfaceService networkInterfaceService;
        private readonly INetworkManager networkManager;
        private readonly IAppSettings appSettings;
        //private readonly IConfiguration configuration;
        private readonly IProcessRunner processRunner;

        public SetupController(
            INetworkInterfaceService networkInterfaceService,
            INetworkManager networkManager,
            IAppSettings appSettings,
            //IWritableOptions<> configuration,
            IProcessRunner processRunner)
        {
            this.networkInterfaceService = networkInterfaceService;
            this.networkManager = networkManager;
            this.appSettings = appSettings;
            //this.configuration = configuration;
            this.processRunner = processRunner;
        }

        [HttpGet("run")]
        public async Task RunAsync(string ssid, string psk, string place, string latitude, string longitude)
        {
            // Connect to wifi network
            var wlan0 = this.networkInterfaceService.GetByName("wlan0");

            var network = new WPASupplicantNetwork
            {
                SSID = ssid,
                PSK = psk,
            };
            await this.networkManager.SetupStationMode(wlan0, network);

            // If everything succeeded, we set the RunSetup flag to false
            this.appSettings.RunSetup = false;

            this.processRunner.ExecuteCommand("sudo reboot");
        }
    }
}
using System.Net;
using Microsoft.AspNetCore.Mvc;
using RaspberryPi;
using RaspberryPi.Network;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/system/network")]
    public class NetworkController : ControllerBase
    {
        private const string DefaultIPAddress = "192.168.99.1";
        private readonly INetworkInterfaceService networkInterfaceService;
        private readonly INetworkManager networkManager;
        private readonly IWPA wpa;

        public NetworkController(
            INetworkInterfaceService networkInterfaceService,
            INetworkManager networkManager,
            IWPA wpa)
        {
            this.networkInterfaceService = networkInterfaceService;
            this.networkManager = networkManager;
            this.wpa = wpa;
        }

        [HttpGet("scan")]
        public IEnumerable<string> ScanAsync()
        {
            var wlan0 = this.networkInterfaceService.GetByName("wlan0");
            var ssids = this.wpa.ScanSSIDs(wlan0);
            return ssids;
        }

        [HttpGet("accesspoint/setup")]
        public async Task ConfigureAccessPoint(string ssid, string psk, string ipAddress = DefaultIPAddress, int? channel = null, Country country = null)
        {
            var wlan0 = this.networkInterfaceService.GetByName("wlan0");
            var parsedIPAddress = IPAddress.Parse(ipAddress);
            await this.networkManager.SetupAccessPoint2(wlan0, ssid, psk, parsedIPAddress, channel, country);
        }
        
        [HttpGet("stationmode/setup")]
        public async Task SetupStationMode(string ssid, string psk)
        {
            var wlan0 = this.networkInterfaceService.GetByName("wlan0");

            var network = new WPASupplicantNetwork
            {
                SSID = ssid,
                PSK = psk,
            };
            await this.networkManager.SetupStationMode(wlan0, network);
        }
    }
}
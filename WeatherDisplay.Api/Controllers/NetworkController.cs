using Microsoft.AspNetCore.Mvc;
using WeatherDisplay.Services;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/system/network")]
    public class NetworkController : ControllerBase
    {
        private readonly INetworkManager networkManager;

        public NetworkController(
            INetworkManager networkManager)
        {
            this.networkManager = networkManager;
        }

        [HttpGet("scan")]
        public IEnumerable<string> ScanAsync()
        {
            var ssids = this.networkManager.ScanAsync();
            return ssids;
        }

        [HttpGet("connectwifi")]
        public async Task ConnectToWifiAsync(string ssid, string psk)
        {
            await this.networkManager.ConnectToWifiAsync(ssid, psk);
        }
    }
}
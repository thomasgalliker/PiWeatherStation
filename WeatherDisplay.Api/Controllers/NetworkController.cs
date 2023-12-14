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

        [HttpPost("wifi/connect")]
        public async Task ConnectToWifiAsync([FromQuery] string ssid, [FromQuery] string psk)
        {
            await this.networkManager.ConnectToWifiAsync(ssid, psk);
        }

        [HttpGet("wifi")]
        public IEnumerable<string> GetConnectedSSIDs()
        {
            return this.networkManager.GetConnectedSSIDs();
        }
    }
}
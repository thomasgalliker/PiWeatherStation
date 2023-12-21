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

        /// <summary>
        /// Scans for wifi ssids.
        /// </summary>
        [HttpGet("wifi/scan")]
        public IEnumerable<string> ScanAsync()
        {
            var ssids = this.networkManager.ScanAsync();
            return ssids;
        }

        /// <summary>
        /// Connects to a wifi network using <paramref name="ssid"/> and <paramref name="psk"/>.
        /// </summary>
        /// <param name="ssid">The SSID.</param>
        /// <param name="psk">The pre-shared key (password for the wifi network).</param>
        [HttpPost("wifi")]
        public async Task ConnectToWifiAsync([FromQuery] string ssid, [FromQuery] string psk)
        {
            await this.networkManager.ConnectToWifiAsync(ssid, psk);
        }

        /// <summary>
        /// Removes a wifi network connection.
        /// </summary>
        /// <param name="ssid">The SSID.</param>
        [HttpDelete("wifi")]
        public async Task RemoveWifiAsync([FromQuery] string ssid)
        {
            await this.networkManager.RemoveWifiAsync(ssid);
        }

        /// <summary>
        /// Gets all connected wifi networks.
        /// </summary>
        /// <returns>List of all connected SSIDs.</returns>
        [HttpGet("wifi")]
        public IEnumerable<string> GetConnectedSSIDs()
        {
            return this.networkManager.GetConnectedSSIDs();
        }
    }
}
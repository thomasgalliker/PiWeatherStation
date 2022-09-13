using System.Net;
using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Network;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/system/network")]
    public class NetworkController : ControllerBase
    {
        private const string DefaultIPAddress = "192.168.99.1";
        private readonly IAccessPoint accessPoint;
        private readonly IWPA wpa;

        public NetworkController(
            IAccessPoint accessPoint,
            IWPA wpa)
        {
            this.accessPoint = accessPoint;
            this.wpa = wpa;
        }

        [HttpGet("report")]
        public Task<string> GetReportAsync()
        {
            var report = this.wpa.GetReportAsync();
            return report;
        }
        

        [HttpGet("accesspoint/configure")]
        public void ConfigureAccessPoint(string ssid, string psk, string ipAddress = DefaultIPAddress, int? channel = null)
        {
            var parsedIPAddress = IPAddress.Parse(ipAddress);
            this.accessPoint.ConfigureAsync(ssid, psk, parsedIPAddress, channel);
        }

        [HttpGet("wpa/updatessid")]
        public async Task UpdateSSIDAsync(string ssid, string psk, string countryCode)
        {
            await this.wpa.UpdateSSIDAsync(ssid, psk, countryCode);
        }
    }
}
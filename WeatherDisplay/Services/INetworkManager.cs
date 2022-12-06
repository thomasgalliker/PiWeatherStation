using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherDisplay.Services
{
    public interface INetworkManager
    {
        IEnumerable<string> ScanAsync();

        Task<(string SSID, string PSK)> SetupAccessPoint();

        Task SetupStationMode(string ssid, string psk);
    }
}

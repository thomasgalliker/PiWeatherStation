using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherDisplay.Services
{
    public interface INetworkManager
    {
        IEnumerable<string> ScanAsync();

        Task<(string SSID, string PSK)> SetupAccessPointAsync();

        Task ConnectToWifiAsync(string ssid, string psk);

        Task RemoveWifiAsync(string ssid);

        IEnumerable<string> GetConnectedSSIDs();

        Task<IEnumerable<string>> GetConfiguredSSIDsAsync();
    }
}

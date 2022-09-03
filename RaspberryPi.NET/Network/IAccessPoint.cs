using System.Net;
using System.Threading.Tasks;

namespace RaspberryPi.Network
{
    public interface IAccessPoint
    {
        /// <summary>
        /// Configures an access point using hostapd service.
        /// </summary>
        /// <param name="ssid">SSID to be used in IEEE 802.11 management frames.</param>
        /// <param name="psk">WPA pre-shared keys for WPA-PSK.</param>
        /// <param name="ipAddress">The IP address to be used for this access point.</param>
        /// <param name="channel">Channel number (IEEE 802.11).</param>
        Task ConfigureAsync(string ssid, string psk, IPAddress ipAddress, int? channel = null);

        bool IsEnabled();

        void Start();

        void Stop();
    }
}
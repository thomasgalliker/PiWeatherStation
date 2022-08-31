using System.Net;
using System.Threading.Tasks;

namespace RaspberryPi.Network
{
    public interface IAccessPoint
    {
        Task ConfigureAsync(string ssid, string psk, IPAddress ipAddress, int channel);
        bool IsEnabled();
        void Start();
        void Stop();
    }
}
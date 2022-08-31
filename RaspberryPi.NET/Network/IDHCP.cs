using System.Net;
using System.Threading.Tasks;

namespace RaspberryPi.Network
{
    public interface IDHCP
    {
        Task<IPAddress> GetConfiguredDNSServer(string iface);

        Task<IPAddress> GetConfiguredGateway(string iface);

        Task<IPAddress> GetConfiguredIPAddress(string iface);

        Task<IPAddress> GetConfiguredNetmask(string iface);

        Task<bool> IsAPConfigured();

        Task SetIPAddress(string iface, IPAddress ip, IPAddress netmask, IPAddress gateway, IPAddress dnsServer, bool? forAP = null);
    }
}
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

        /// <summary>
        /// Sets the IP address.
        /// </summary>
        /// <param name="iface"></param>
        /// <param name="ip"></param>
        /// <param name="netmask"></param>
        /// <param name="gateway"></param>
        /// <param name="dnsServer"></param>
        /// <param name="forAP"></param>
        /// <returns></returns>
        Task SetIPAddressAsync(string iface, IPAddress ip, IPAddress netmask, IPAddress gateway, IPAddress dnsServer, bool? forAP = null);
    }
}
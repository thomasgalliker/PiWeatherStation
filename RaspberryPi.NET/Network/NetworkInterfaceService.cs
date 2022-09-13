using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using SystemNetworkInterface = System.Net.NetworkInformation.NetworkInterface;

namespace RaspberryPi.Network
{
    public class NetworkInterfaceService : INetworkInterfaceService
    {
        public IEnumerable<INetworkInterface> GetAllNetworkInterfaces()
        {
            return SystemNetworkInterface.GetAllNetworkInterfaces().Select(i => new NetworkInterface(i));
        }
    }
}

using System.Net.NetworkInformation;
using SystemNetworkInterface = System.Net.NetworkInformation.NetworkInterface;

namespace RaspberryPi.Network
{
    internal class NetworkInterface : INetworkInterface
    {
        private readonly SystemNetworkInterface networkInterface;

        public NetworkInterface(SystemNetworkInterface networkInterface)
        {
            this.networkInterface = networkInterface;
        }

        public string Name => this.networkInterface.Name;

        public OperationalStatus OperationalStatus => this.networkInterface.OperationalStatus;

        public IPInterfaceProperties GetIPProperties()
        {
            return this.networkInterface.GetIPProperties();
        }
    }
}
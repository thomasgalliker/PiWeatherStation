using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using RaspberryPi.Network;
using RaspberryPi;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace WeatherDisplay.Services
{
    public class NetworkManager : INetworkManager
    {
        private const string DefaultIPAddress = "192.168.99.1";
        private const int Channel = 6;
        private const string WlanNetworkName = "wlan0";
        private static readonly Country Country = Countries.Switzerland;
        private readonly INetworkInterfaceService networkInterfaceService;
        private readonly RaspberryPi.Network.INetworkManager networkManager;
        private readonly IWPA wpa;
        private readonly ISystemInfoService systemInfoService;

        public NetworkManager(
            INetworkInterfaceService networkInterfaceService,
            RaspberryPi.Network.INetworkManager networkManager,
            IWPA wpa,
            ISystemInfoService systemInfoService)
        {
            this.networkInterfaceService = networkInterfaceService;
            this.networkManager = networkManager;
            this.wpa = wpa;
            this.systemInfoService = systemInfoService;
        }

        public IEnumerable<string> ScanAsync()
        {
            var wlan0 = this.GetWlanNetworkInterface();
            var ssids = this.wpa.ScanSSIDs(wlan0);
            return ssids;
        }

        private INetworkInterface GetWlanNetworkInterface()
        {
            INetworkInterface iface;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                iface = this.networkInterfaceService.GetAll()
                    .FirstOrDefault(i => i.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && i.OperationalStatus == OperationalStatus.Up);
            }
            else
            {
                iface = this.networkInterfaceService.GetByName(WlanNetworkName);
            }

            return iface;
        }

        public async Task<(string SSID, string PSK)> SetupAccessPoint()
        {
            var cpuInfo = await this.systemInfoService.GetCpuInfoAsync();
            var ssid = $"PiWeatherDisplay_{cpuInfo.Serial.Substring(cpuInfo.Serial.Length - 4).ToUpperInvariant()}";
            var psk = Guid.NewGuid().ToString("N").Substring(0, 8);
            var wlan0 = this.GetWlanNetworkInterface();
            var parsedIPAddress = IPAddress.Parse(DefaultIPAddress);
            await this.networkManager.SetupAccessPoint(wlan0, ssid, psk, parsedIPAddress, Channel, Country);
            return (ssid, psk);
        }

        public async Task SetupStationMode(string ssid, string psk)
        {
            var wlan0 = this.GetWlanNetworkInterface();

            var network = new WPASupplicantNetwork
            {
                SSID = ssid,
                PSK = psk,
            };
            await this.networkManager.SetupStationMode(wlan0, network);
        }
    }
}

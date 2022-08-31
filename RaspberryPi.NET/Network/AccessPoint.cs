using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RaspberryPi.Services;
using RaspberryPi.Storage;

namespace RaspberryPi.Network
{
    /// <summary>
    /// Functions for access point mode
    /// </summary>
    public class AccessPoint : IAccessPoint
    {
        private const string HostapdServiceName = "hostapd@wlan0.service";
        private const string HostapdWlan0ConfFilePath = "/etc/hostapd/wlan0.conf";
        private const string InterfaceName = "wlan0";
        private const string DnsmasqServiceName = "dnsmasq.service";
        private const string DnsmasqConfFilePath = "/etc/dnsmasq.conf";

        private readonly ILogger logger;
        private readonly ISystemCtl systemCtl;
        private readonly IWPA wpa;
        private readonly IDHCP dhcp;
        private readonly IFileSystem fileSystem;

        public AccessPoint(
            ILogger<AccessPoint> logger,
            ISystemCtl systemCtl,
            IWPA wpa,
            IDHCP dhcp,
            IFileSystem fileSystem)
        {
            this.logger = logger;
            this.systemCtl = systemCtl;
            this.wpa = wpa;
            this.dhcp = dhcp;
            this.fileSystem = fileSystem;
        }


        /// <summary>
        /// Check if the given adapter is in access point mode
        /// </summary>
        /// <param name="iface">Name of the interface</param>
        /// <returns>True if the adapter is in AP mode</returns>
        public bool IsEnabled()
        {
            return
                this.fileSystem.Exists(HostapdWlan0ConfFilePath) &&
                this.systemCtl.IsActive(HostapdServiceName);
        }

        /// <summary>
        /// Start AP mode
        /// </summary>
        /// <returns>Start result</returns>
        public void Start()
        {
            if (!this.systemCtl.IsActive(HostapdServiceName))
            {
                this.systemCtl.StartService(HostapdServiceName);
                this.systemCtl.EnableService(HostapdServiceName);
            }

            if (!this.systemCtl.IsActive(DnsmasqServiceName))
            {
                this.systemCtl.StartService(DnsmasqServiceName);
                this.systemCtl.EnableService(DnsmasqServiceName);
            }
        }

        /// <summary>
        /// Stop AP mode
        /// </summary>
        /// <returns>Stop result</returns>
        public void Stop()
        {

            if (this.systemCtl.IsActive(HostapdServiceName))
            {
                this.systemCtl.StopService(HostapdServiceName);
                this.systemCtl.DisableService(HostapdServiceName);
            }

            if (this.systemCtl.IsActive(DnsmasqServiceName))
            {
                this.systemCtl.StopService(DnsmasqServiceName);
                this.systemCtl.DisableService(DnsmasqServiceName);
            }
        }

        /// <summary>
        /// Configure access point mode
        /// </summary>
        /// <param name="ssid">SSID to use</param>
        /// <param name="psk">Password to use</param>
        /// <param name="ipAddress">IP address</param>
        /// <param name="channel">Optional channel number</param>
        /// <returns></returns>
        public async Task ConfigureAsync(string ssid, string psk, IPAddress ipAddress, int channel)
        {
            var countryCode = await this.wpa.GetCountryCode();

            if (string.IsNullOrWhiteSpace(countryCode))
            {
                throw new InvalidOperationException("Cannot configure access point because no country code has been set. Use M587 C to set it first");
            }

            if (ssid == "*")
            {
                // Delete configuration files again
                var fileDeleted = false;
                if (this.fileSystem.Exists(HostapdWlan0ConfFilePath))
                {
                    this.fileSystem.Delete(HostapdWlan0ConfFilePath);
                    fileDeleted = true;
                }

                if (this.fileSystem.Exists(DnsmasqConfFilePath))
                {
                    this.fileSystem.Delete(DnsmasqConfFilePath);
                    fileDeleted = true;
                }

                if (fileDeleted)
                {
                    // Reset IP address configuration to station mode
                    await this.dhcp.SetIPAddress(InterfaceName, IPAddress.Any, null, null, null);
                }
            }
            else
            {
                // Write hostapd config
                using (FileStream hostapdTemplateStream = new(Path.Combine(Directory.GetCurrentDirectory(), "hostapd.conf"), FileMode.Open, FileAccess.Read))
                {
                    using StreamReader reader = new(hostapdTemplateStream);
                    using FileStream hostapdConfigStream = new(HostapdWlan0ConfFilePath, FileMode.Create, FileAccess.Write);
                    using StreamWriter writer = new(hostapdConfigStream);

                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        line = line.Replace("{ssid}", ssid);
                        line = line.Replace("{psk}", psk);
                        line = line.Replace("{channel}", channel.ToString());
                        line = line.Replace("{countryCode}", countryCode);
                        await writer.WriteLineAsync(line);
                    }
                }

                // Write dnsmasq config
                using (var dnsmasqTemplateStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "dnsmasq.conf"), FileMode.Open, FileAccess.Read))
                {
                    using StreamReader reader = new(dnsmasqTemplateStream);
                    using FileStream dnsmasqConfigStream = new(DnsmasqConfFilePath, FileMode.Create, FileAccess.Write);
                    using StreamWriter writer = new(dnsmasqConfigStream);

                    var ip = ipAddress.GetAddressBytes();
                    var ipRangeStart = $"{ip[0]}.{ip[1]}.{ip[2]}.{(ip[3] is < 100 or > 150 ? 100 : 151)}";
                    var ipRangeEnd = $"{ip[0]}.{ip[1]}.{ip[2]}.{(ip[3] is < 100 or > 150 ? 150 : 200)}";

                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        line = line.Replace("{ipRangeStart}", ipRangeStart);
                        line = line.Replace("{ipRangeEnd}", ipRangeEnd);
                        line = line.Replace("{ipAddress}", ipAddress.ToString());
                        await writer.WriteLineAsync(line);
                    }
                }

                // Set IP address configuration for AP mode
                await this.dhcp.SetIPAddress(InterfaceName, ipAddress, null, null, null, true);
            }
        }
    }
}
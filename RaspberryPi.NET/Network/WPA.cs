using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RaspberryPi.Services;
using RaspberryPi.Storage;
using NetworkInterface = System.Net.NetworkInformation.NetworkInterface;

namespace RaspberryPi.Network
{
    /// <summary>
    /// Functions for WiFi network management via wpa_supplicant
    /// </summary>
    public class WPA : IWPA
    {
        private const string WpaSupplicantService = "wpa_supplicant.service";
        private const string WpaSupplicantConfFilePath = "/etc/wpa_supplicant/wpa_supplicant.conf";
        private readonly ILogger logger;
        private readonly ISystemCtl systemCtl;
        private readonly IFileSystem fileSystem;
        private readonly IDHCP dhcp;

        public WPA(
            ILogger<WPA> logger,
            ISystemCtl systemCtl,
            IFileSystem fileSystem,
            IDHCP dhcp)
        {
            this.logger = logger;
            this.systemCtl = systemCtl;
            this.fileSystem = fileSystem;
            this.dhcp = dhcp;
        }

        /// <summary>
        /// Start wpa_supplicant for station mode
        /// </summary>
        /// <returns>Start result</returns>
        public async Task Start()
        {
            if (!this.systemCtl.IsEnabled(WpaSupplicantService))
            {
                await this.SetIPAddress(null, null, null, null);
                this.systemCtl.StartService(WpaSupplicantService);
                this.systemCtl.EnableService(WpaSupplicantService);
            }
        }

        /// <summary>
        /// Stop wpa_supplicant for station mode
        /// </summary>
        /// <returns>Stop result</returns>
        public void Stop()
        {
            if (this.systemCtl.IsEnabled(WpaSupplicantService))
            {
                this.systemCtl.StopService(WpaSupplicantService);
                this.systemCtl.DisableService(WpaSupplicantService);
            }
        }

        /// <summary>
        /// Retrieve a list of configured SSIDs
        /// </summary>
        /// <returns>List of configured SSIDs</returns>
        public async Task<List<string>> GetSSIDs()
        {
            List<string> ssids = new();
            if (this.fileSystem.File.Exists(WpaSupplicantConfFilePath))
            {
                using FileStream configStream = new(WpaSupplicantConfFilePath, FileMode.Open, FileAccess.Read);
                using StreamReader reader = new(configStream);

                var inNetworkSection = false;
                string ssid = null;
                while (!reader.EndOfStream)
                {
                    var line = (await reader.ReadLineAsync()).TrimStart();
                    if (inNetworkSection)
                    {
                        if (ssid == null)
                        {
                            if (line.StartsWith("ssid="))
                            {
                                // Parse next SSID
                                //ssid = line["ssid=".Length..].Trim(' ', '\t', '"');
                                ssid = line.Substring("ssid=".Length, line.Length).Trim(' ', '\t', '"');
                            }
                        }
                        else if (line.StartsWith("}"))
                        {
                            if (ssid != null)
                            {
                                ssids.Add(ssid);
                                ssid = null;
                            }
                            inNetworkSection = false;
                        }
                    }
                    else if (line.StartsWith("network={"))
                    {
                        inNetworkSection = true;
                    }
                }

                if (ssid != null)
                {
                    ssids.Add(ssid);
                }
            }
            return ssids;
        }

        /// <summary>
        /// Report the current WiFi stations
        /// </summary>
        /// <returns></returns>
        public async Task<string> Report()
        {
            var ssids = await this.GetSSIDs();
            if (ssids.Count > 0)
            {
                StringBuilder builder = new();

                // List SSIDs
                builder.AppendLine("Remembered networks:");
                foreach (var ssid in ssids)
                {
                    builder.AppendLine(ssid);
                }

                // Current IP address configuration
                foreach (var iface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (iface.OperationalStatus == OperationalStatus.Up && iface.Name.StartsWith("w"))
                    {
                        var ipAddress = (from item in iface.GetIPProperties().UnicastAddresses
                                         where item.Address.AddressFamily == AddressFamily.InterNetwork
                                         select item.Address).FirstOrDefault() ?? IPAddress.Any;
                        var netMask = (from item in iface.GetIPProperties().UnicastAddresses
                                       where item.Address.AddressFamily == AddressFamily.InterNetwork
                                       select item.IPv4Mask).FirstOrDefault() ?? IPAddress.Any;
                        var gateway = (from item in iface.GetIPProperties().GatewayAddresses
                                       where item.Address.AddressFamily == AddressFamily.InterNetwork
                                       select item.Address).FirstOrDefault() ?? IPAddress.Any;
                        var dnsServer = (from item in iface.GetIPProperties().DnsAddresses
                                         where item.AddressFamily == AddressFamily.InterNetwork
                                         select item).FirstOrDefault() ?? IPAddress.Any;
                        builder.AppendLine($"IP={ipAddress} GW={gateway} NM={netMask} DNS={dnsServer}");
                        break;
                    }
                }

                return builder.ToString().Trim();
            }

            // No networks available
            return null;
        }

        /// <summary>
        /// Try to read the country code from the wpa_supplicant config file
        /// </summary>
        /// <returns>Country code or null if not found</returns>
        public async Task<string> GetCountryCode()
        {
            if (this.fileSystem.File.Exists(WpaSupplicantConfFilePath))
            {
                using FileStream configStream = new(WpaSupplicantConfFilePath, FileMode.Open, FileAccess.Read);
                using StreamReader reader = new(configStream);

                while (!reader.EndOfStream)
                {
                    var line = (await reader.ReadLineAsync()).TrimStart();
                    if (line.StartsWith("country="))
                    {
                        // Country code found
                        return line.Substring("country=".Length, line.Length).Trim(' ', '\t');
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Update a given SSID or add it to the configuration, or delete either a single or all saved SSIDs
        /// </summary>
        /// <param name="ssid">SSID to update or an asterisk with password set to null to delete all the profiles</param>
        /// <param name="psk">Password of the new network or null to delete it</param>
        /// <param name="countryCode">Optional country code, must be set if no country code is present yet</param>
        /// <returns></returns>
        public async Task UpdateSSID(string ssid, string psk, string countryCode = null)
        {
            // Create template if it doesn't already exist or if the 
            if (!this.fileSystem.File.Exists(WpaSupplicantConfFilePath))
            {
                if (string.IsNullOrWhiteSpace(countryCode))
                {
                    throw new ArgumentException("WiFi country is unset. Please use M587 C to specify your country code (e.g. M587 C\"US\")");
                }

                using FileStream configTemplateStream = new(WpaSupplicantConfFilePath, FileMode.Create, FileAccess.Write);
                using StreamWriter writer = new(configTemplateStream);
                await writer.WriteLineAsync($"country={countryCode}");
                await writer.WriteLineAsync("ctrl_interface=DIR=/var/run/wpa_supplicant GROUP=netdev");
                await writer.WriteLineAsync("update_config=1");
            }

            // Rewrite wpa_supplicant.conf as requested
            var countrySeen = false;
            using (FileStream configStream = new(WpaSupplicantConfFilePath, FileMode.Open, FileAccess.ReadWrite))
            {
                // Parse the existing config file
                using var newConfigStream = new MemoryStream();
                {
                    using StreamReader reader = new(configStream, Encoding.UTF8, true, 1024, leaveOpen: true);
                    using StreamWriter writer = new(newConfigStream, Encoding.UTF8, 1024, leaveOpen: true);

                    StringBuilder networkSection = null;
                    string parsedSsid = null;
                    var networkUpdated = false;

                    while (!reader.EndOfStream)
                    {
                        string line = await reader.ReadLineAsync(), trimmedLine = line.TrimStart();
                        if (trimmedLine.StartsWith("country=") && !countrySeen)
                        {
                            // Read country code, replace it if requested
                            await writer.WriteLineAsync(string.IsNullOrWhiteSpace(countryCode) ? line : $"country={countryCode}");
                            countrySeen = true;
                        }
                        else if (networkSection != null)
                        {
                            // Dealing with the content of a network section
                            if (trimmedLine.StartsWith("ssid="))
                            {
                                // Parse SSID
                                parsedSsid = trimmedLine.Substring("ssid=".Length, trimmedLine.Length).Trim(' ', '\t', '"');
                                networkSection.AppendLine(line);
                            }
                            else if (parsedSsid == ssid && trimmedLine.StartsWith("psk=") && psk != null)
                            {
                                // Replace PSK
                                networkSection.AppendLine($"psk=\"{psk}\"");
                                networkUpdated = true;
                            }
                            else if (trimmedLine.StartsWith("}"))
                            {
                                // End of network section
                                networkSection.AppendLine(line);
                                if ((ssid != "*" && ssid != parsedSsid) || psk != null)
                                {
                                    await writer.WriteAsync(networkSection.ToString());
                                }
                                networkSection = null;
                                parsedSsid = null;
                            }
                            else
                            {
                                // Copy everything else
                                networkSection.AppendLine(line);
                            }
                        }
                        else if (trimmedLine.StartsWith("network={"))
                        {
                            // Entering a new network section
                            networkSection = new StringBuilder();
                            networkSection.AppendLine(line);
                        }
                        else
                        {
                            // Copy everything else
                            await writer.WriteLineAsync(line);
                        }
                    }

                    // Add missing network if required
                    if (!networkUpdated && ssid != null && psk != null)
                    {
                        await writer.WriteLineAsync("network={");
                        await writer.WriteLineAsync($"    ssid=\"{ssid}\"");
                        await writer.WriteLineAsync($"    psk=\"{psk}\"");
                        await writer.WriteLineAsync("}");
                    }
                }

                // Truncate the old config file
                configStream.Seek(0, SeekOrigin.Begin);
                configStream.SetLength(newConfigStream.Length);

                // Insert the country code at the start if it was missing before
                if (!countrySeen && !string.IsNullOrWhiteSpace(countryCode))
                {
                    using StreamWriter countryCodeWriter = new(configStream, Encoding.UTF8, 1024, leaveOpen: true);
                    await countryCodeWriter.WriteLineAsync($"country={countryCode}");
                    countrySeen = true;
                }

                // Overwrite the rest of the previous config
                newConfigStream.Seek(0, SeekOrigin.Begin);
                await newConfigStream.CopyToAsync(configStream);
            }

            if (!countrySeen)
            {
                this.logger.LogWarning("No country code found in wpa_supplicant.conf, WiFi may not work");
            }

            // Restart the service to apply the new configuration
            this.systemCtl.RestartService(WpaSupplicantService);
        }

        /// <summary>
        /// Update the IP address of the WiFi network interface
        /// </summary>
        /// <param name="ip">IP address or null if unchanged</param>
        /// <param name="netmask">Subnet mask or null if unchanged</param>
        /// <param name="gateway">Gateway or null if unchanged</param>
        /// <param name="netmask">Subnet mask or null if unchanged</param>
        /// <param name="dnsServer">Set IP address for AP mode</param>
        /// <returns>Asynchronous task</returns>
        public async Task SetIPAddress(IPAddress ip, IPAddress netmask, IPAddress gateway, IPAddress dnsServer, bool forAP = false)
        {
            await this.dhcp.SetIPAddressAsync("wlan0", ip, netmask, gateway, dnsServer, forAP);
        }
    }
}
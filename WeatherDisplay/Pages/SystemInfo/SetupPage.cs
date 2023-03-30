using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Resources;
using DisplayService.Services;
using Microsoft.Extensions.Options;
using NCrontab;
using RaspberryPi.Network;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Resources.Strings;
using WeatherDisplay.Services.Navigation;
using WeatherDisplay.Services.QR;

namespace WeatherDisplay.Pages.SystemInfo
{
    public class SetupPage : INavigatedTo
    {
        private readonly IDisplayManager displayManager;
        private readonly IDateTime dateTime;
        private readonly IOptionsMonitor<AppSettings> appSettings;
        private readonly IWPA wpa;
        private readonly IAccessPoint accessPoint;
        private readonly INetworkInterfaceService networkInterfaceService;
        private readonly IQRCodeService qrCodeService;

        public SetupPage(
            IDisplayManager displayManager,
            IDateTime dateTime,
            IOptionsMonitor<AppSettings> appSettings,
            IWPA wpa,
            IAccessPoint accessPoint,
            INetworkInterfaceService networkInterfaceService,
            IQRCodeService qrCodeService)
        {
            this.displayManager = displayManager;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
            this.wpa = wpa;
            this.accessPoint = accessPoint;
            this.networkInterfaceService = networkInterfaceService;
            this.qrCodeService = qrCodeService;
        }

        public async Task OnNavigatedToAsync(INavigationParameters navigationParameters)
        {
            var wlan0 = this.GetWifiNetworkInterface();
            var connectedClients = this.GetConnectedClients(wlan0);
            var connectedSSIDs = this.GetConnectedSSIDs(wlan0);

            // Date header
            this.displayManager.AddRenderActions(
                () =>
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

                    return new List<IRenderAction>
                    {
                        new RenderActions.Rectangle
                        {
                            X = 0,
                            Y = 0,
                            Height = 100,
                            Width = 800,
                            BackgroundColor = "#000000",
                        },
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 50,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = this.dateTime.Now.ToString("dddd, d. MMMM"),
                            ForegroundColor = "#FFFFFF",
                            FontSize = 70,
                            AdjustsFontSizeToFitWidth = true,
                            AdjustsFontSizeToFitHeight = true,
                            Bold = true,
                        },
                        
                        // Version
                        new RenderActions.Text
                        {
                            X = 798,
                            Y = 88,
                            HorizontalTextAlignment = HorizontalAlignment.Right,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"v{fvi.ProductVersion}",
                            ForegroundColor = "#FFFFFF",
                            BackgroundColor = "#000000",
                            FontSize = 12,
                            Bold = false,
                        },
                    };
                },
                CrontabSchedule.Parse("0 0 * * *")); // Update every day at 00:00


            if (!(this.appSettings.CurrentValue.AccessPoint is AccessPointSettings accessPointSettings))
            {
                this.displayManager.AddRenderActions(() =>
                {
                    return new List<IRenderAction>
                    {
                        new RenderActions.Rectangle
                        {
                            X = 0,
                            Y = 100,
                            Width = 800,
                            Height = 380,
                            BackgroundColor = Colors.White,
                        },
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 120,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = "Access point configuration could not be found",
                            FontSize = 20,
                        },
                    };
                });
            }
            else
            {
                // Setup info
                this.displayManager.AddRenderActions(
                () =>
                {
                    var qrCodeBitmap = this.qrCodeService.GenerateWifiQRCode(accessPointSettings.SSID, accessPointSettings.PSK);

                    var renderActions = new List<IRenderAction>
                    {
                            new RenderActions.Rectangle
                            {
                                X = 0,
                                Y = 100,
                                Width = 800,
                                Height = 380,
                                BackgroundColor = Colors.White,
                            },
                            new RenderActions.Text
                            {
                                X = 20,
                                Y = 120,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = Translations.WifiSetupIntroLabelText,
                                FontSize = 20,
                            },
                            new RenderActions.Text
                            {
                                X = 20,
                                Y = 140,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{Translations.WifiSSIDLabelText}: {accessPointSettings.SSID}",
                                FontSize = 20,
                            },
                            new RenderActions.Text
                            {
                                X = 20,
                                Y = 160,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{Translations.WifiPSKLabelText}: {accessPointSettings.PSK}",
                                FontSize = 20,
                            },
                            new RenderActions.StreamImage
                            {
                                X = 780,
                                Y = 120,
                                Image = qrCodeBitmap.ToStream(),
                                Width= 340,
                                Height= 340,
                                HorizontalAlignment = HorizontalAlignment.Right
                            },
                    };

                    if (this.appSettings.CurrentValue.IsDebug)
                    {

                    }

                    if (connectedClients.Any())
                    {
                        var yOffset = 200;
                        renderActions.Add(new RenderActions.Text
                        {
                            X = 20,
                            Y = yOffset,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Connected clients:",
                            FontSize = 20,
                        });

                        foreach (var connectedClient in connectedClients)
                        {
                            yOffset += 20;

                            renderActions.Add(new RenderActions.Text
                            {
                                X = 20,
                                Y = yOffset,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{PhysicalAddressFormat(connectedClient.MacAddress)} ({connectedClient.ConnectedTime})",
                                FontSize = 20,
                            });
                        }
                    }

                    if (connectedSSIDs.Any())
                    {
                        var yOffset = 300;
                        renderActions.Add(new RenderActions.Text
                        {
                            X = 20,
                            Y = yOffset,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Connected to WiFi networks:",
                            FontSize = 20,
                        });

                        foreach (var connectedSSID in connectedSSIDs)
                        {
                            yOffset += 20;

                            renderActions.Add(new RenderActions.Text
                            {
                                X = 20,
                                Y = yOffset,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = connectedSSID,
                                FontSize = 20,
                            });
                        }
                    }

                    return renderActions;
                });
            }
        }

        private static string PhysicalAddressFormat(PhysicalAddress physicalAddress)
        {
            return string.Join(":", physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")).ToArray());
        }

        private IEnumerable<ConnectedAccessPointClient> GetConnectedClients(INetworkInterface wlan0)
        {
            IEnumerable<ConnectedAccessPointClient> connectedClients;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connectedClients = new List<ConnectedAccessPointClient>
                {
                    new ConnectedAccessPointClient
                    {
                        MacAddress = PhysicalAddress.Parse("00-11-22-33-44-55"),
                        ConnectedTime = TimeSpan.FromMinutes(1),
                    }
                };
            }
            else
            {
                connectedClients = this.accessPoint.GetConnectedClients(wlan0);
            }

            return connectedClients;
        }

        private IEnumerable<string> GetConnectedSSIDs(INetworkInterface wlan0)
        {
            IEnumerable<string> connectedSSIDs;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connectedSSIDs = new List<string>
                {
                    "testssid",
                };
            }
            else
            {
                connectedSSIDs = this.wpa.GetConnectedSSIDs(wlan0);
            }

            return connectedSSIDs;
        }

        private INetworkInterface GetWifiNetworkInterface()
        {
            INetworkInterface iface;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                iface = this.networkInterfaceService.GetAll()
                    .FirstOrDefault(i => i.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && i.OperationalStatus == OperationalStatus.Up);
            }
            else
            {
                iface = this.networkInterfaceService.GetByName("wlan0");
            }

            return iface;
        }

        public Task OnNavigatedFromAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }
    }
}

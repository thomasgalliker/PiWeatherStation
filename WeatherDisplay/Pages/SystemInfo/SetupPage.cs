using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using DisplayService.Model;
using DisplayService.Resources;
using DisplayService.Services;
using Microsoft.Extensions.Options;
using NCrontab;
using RaspberryPi;
using RaspberryPi.Network;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Resources.Strings;
using WeatherDisplay.Services.Navigation;
using WeatherDisplay.Services.QR;
using WeatherDisplay.Utils;

namespace WeatherDisplay.Pages.SystemInfo
{
    public class SetupPage : ISystemPage, INavigatedTo
    {
        private readonly IDisplayManager displayManager;
        private readonly IDateTime dateTime;
        private readonly IOptionsMonitor<AppSettings> appSettings;
        private readonly IWPA wpa;
        private readonly INetworkInterfaceService networkInterfaceService;
        private readonly IQRCodeService qrCodeService;
        private readonly ISystemInfoService systemInfoService;

        public SetupPage(
            IDisplayManager displayManager,
            IDateTime dateTime,
            IOptionsMonitor<AppSettings> appSettings,
            IWPA wpa,
            INetworkInterfaceService networkInterfaceService,
            IQRCodeService qrCodeService,
            ISystemInfoService systemInfoService)
        {
            this.displayManager = displayManager;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
            this.wpa = wpa;
            this.networkInterfaceService = networkInterfaceService;
            this.qrCodeService = qrCodeService;
            this.systemInfoService = systemInfoService;
        }

        public async Task OnNavigatedToAsync(INavigationParameters navigationParameters)
        {
            HostInfo hostInfo;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                hostInfo = await this.systemInfoService.GetHostInfoAsync();
            }
            else
            {
                hostInfo = new HostInfo { Hostname = "raspi_0000000000000" };
            }

            var wlan0 = this.GetWifiNetworkInterface();
            var connectedSSIDs = this.GetConnectedSSIDs(wlan0);

            // Date header
            this.displayManager.AddRenderActions(
                () =>
                {
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
                            Value = $"v{FileVersionInfoHelper.GetProductVersion(this.appSettings.CurrentValue.IsDebug)}",
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
                                Value = $"{Translations.SetupPage_HostnameLabelText}: {hostInfo.Hostname}",
                                FontSize = 20,
                            },
                            new RenderActions.Text
                            {
                                X = 20,
                                Y = 160,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = Translations.SetupPage_WifiIntroLabelText,
                                FontSize = 20,
                            },
                            new RenderActions.Text
                            {
                                X = 20,
                                Y = 180,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{Translations.WifiSSIDLabelText}: {accessPointSettings.SSID}",
                                FontSize = 20,
                            },
                            new RenderActions.Text
                            {
                                X = 20,
                                Y = 200,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{Translations.WifiPSKLabelText}: {accessPointSettings.PSK}",
                                FontSize = 20,
                            },
                            new RenderActions.BitmapImage
                            {
                                X = 780,
                                Y = 120,
                                Image = qrCodeBitmap.ToStream(),
                                Width= 340,
                                Height= 340,
                                HorizontalAlignment = HorizontalAlignment.Right
                            },
                    };

                    if (connectedSSIDs.Any())
                    {
                        var yOffset = 240;
                        renderActions.Add(new RenderActions.Text
                        {
                            X = 20,
                            Y = yOffset,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = Translations.SetupPage_ConnectedWifiNetworks,
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

        private IEnumerable<string> GetConnectedSSIDs(INetworkInterface wlan0)
        {
            IEnumerable<string> connectedSSIDs;
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
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
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
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

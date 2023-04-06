using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Resources;
using DisplayService.Services;
using NCrontab;
using RaspberryPi;
using RaspberryPi.Network;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Resources;
using WeatherDisplay.Services.Hardware;
using WeatherDisplay.Services.Navigation;

namespace WeatherDisplay.Pages.SystemInfo
{
    public class SystemInfoPage : INavigatedTo
    {
        private readonly IDisplayManager displayManager;
        private readonly IDateTime dateTime;
        private readonly IWPA wpa;
        private readonly IAppSettings appSettings;
        private readonly ISystemInfoService systemInfoService;
        private readonly INetworkInterfaceService networkInterfaceService;
        private readonly ISensorAccessService sensorAccessService;

        public SystemInfoPage(
            IDisplayManager displayManager,
            IDateTime dateTime,
            IWPA wpa,
            IAppSettings appSettings,
            ISystemInfoService systemInfoService,
            INetworkInterfaceService networkInterfaceService,
            ISensorAccessService sensorAccessService)
        {
            this.displayManager = displayManager;
            this.dateTime = dateTime;
            this.wpa = wpa;
            this.appSettings = appSettings;
            this.systemInfoService = systemInfoService;
            this.networkInterfaceService = networkInterfaceService;
            this.sensorAccessService = sensorAccessService;
        }

        public async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

            HostInfo hostInfo = null;
            CpuInfo cpuInfo = null;
            CpuSensorsStatus cpuSensorsStatus = null;

            var osplatform = OperatingSystemHelper.GetOperatingSystem();
            if (osplatform == OSPlatform.Linux)
            {
                hostInfo = await this.systemInfoService.GetHostInfoAsync();
                cpuInfo = await this.systemInfoService.GetCpuInfoAsync();
                cpuSensorsStatus = this.systemInfoService.GetCpuSensorsStatus();
            }
            //#if DEBUG
            else if (osplatform == OSPlatform.Windows)
            {
                hostInfo = new HostInfo { Hostname = "raspi_0000000000000" };
                cpuInfo = new CpuInfo { Model = "Raspberry Pi Zero 2 W Rev 1.0" };
                cpuSensorsStatus = new CpuSensorsStatus
                {
                    Temperature = UnitsNet.Temperature.FromDegreesCelsius(38),
                    Voltage = UnitsNet.ElectricPotential.FromVolts(1.23),
                    UnderVoltageDetected = true,
                };
            }
            //#endif

            var wlan0 = this.GetWifiNetworkInterface();
            var connectedSSIDs = this.GetConnectedSSIDs(wlan0);
            var interfaces = new[] { wlan0 };
            //var interfaces = this.networkInterfaceService.GetAll();
            var networkInterfaces = interfaces.Select(i => (InterfaceName: i.Name, IPAddress: i.GetIPProperties().UnicastAddresses
                                                                                                .Where(u => u.Address.AddressFamily == AddressFamily.InterNetwork)
                                                                                                .Select(n => n.Address).FirstOrDefault())).ToList();

            var scd41 = this.sensorAccessService.Scd41;

            this.displayManager.AddRenderActions(
                () =>
                {
                    var renderActions = new List<IRenderAction>
                    {
                        //new RenderActions.StreamImage
                        //{
                        //    X = 0,
                        //    Y = 0,
                        //    Image = Images.GetSystemInfoPageBackground(),
                        //    Width = 800,
                        //    Height = 480,
                        //    HorizontalAlignment = HorizontalAlignment.Left,
                        //    VerticalAlignment = VerticalAlignment.Top,
                        //},
                        new RenderActions.Rectangle
                        {
                            X = 1,
                            Y = 1,
                            Height = 480 - 2,
                            Width = 800 - 2,
                            StrokeColor = Colors.DarkGray,
                            StrokeWidth = 2,
                        }
                    };

                    renderActions.AddRange(new IRenderAction[]
                    {
                        // Raspberry Pi Infos
                        new RaspberryPiBreakout
                        {
                            X = 400 - 5,
                            Y = 4,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = VerticalAlignment.Top,
                            StrokeColor = Colors.Black,
                        },
                        new RaspberryPiBreakout
                        {
                            X = 400 + 5,
                            Y = 4,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            StrokeColor = Colors.Black,
                        },
                        new RenderActions.Text
                        {
                            X = 128,
                            Y = 15,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = cpuInfo.Model,
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                        new RenderActions.Text
                        {
                            X = 128,
                            Y = 35,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Hostname: {hostInfo.Hostname}",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                        new RenderActions.Text
                        {
                            X = 128,
                            Y = 50,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Temp: {cpuSensorsStatus.Temperature}",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                    });

                    if (connectedSSIDs.Any())
                    {
                        var yOffset = 65;
                        renderActions.Add(new RenderActions.Text
                        {
                            X = 128,
                            Y = yOffset,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Wifi:",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        });

                        foreach (var connectedSSID in connectedSSIDs)
                        {
                            renderActions.Add(new RenderActions.Text
                            {
                                X = 228,
                                Y = yOffset,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = connectedSSID,
                                ForegroundColor = Colors.Black,
                                BackgroundColor = Colors.White,
                                FontSize = 12,
                                Bold = true,
                            });

                            yOffset += 15;
                        }
                    }

                    if (networkInterfaces.Any())
                    {
                        var yOffset = 80;
                        renderActions.Add(new RenderActions.Text
                        {
                            X = 128,
                            Y = yOffset,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"IP addresses:",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        });

                        foreach (var networkInterface in networkInterfaces)
                        {
                            renderActions.Add(new RenderActions.Text
                            {
                                X = 228,
                                Y = yOffset,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{networkInterface.InterfaceName} {networkInterface.IPAddress}",
                                ForegroundColor = Colors.Black,
                                BackgroundColor = Colors.White,
                                FontSize = 12,
                                Bold = true,
                            });

                            yOffset += 15;
                        }
                    }

                    renderActions.AddRange(new IRenderAction[]
                    {
                        new RenderActions.Text
                        {
                            X = 128,
                            Y = 95,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"PiWeatherDisplay Version: v{fvi.ProductVersion}",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        }
                    });

                    // WaveShare Display Info
                    renderActions.AddRange(new IRenderAction[]
                    {
                        new RenderActions.Text
                        {
                            X = 442,
                            Y = 15,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"WaveShare Display Controller",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                        new RenderActions.Text
                        {
                            X = 442,
                            Y = 35,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Model: WaveShare 7.5\" eInk V2",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                        new RenderActions.Text
                        {
                            X = 442,
                            Y = 50,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Resolution: 800 x 480 Pixels",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                    });

                    // Button Infos
                    renderActions.AddRange(new IRenderAction[]
                    {
                        new RenderActions.Rectangle
                        {
                            X = 800 - 1,
                            Y = 1,
                            Height = 260 - 2,
                            Width = 70 - 2,
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            StrokeColor = Colors.DarkGray,
                            StrokeWidth = 2,
                        },
                        new RenderActions.Text
                        {
                            X = 800 - 70 + 10,
                            Y = 15,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Buttons",
                            ForegroundColor = Colors.DarkGray,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                        // TODO: Show buttons mapping here
                        
                    });

                    // SCD41 Infos
                    renderActions.AddRange(new IRenderAction[]
                    {
                        new RenderActions.Rectangle
                        {
                            X = 1,
                            Y = 201,
                            Height = 140 - 2,
                            Width = 140 - 2,
                            StrokeColor = Colors.Black,
                            StrokeWidth = 2,
                            CornerRadius = 8,
                        },
                        new RenderActions.Text
                        {
                            X = 10,
                            Y = 201 + 15,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Sensirion SCD41",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                        new RenderActions.Text
                        {
                            X = 10,
                            Y = 201 + 30,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Temp: {scd41.Temperature}",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                        new RenderActions.Text
                        {
                            X = 10,
                            Y = 201 + 45,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Humidity: {scd41.RelativeHumidity}",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                        new RenderActions.Text
                        {
                            X = 10,
                            Y = 201 + 60,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"CO2: {scd41.Co2}",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },

                    });

                    // Power Info
                    renderActions.AddRange(new IRenderAction[]
                    {
                        new RenderActions.Rectangle
                        {
                            X = 1,
                            Y = 343,
                            Height = 137 - 2,
                            Width = 70 - 2,
                            StrokeColor = Colors.Black,
                            StrokeWidth = 2,
                            CornerRadius = 8,
                        },
                        new RenderActions.Text
                        {
                            X = 10,
                            Y = 343 + 15,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Input",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                        new RenderActions.Text
                        {
                            X = 10,
                            Y = 343 + 30,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"voltage:",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                        new RenderActions.Text
                        {
                            X = 10,
                            Y = 343 + 45,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{cpuSensorsStatus.Voltage}",
                            ForegroundColor = Colors.Black,
                            BackgroundColor = Colors.White,
                            FontSize = 12,
                            Bold = true,
                        },
                    });

                    if (cpuSensorsStatus.UnderVoltageDetected)
                    {
                        renderActions.Add(new RenderActions.StreamImage
                        {
                            X = 10,
                            Y = 343 + 60,
                            Image = Icons.Alert(),
                            Width = 24,
                            Height = 24,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                        });
                    }

                    return renderActions;
                },
                CrontabSchedule.Parse("0 0 * * *")); // Update every day at 00:00
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

        internal class RaspberryPiBreakout : RenderActions.Canvas
        {
            private string strokeColor = Colors.Black;

            public RaspberryPiBreakout()
            {
                var width = 300;
                var height = 138;
                var strokeWidth = 2;
                var strokeWidthHalf = strokeWidth / 2;

                this.Height = height;
                this.Width = width;
                this.HorizontalAlignment = HorizontalAlignment.Left;
                this.VerticalAlignment = VerticalAlignment.Top;

                this.Add(new RenderActions.Rectangle
                {
                    X = strokeWidthHalf,
                    Y = strokeWidthHalf,
                    Height = height - strokeWidth,
                    Width = width - strokeWidth,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    StrokeColor = StrokeColor,
                    StrokeWidth = strokeWidth,
                    CornerRadius = 16,
                });

                var distanceFromBorder = 8;
                var drillHoleDiameter = 16;

                // Top/Left
                this.Add(new RenderActions.Rectangle
                {
                    X = distanceFromBorder + strokeWidthHalf,
                    Y = distanceFromBorder + strokeWidthHalf,
                    Height = drillHoleDiameter - strokeWidth,
                    Width = drillHoleDiameter - strokeWidth,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    StrokeColor = Colors.DarkGray,
                    StrokeWidth = strokeWidth,
                    CornerRadius = drillHoleDiameter / 2,
                });

                // Top/Right
                this.Add(new RenderActions.Rectangle
                {
                    X = width - distanceFromBorder - strokeWidthHalf,
                    Y = distanceFromBorder - strokeWidthHalf,
                    Height = drillHoleDiameter - strokeWidth,
                    Width = drillHoleDiameter - strokeWidth,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top,
                    StrokeColor = Colors.DarkGray,
                    StrokeWidth = strokeWidth,
                    CornerRadius = drillHoleDiameter / 2,
                });

                // Bottom/Left
                this.Add(new RenderActions.Rectangle
                {
                    X = distanceFromBorder + strokeWidthHalf,
                    Y = height - distanceFromBorder - strokeWidthHalf,
                    Height = drillHoleDiameter - strokeWidth,
                    Width = drillHoleDiameter - strokeWidth,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    StrokeColor = Colors.DarkGray,
                    StrokeWidth = strokeWidth,
                    CornerRadius = drillHoleDiameter / 2,
                });

                // Bottom/Right
                this.Add(new RenderActions.Rectangle
                {
                    X = width - distanceFromBorder - strokeWidthHalf,
                    Y = height - distanceFromBorder - strokeWidthHalf,
                    Height = drillHoleDiameter - strokeWidth,
                    Width = drillHoleDiameter - strokeWidth,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    StrokeColor = Colors.DarkGray,
                    StrokeWidth = strokeWidth,
                    CornerRadius = drillHoleDiameter / 2,
                });
            }

            public string StrokeColor
            {
                get => this.strokeColor;
                set
                {
                    if (this.strokeColor != value)
                    {
                        this.strokeColor = value;

                        foreach (var rectangle in this.OfType<RenderActions.Rectangle>())
                        {
                            rectangle.StrokeColor = value;
                        }
                    }
                }
            }
        }
    }
}

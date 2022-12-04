using System.Collections.Generic;
using System.Reflection;
using DisplayService.Model;
using DisplayService.Services;
using NCrontab;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model;
using WeatherDisplay.Services.QR;
using RaspberryPi;
using RaspberryPi.Network;
using System.Threading.Tasks;

namespace WeatherDisplay.Pages
{
    public class SetupPage : INavigatedAware
    {
        private readonly IDisplayManager displayManager;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;
        private readonly INetworkManager networkManager;
        private readonly IQRCodeService qrCodeService;

        public SetupPage(
            IDisplayManager displayManager,
            IDateTime dateTime,
            IAppSettings appSettings,
            INetworkManager networkManager,
            IQRCodeService qrCodeService)
        {
            this.displayManager = displayManager;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
            this.networkManager = networkManager;
            this.qrCodeService = qrCodeService;
        }

        public Task OnNavigatedToAsync(INavigationParameters navigationParameters)
        {
            //this.networkManager.SetupAccessPoint()

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

            // Setup info
            this.displayManager.AddRenderActionsAsync(
                async () =>
                {
                    // TODO: Get ssid/psdk from system
                    var ssid = "testssid";
                    var psk = "testpassword";
                    var qrCodeBitmap = this.qrCodeService.GenerateWifiQRCode(ssid, psk);

                    var currentWeatherRenderActions = new List<IRenderAction>
                    {
                        new RenderActions.Rectangle
                        {
                            X = 0,
                            Y = 100,
                            Width = 800,
                            Height = 380,
                            BackgroundColor = "#FFFFFF",
                        },
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 120,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Verbinden Sie sich mit folgendem WiFi Netzwerk: {ssid} - {psk}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.StreamImage
                        {
                            X = 20,
                            Y = 140,
                            Image = qrCodeBitmap.ToStream(),
                            Width= 320,
                            Height= 320,
                        },
                    };

                    if (this.appSettings.IsDebug)
                    {

                    }

                    return currentWeatherRenderActions;
                });

            return Task.CompletedTask;
        }
    }
}

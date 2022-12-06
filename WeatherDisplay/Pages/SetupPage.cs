using System.Collections.Generic;
using System.Reflection;
using DisplayService.Model;
using DisplayService.Services;
using NCrontab;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model;
using WeatherDisplay.Services;
using WeatherDisplay.Services.QR;
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

        public async Task OnNavigatedToAsync(INavigationParameters navigationParameters)
        {
            var accessPoint = await this.networkManager.SetupAccessPoint();

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
                    var qrCodeBitmap = this.qrCodeService.GenerateWifiQRCode(accessPoint.SSID, accessPoint.PSK);

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
                            Value = $"Verbinden Sie sich mit folgendem WiFi:",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 140,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"SSID: {accessPoint.SSID}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 160,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Passwort: {accessPoint.PSK}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
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

                    if (this.appSettings.IsDebug)
                    {

                    }

                    return currentWeatherRenderActions;
                });
        }
    }
}

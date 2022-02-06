using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Timers;
using DisplayService.ConsoleApp.Service;
using DisplayService.Model;
using DisplayService.Services;
using DisplayService.Settings;
using SkiaSharp;

namespace DisplayService.ConsoleApp
{
    class MainClass
    {
        private static IDisplay displayService;
        private static IRenderService renderService;
        private static ICacheService cacheService;

        public static async Task Main(string[] args)
        {
            var longitude = 24.1567359d;
            var latitude = 67.6039143d;

            var cultureInfo = new CultureInfo("de-CH");
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            //var openWeatherMapConfiguration = new OpenWeatherMapConfiguration();
            //var openWeatherMapService = new OpenWeatherMapService(openWeatherMapConfiguration);
            var openWeatherMapService = new NullOpenWeatherMapService();

            // Setup services, wire-up dependencies
            // TODO: Use Microsoft DependencyInjection
            //displayService = new WsEPaperDisplayService("WaveShare7In5_V2");

            displayService = new NullDisplayService();

            // TODO: Load from appsettings
            IRenderSettings renderSettings = new RenderSettings
            {
                Background = SKColor.Parse("#FFFFFFFF"),
            };
            renderSettings.Resize(displayService.Width, displayService.Height);

            IDisplayManager displayManager = new DisplayManager(renderSettings, displayService);

            displayManager.AddRenderActions(
                () => new List<IRenderAction>
                {
                    new RenderActions.Rectangle
                    {
                        X = 0,
                        Y = 0,
                        Height = 90,
                        Width = 800,
                        BackgroundColor = "#99FEFF",
                    },
                    new RenderActions.Text
                    {
                        X = 20,
                        Y = 20,
                        HorizontalTextAlignment = HorizontalAlignment.Left,
                        VerticalTextAlignment = VerticalAlignment.Top,
                        Value = $"Display Demo",
                        ForegroundColor = "#B983FF",
                        FontSize = 70,
                        Bold = true,
                    },
                    new RenderActions.Rectangle
                    {
                        X = 0,
                        Y = 440,
                        Height = 40,
                        Width = 800,
                        BackgroundColor = "#99FEFF",
                    }
                });

            int dayCounter = 0;
            displayManager.AddRenderActions(
                () =>
                {
                    var dateTimeNow = DateTime.Now.AddDays(dayCounter++);
                    return new List<IRenderAction>
                    {
                        new RenderActions.Rectangle
                        {
                            X = 780,
                            Y = 19,
                            Height = 25,
                            Width = 300,
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            BackgroundColor = "#99FEFF",
                        },
                        new RenderActions.Text
                        {
                            X = 780,
                            Y = 20,
                            HorizontalTextAlignment = HorizontalAlignment.Right,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{dateTimeNow:dddd}, {dateTimeNow:M}",
                            ForegroundColor = "#99FEFF",
                            BackgroundColor = "#B983FF",
                            FontSize = 22,
                        }
                    };
                },
                TimeSpan.FromSeconds(1)); // TODO: Use crontab-like scheduling

            displayManager.AddRenderActions(
                () =>
                {
                    var dateTimeNow = DateTime.Now;
                    return new List<IRenderAction>
                    {
                        new RenderActions.Rectangle
                        {
                            X = 780,
                            Y = 19,
                            Height = 25,
                            Width = 300,
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            BackgroundColor = "#99FEFF",
                        },
                        new RenderActions.Text
                        {
                            X = 780,
                            Y = 50,
                            HorizontalTextAlignment = HorizontalAlignment.Right,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{dateTimeNow:t}",
                            ForegroundColor = "#99FEFF",
                            BackgroundColor = "#B983FF",
                            FontSize = 22,
                        }
                    };
                },
                TimeSpan.FromMinutes(1));

            displayManager.AddRenderActionsAsync(
                async () =>
                {
                    var weatherResponse = await openWeatherMapService.GetWeatherInfoAsync(longitude, latitude);

                    return new List<IRenderAction>
                    {
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 240,
                            HorizontalTextAlignment = HorizontalAlignment.Center,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = $"{weatherResponse.Temperature:F1}°C",
                            ForegroundColor = "#B983FF",
                            BackgroundColor = "#00FFFFFF",
                            FontSize = 80,
                        }
                    };
                },
                TimeSpan.FromHours(1));

            await displayManager.StartAsync();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            /*
            cacheService = new CacheService();
            renderService = new RenderService((RenderSettings)renderSettings, cacheService);

            // TODO: InitializeFromCache()
            if (cacheService.Exists())
            {
                renderService.Image(new RenderActions.Image
                {
                    X = 0,
                    Y = 0,
                    Filename = cacheService.CacheFile
                });
            }

            // Initialize display
            UpdateDisplay();

            // Start timer to automatically refresh the display
            ITimerService updateTimer = new TimerService()
            {
                TargetMillisecond = 1000,
                ToleranceMillisecond = 0,
                Enabled = true
            };
            updateTimer.Elapsed += OnUpdateTimerElapsed;
            updateTimer.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            updateTimer.Elapsed -= OnUpdateTimerElapsed;
            updateTimer.Stop();
            */
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DisplayService.ConsoleApp.Model;
using DisplayService.ConsoleApp.Services;
using DisplayService.Model;
using DisplayService.Services;
using DisplayService.Settings;
using Microsoft.Extensions.Configuration;

namespace DisplayService.ConsoleApp
{
    class MainClass
    {
        private static IDisplay displayService;

        public static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var appSettings = new AppSettings();
            var appSettingsSection = config.GetSection("AppSettings");
            appSettingsSection.Bind(appSettings);

            var openWeatherMapConfiguration = new OpenWeatherMapConfiguration();
            var openWeatherMapSection = config.GetSection("OpenWeatherMap");
            openWeatherMapSection.Bind(openWeatherMapConfiguration);

            var places = appSettings.Places;

            CultureInfo.CurrentCulture = appSettings.CultureInfo;
            CultureInfo.CurrentUICulture = appSettings.CultureInfo;

            var openWeatherMapService = new OpenWeatherMapService(openWeatherMapConfiguration);
            //var openWeatherMapService = new NullOpenWeatherMapService();

            // Setup services, wire-up dependencies
            // TODO: Use Microsoft DependencyInjection

            if (appSettings.IsDebug)
            {
                displayService = new NullDisplayService();
            }
            else
            {
                var display = appSettings.Displays.First(); // Supports only one display at the time
                switch (display.DriverType)
                {
                    case "WaveShareDisplay":
                        displayService = new WaveShareDisplay(display.Driver);
                        break;
                    default:
                        throw new NotSupportedException($"DriverType '{display.DriverType}' is not supported");
                }
            }

            // TODO: Load from appsettings
            IRenderSettings renderSettings = new RenderSettings
            {
                BackgroundColor = "#FFFFFFFF",
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
                        Value = appSettings.Title,
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
                TimeSpan.FromDays(1)); // TODO: Use crontab-like scheduling

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
                    var place = places.First();
                    var weatherResponse = await openWeatherMapService.GetWeatherInfoAsync(place.Latitude, place.Longitude);

                    return new List<IRenderAction>
                    {
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 200,
                            HorizontalTextAlignment = HorizontalAlignment.Center,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = $"{place.Name ?? weatherResponse.LocationName}",
                            ForegroundColor = "#B983FF",
                            BackgroundColor = "#00FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 240,
                            HorizontalTextAlignment = HorizontalAlignment.Center,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = FormatTemperature(weatherResponse),
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

        private static string FormatTemperature(WeatherResponse weatherResponse)
        {
            switch (weatherResponse.UnitSystem)
            {
                case "metric":
                    return $"{weatherResponse.Temperature:F1}°C";
                case "imperial":
                    return $"{weatherResponse.Temperature:F1}°F";
                default:
                    throw new NotSupportedException($"Unit system {weatherResponse.UnitSystem} not supported");
            }
        }
    }
}

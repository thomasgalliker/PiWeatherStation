using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using DisplayService.Model;
using DisplayService.Services;
using NCrontab;
using SkiaSharp;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.OpenWeatherMap;

namespace WeatherDisplay.Compilations
{
    public class TemperatureWeatherDisplayCompilation : IDisplayCompilation
    {
        private readonly IDisplayManager displayManager;
        private readonly IOpenWeatherMapService openWeatherMapService;
        private readonly ITranslationService translationService;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;

        public TemperatureWeatherDisplayCompilation(
            IDisplayManager displayManager,
            IOpenWeatherMapService openWeatherMapService,
            ITranslationService translationService,
            IDateTime dateTime,
            IAppSettings appSettings)
        {
            this.displayManager = displayManager;
            this.openWeatherMapService = openWeatherMapService;
            this.translationService = translationService;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
        }

        public string Name => "TemperatureWeatherDisplayCompilation";

        public void AddRenderActions()
        {
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


            // Current weather info
            this.displayManager.AddRenderActionsAsync(
                async () =>
                {
                    var place = this.appSettings.Places.First();

                    // Get current weather & daily forecasts
                    var oneCallOptions = new OneCallOptions
                    {
                        IncludeCurrentWeather = true,
                        IncludeDailyForecasts = true,
                        IncludeMinutelyForecasts = true,
                        IncludeHourlyForecasts = true,
                    };

                    var oneCallWeatherInfo = await this.openWeatherMapService.GetWeatherOneCallAsync(place.Latitude, place.Longitude, oneCallOptions);

                    var dailyForecasts = oneCallWeatherInfo.DailyForecasts.ToList();
                    var dailyForecastToday = dailyForecasts.OrderBy(f => f.DateTime).First();

                    var currentWeatherInfo = oneCallWeatherInfo.CurrentWeather;
                    var currentWeatherCondition = currentWeatherInfo.Weather.First();

                    var dateTimeNow = this.dateTime.Now;

                    var currentWeatherRenderActions = new List<IRenderAction>
                    {
                        // Current location + current temperature
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
                            Value = $"{place.Name}, um {dateTimeNow:t} Uhr",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        }
                    };

                    // Draw temperature diagram
                    var screen = new SKBitmap(800, 340);
                    var canvas = new SKCanvas(screen);

                    var temperatureDiagram = new TemperatureDiagram();

                    var callWeatherInfoHistoric = await this.openWeatherMapService.GetWeatherOneCallHistoricAsync(place.Latitude, place.Longitude, dateTimeNow.Date, onlyCurrent: false);
                    var weatherForecast = await this.openWeatherMapService.GetWeatherForecast4Async(place.Latitude, place.Longitude);

                    var historicTemperatureSets = callWeatherInfoHistoric.HourlyForecasts
                        .Select(h => new TemperatureSet
                        {
                            DateTime = DateTime.SpecifyKind(h.DateTime, DateTimeKind.Local),
                            Min = h.Temperature,
                            Avg = h.Temperature,
                            Max = h.Temperature,
                        });

                    var forecastTemperatureSets =
                        weatherForecast.Items
                        .Select(i => new TemperatureSet
                        {
                            DateTime = DateTime.SpecifyKind(i.DateTime, DateTimeKind.Local),
                            Min = i.Main.Temperature,
                            Avg = i.Main.Temperature,
                            Max = i.Main.Temperature,
                        })
                        .ToArray();

                    var lastForecastDate = forecastTemperatureSets.Max(t => t.DateTime);

                    var oneCallTemperatureSetsDaily = oneCallWeatherInfo.DailyForecasts
                        .Where(d => d.DateTime > lastForecastDate)
                        .SelectMany(d => new[]
                        {
                    new TemperatureSet
                    {
                        DateTime = DateTime.SpecifyKind(d.DateTime.Date, DateTimeKind.Local),
                        Min = d.Temperature.Night,
                        Avg = d.Temperature.Night,
                        Max = d.Temperature.Night,
                    },
                    new TemperatureSet
                    {
                        DateTime = DateTime.SpecifyKind(d.DateTime.Date, DateTimeKind.Local).AddHours(6d),
                        Min = d.Temperature.Morning,
                        Avg = d.Temperature.Morning,
                        Max = d.Temperature.Morning,
                    },
                    new TemperatureSet
                    {
                        DateTime = DateTime.SpecifyKind(d.DateTime.Date, DateTimeKind.Local).AddHours(12d),
                        Min = d.Temperature.Day,
                        Avg = d.Temperature.Day,
                        Max = d.Temperature.Day,
                    },
                    new TemperatureSet
                    {
                        DateTime = DateTime.SpecifyKind(d.DateTime.Date, DateTimeKind.Local).AddHours(18d),
                        Min = d.Temperature.Evening,
                        Avg = d.Temperature.Evening,
                        Max = d.Temperature.Evening,
                    },
                        });

                    var temperatureSets = historicTemperatureSets
                        .Concat(forecastTemperatureSets)
                        .OrderBy(t => t.DateTime)
                        .ToArray();

                    var precipitation = temperatureSets.Select(t => (float)t.Rain).ToArray();

                    var temperatureDiagramOptions = TemperatureDiagramOptions.Default;

                    temperatureDiagram.Draw(canvas, screen.Width, screen.Height, temperatureSets, precipitation, TemperatureRangeSelector, dateTimeNow, temperatureDiagramOptions);

                    var bitmapStream = screen.ToStream();

                    currentWeatherRenderActions.Add(new RenderActions.StreamImage
                    {
                        X = 0,
                        Y = 140,
                        Image = bitmapStream,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                    });

                    return currentWeatherRenderActions;
                },
                CrontabSchedule.Parse("*/15 * * * *")); // Update every 15mins
        }

        private static (Temperature Min, Temperature Max) TemperatureRangeSelector(IEnumerable<TemperatureSet> s)
        {
            return (s.Min(x => x.Min) - 1, s.Max(x => x.Max) + 1);
        }
    }
}

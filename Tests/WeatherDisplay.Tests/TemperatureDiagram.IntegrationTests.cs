using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisplayService.Tests.Services;
using SkiaSharp;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Services;
using WeatherDisplay.Tests.Extensions;
using WeatherDisplay.Tests.Logging;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests
{
    public class TemperatureDiagramIntegrationTests
    {
        private readonly TestHelper testHelper;
        private readonly IOpenWeatherMapService openWeatherMapService;

        public TemperatureDiagramIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            this.testHelper = new TestHelper(testOutputHelper);
            var logger = new TestOutputHelperLogger<OpenWeatherMapService>(testOutputHelper);
            var openWeatherMapConfiguration = new OpenWeatherMapConfiguration
            {
                ApiEndpoint = "https://pro.openweathermap.org",
                ApiKey = "b1b15e88fa797225412429c1c50c122a1",
                UnitSystem = "metric",
                Language = "de",
                VerboseLogging = true
            };

            this.openWeatherMapService = new OpenWeatherMapService(logger, openWeatherMapConfiguration);
        }

        [Fact]
        public async Task ShouldDrawBasicTemperatureDiagram()
        {
            // Arrange
            var now = DateTime.Now;

            var screen = new SKBitmap(1600, 480);
            var canvas = new SKCanvas(screen);

            var temperatureDiagram = new TemperatureDiagram();

            static (Temperature Min, Temperature Max) temperatureRangeSelector(IEnumerable<TemperatureSet> s)
            {
                return (s.Min(x => x.Min) - 1, s.Max(x => x.Max) + 1);
            }

            (float Min, float Max) precipitationRangeSelector(IEnumerable<float> s)
            {
                return (0f, s.Max() + 10);
            }

            var latitude = 49.2178194d;
            var longitude = 12.6663832d;

            var oneCallOptions = new OneCallOptions
            {
                IncludeDailyForecasts = true,
            };

            var weatherForecastOptions = new WeatherForecastOptions
            {
                WeatherForecastKind = WeatherForecastKind.Hourly,
            };

            var callWeatherInfoHistoric = await this.openWeatherMapService.GetWeatherOneCallHistoricAsync(latitude, longitude, now.Date, onlyCurrent: false);
            var weatherForecast = await this.openWeatherMapService.GetWeatherForecastAsync(latitude, longitude, weatherForecastOptions);
            var callWeatherInfo = await this.openWeatherMapService.GetWeatherOneCallAsync(latitude, longitude, oneCallOptions);

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

            var oneCallTemperatureSetsDaily = callWeatherInfo.DailyForecasts
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
                //.Concat(oneCallTemperatureSetsDaily)
                .OrderBy(t => t.DateTime)
                .ToArray();

            var precipitation = temperatureSets.Select(t => (float)t.Rain).ToArray();

            var temperatureDiagramOptions = TemperatureDiagramOptions.Default;

            // Act
            temperatureDiagram.Draw(canvas, screen.Width, screen.Height, temperatureSets, precipitation, temperatureRangeSelector, now, temperatureDiagramOptions);

            // Assert
            var bitmapStream = screen.ToStream();
            this.testHelper.WriteFile(bitmapStream);
        }
    }
}
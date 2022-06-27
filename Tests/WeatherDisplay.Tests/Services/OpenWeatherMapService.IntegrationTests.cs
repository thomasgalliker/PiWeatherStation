using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Services.OpenWeatherMap;
using WeatherDisplay.Tests.Logging;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Services
{
    public class OpenWeatherMapServiceIntegrationTests
    {
        private readonly ILogger<OpenWeatherMapService> logger;
        private readonly IOpenWeatherMapConfiguration openWeatherMapConfiguration;
        private readonly ITestOutputHelper testOutputHelper;

        public OpenWeatherMapServiceIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            this.logger = new TestOutputHelperLogger<OpenWeatherMapService>(testOutputHelper);
            this.openWeatherMapConfiguration = new OpenWeatherMapConfiguration
            {
                ApiEndpoint = "https://pro.openweathermap.org",
                ApiKey = "b1b15e88fa797225412429c1c50c122a1",
                //ApiEndpoint = "https://api.openweathermap.org",
                //ApiKey = "001c4dffbe586e8e2542fb379031bc99",
                UnitSystem = "metric",
                Language = "de",
                VerboseLogging = true
            };
            this.testOutputHelper = testOutputHelper;
        }

        [Theory]
        [ClassData(typeof(WeatherForecastTestData))]
        public async Task ShouldGetWeatherForecastAsync(WeatherForecastOptions options)
        {
            // Arrange
            var latitude = 47.0907124d;
            var longitude = 8.0559381d;

            IOpenWeatherMapService openWeatherMapService = new OpenWeatherMapService(this.logger, this.openWeatherMapConfiguration);

            // Act
            var weatherForecast = await openWeatherMapService.GetWeatherForecastAsync(latitude, longitude, options);

            // Assert
            var dumpOptions = new DumpOptions
            {
                DumpStyle = DumpStyle.CSharp,
                SetPropertiesOnly = true
            };

            dumpOptions.CustomInstanceFormatters.AddFormatter<Temperature>(t => $"new Temperature({t.Value}, {nameof(TemperatureUnit)}.{t.Unit})");
            dumpOptions.CustomInstanceFormatters.AddFormatter<Pressure>(p => $"new Pressure({p.Value})");
            dumpOptions.CustomInstanceFormatters.AddFormatter<Humidity>(h => $"new Humidity({h.Value})");
            dumpOptions.CustomInstanceFormatters.AddFormatter<UVIndex>(uvi => $"new UVIndex({uvi.Value})");
            this.testOutputHelper.WriteLine(ObjectDumper.Dump(weatherForecast, dumpOptions));

            _ = weatherForecast.Should().NotBeNull();
        }

        public class WeatherForecastTestData : TheoryData<WeatherForecastOptions>
        {
            public WeatherForecastTestData()
            {
                this.Add(new WeatherForecastOptions
                {
                    WeatherForecastKind = WeatherForecastKind.Default,
                });
                this.Add(new WeatherForecastOptions
                {
                    WeatherForecastKind = WeatherForecastKind.Hourly,
                });
                this.Add(new WeatherForecastOptions
                {
                    WeatherForecastKind = WeatherForecastKind.Daily,
                });
            }
        }

        [Fact]
        public async Task ShouldGetWeatherOneCallAsync()
        {
            // Arrange
            var latitude = 47.0907124d;
            var longitude = 8.0559381d;

            var oneCallOptions = new OneCallOptions
            {
                IncludeCurrentWeather = true,
                IncludeDailyForecasts = true,
                IncludeMinutelyForecasts = true,
                IncludeHourlyForecasts = true,
            };

            IOpenWeatherMapService openWeatherMapService = new OpenWeatherMapService(this.logger, this.openWeatherMapConfiguration);

            // Act
            var oneCallWeatherInfo = await openWeatherMapService.GetWeatherOneCallAsync(latitude, longitude, oneCallOptions);

            // Assert
            var dumpOptions = new DumpOptions
            {
                DumpStyle = DumpStyle.CSharp,
                SetPropertiesOnly = true
            };

            dumpOptions.CustomInstanceFormatters.AddFormatter<Temperature>(t => $"new Temperature({t.Value}, {nameof(TemperatureUnit)}.{t.Unit})");
            dumpOptions.CustomInstanceFormatters.AddFormatter<Pressure>(p => $"new Pressure({p.Value})");
            dumpOptions.CustomInstanceFormatters.AddFormatter<Humidity>(h => $"new Humidity({h.Value})");
            dumpOptions.CustomInstanceFormatters.AddFormatter<UVIndex>(uvi => $"new UVIndex({uvi.Value})");
            this.testOutputHelper.WriteLine(ObjectDumper.Dump(oneCallWeatherInfo, dumpOptions));

            _ = oneCallWeatherInfo.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldGetAirPollutionAsync()
        {
            // Arrange
            var latitude = 47.0907124d;
            var longitude = 8.0559381d;

            IOpenWeatherMapService openWeatherMapService = new OpenWeatherMapService(this.logger, this.openWeatherMapConfiguration);

            // Act
            var airPollutionInfo = await openWeatherMapService.GetAirPollutionAsync(latitude, longitude);

            // Assert
            this.testOutputHelper.WriteLine(ObjectDumper.Dump(airPollutionInfo, DumpStyle.CSharp));

            _ = airPollutionInfo.Should().NotBeNull();
        }

    }
}
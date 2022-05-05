using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using WeatherDisplay.Services;
using WeatherDisplay.Tests.Logging;
using WeatherDisplay.Tests.Testdata;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Services;

public class OpenWeatherMapServiceIntegrationTests
{
    private readonly ILogger<OpenWeatherMapService> logger;
    private readonly IOpenWeatherMapConfiguration openWeatherMapConfiguration;

    public OpenWeatherMapServiceIntegrationTests(ITestOutputHelper testOutputHelper)
    {
        this.logger = new TestOutputHelperLogger<OpenWeatherMapService>(testOutputHelper);
        this.openWeatherMapConfiguration = new OpenWeatherMapConfiguration
        {
            ApiKey = "001c4dffbe586e8e2542fb379031bc99",
            UnitSystem = "metric",
            Language = "de",
            VerboseLogging = true
        };
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
        airPollutionInfo.Should().NotBeNull();

        var expectedAirPollutionInfo = AirPollutionInfos.GetTestAirPollutionInfo();
        airPollutionInfo.Should().BeEquivalentTo(expectedAirPollutionInfo);
    }
}
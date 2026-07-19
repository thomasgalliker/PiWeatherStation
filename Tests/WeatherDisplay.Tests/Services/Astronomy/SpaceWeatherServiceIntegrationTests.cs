using Microsoft.Extensions.Logging;
using WeatherDisplay.Services.Astronomy;
using WeatherDisplay.Tests.Logging;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Services.Astronomy
{
    public class SpaceWeatherServiceIntegrationTests
    {
        private readonly ILogger<SpaceWeatherService> logger;
        private readonly ITestOutputHelper testOutputHelper;

        public SpaceWeatherServiceIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            this.logger = new TestOutputHelperLogger<SpaceWeatherService>(testOutputHelper);
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task ShouldGetPlanetaryKIndexForecastAsync()
        {
            // Arrange
            ISpaceWeatherService spaceWeatherService = new SpaceWeatherService(this.logger);

            // Act
            var planetaryKIndexForecasts = await spaceWeatherService.GetPlanetaryKIndexForecastAsync();

            // Assert
            this.testOutputHelper.WriteLine(ObjectDumper.Dump(planetaryKIndexForecasts, DumpStyle.CSharp));

            planetaryKIndexForecasts.Should().NotBeNull();
            planetaryKIndexForecasts.Should().NotBeEmpty();
            planetaryKIndexForecasts[0].TimeTag.Kind.Should().Be(DateTimeKind.Utc);
            planetaryKIndexForecasts[0].KpIndex.Should().BeInRange(0, 9);
            planetaryKIndexForecasts.Should().OnlyContain(x => x.KpIndex >= 0 && x.KpIndex <= 9);
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
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
            planetaryKIndexForecasts.Should().HaveCount(81);
            planetaryKIndexForecasts[0].TimeTag.Kind.Should().Be(DateTimeKind.Utc);
            planetaryKIndexForecasts[0].KpIndex.Should().BeInRange(0, 5);

            var firstEstimated = planetaryKIndexForecasts.OrderBy(f => f.TimeTag).First(f => f.Observed == "estimated");
            firstEstimated.TimeTag.Should().BeBefore(DateTime.UtcNow);
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Services.Wiewarm;
using WeatherDisplay.Tests.Logging;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Services.Wiewarm
{
    public class WiewarmServiceIntegrationTests
    {
        private readonly ILogger<WiewarmService> logger;
        private readonly ITestOutputHelper testOutputHelper;
        private readonly DumpOptions dumpOptions;

        public WiewarmServiceIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            this.logger = new TestOutputHelperLogger<WiewarmService>(testOutputHelper);
            this.testOutputHelper = testOutputHelper;

            this.dumpOptions = new DumpOptions
            {
                DumpStyle = DumpStyle.CSharp,
                SetPropertiesOnly = true
            };

            this.dumpOptions.CustomInstanceFormatters.AddFormatter<Temperature>(t => $"new Temperature({t.Value}, {nameof(TemperatureUnit)}.{t.Unit})");
            this.dumpOptions.CustomInstanceFormatters.AddFormatter<Pressure>(p => $"new Pressure({p.Value})");
            this.dumpOptions.CustomInstanceFormatters.AddFormatter<Humidity>(h => $"new Humidity({h.Value})");
            this.dumpOptions.CustomInstanceFormatters.AddFormatter<UVIndex>(uvi => $"new UVIndex({uvi.Value})");
        }

        [Fact]
        public async Task ShouldSearchBadAsync()
        {
            // Arrange
            var search = "Bern";

            IWiewarmService wiewarmService = new WiewarmService(this.logger);

            // Act
            var baths = await wiewarmService.SearchBathsAsync(search);

            // Assert
            this.testOutputHelper.WriteLine(ObjectDumper.Dump(baths, this.dumpOptions));

            baths.Should().NotBeNull();
            baths.Count().Should().Be(2);
        }
    }
}
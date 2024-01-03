using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using UnitsNet;
using UnitsNet.Units;
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
        }

        [Fact]
        public async Task ShoulGetBathById_Bern()
        {
            // Arrange
            var bathid = 17;

            IWiewarmService wiewarmService = new WiewarmService(this.logger);

            // Act
            var bath = await wiewarmService.GetBathByIdAsync(bathid);

            // Assert
            this.testOutputHelper.WriteLine(ObjectDumper.Dump(bath, this.dumpOptions));

            bath.Should().NotBeNull();
            bath.Id.Should().Be(bathid);
            bath.Name.Should().Be("Stadt Bern");
        }

        [Fact]
        public async Task ShouldSearchBad_Bern()
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
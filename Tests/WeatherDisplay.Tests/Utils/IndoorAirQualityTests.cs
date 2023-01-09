using FluentAssertions;
using UnitsNet;
using WeatherDisplay.Resources.Strings;
using WeatherDisplay.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Utils
{
    public class IndoorAirQualityTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public IndoorAirQualityTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Theory]
        [ClassData(typeof(IndoorAirQualityTestData))]
        public void ShouldCalculateIAQ(Temperature temperature, RelativeHumidity humidity, ElectricResistance gasResistance, int expectedIAQ)
        {
            // Act
            var iaq = IndoorAirQuality.CalculateIAQ(humidity, gasResistance);

            // Assert
            var iaqText = IndoorAirQuality.GetIAQRange(iaq);
            this.testOutputHelper.WriteLine($"{temperature} / {humidity} / {gasResistance} --> {iaq} ({iaqText})");
            iaq.Value.Should().Be(expectedIAQ);
        }

        public class IndoorAirQualityTestData : TheoryData<Temperature, RelativeHumidity, ElectricResistance, int>
        {
            public IndoorAirQualityTestData()
            {
                this.Add(Temperature.FromDegreesCelsius(20), RelativeHumidity.FromPercent(40), ElectricResistance.FromKiloohms(5), 25);
                this.Add(Temperature.FromDegreesCelsius(20), RelativeHumidity.FromPercent(40), ElectricResistance.FromKiloohms(50), 100);
                
                this.Add(Temperature.FromDegreesCelsius(20), RelativeHumidity.FromPercent(50), ElectricResistance.FromKiloohms(30), 62);
            }
        }

        [Theory]
        [ClassData(typeof(IndoorAirQualityRangeTestData))]
        public void ShouldGetIAQRange(Ratio iaq, string expectedOutput)
        {
            // Act
            var output = IndoorAirQuality.GetIAQRange(iaq);

            // Assert
            this.testOutputHelper.WriteLine($"{iaq} --> {output}");
            output.Should().Be(expectedOutput);
        }

        public class IndoorAirQualityRangeTestData : TheoryData<Ratio, string>
        {
            public IndoorAirQualityRangeTestData()
            {
                this.Add(Ratio.FromPercent(100), IndoorAirQualityStrings.Good);
                this.Add(Ratio.FromPercent(90), IndoorAirQualityStrings.Good);
                this.Add(Ratio.FromPercent(89), IndoorAirQualityStrings.Moderate);
                this.Add(Ratio.FromPercent(80), IndoorAirQualityStrings.Moderate);
                this.Add(Ratio.FromPercent(79), IndoorAirQualityStrings.UnhealthyForSensitiveGroups);
                this.Add(Ratio.FromPercent(70), IndoorAirQualityStrings.UnhealthyForSensitiveGroups);
                this.Add(Ratio.FromPercent(69), IndoorAirQualityStrings.Unhealthy);
                this.Add(Ratio.FromPercent(60), IndoorAirQualityStrings.Unhealthy);
                this.Add(Ratio.FromPercent(59), IndoorAirQualityStrings.VeryUnhealthy);
                this.Add(Ratio.FromPercent(40), IndoorAirQualityStrings.VeryUnhealthy);
                this.Add(Ratio.FromPercent(39), IndoorAirQualityStrings.Hazardous);
                this.Add(Ratio.FromPercent(0), IndoorAirQualityStrings.Hazardous);
            }
        }
    }
}

using System;
using FluentAssertions;
using UnitsNet;
using UnitsNet.Units;
using WeatherDisplay.Utils;
using Xunit;

namespace WeatherDisplay.Tests.Utils
{
    [Collection("CultureCollection")]
    public class MeteoFormatterTests
    {
        [Theory]
        [ClassData(typeof(FormatTemperatureTestData))]
        [UseCulture("en-US")]
        public void ShouldFormatTemperature(Temperature? temperature, string expectedOutput)
        {
            // Act
            var output = MeteoFormatter.FormatTemperature(temperature);

            // Assert
            output.Should().Be(expectedOutput);
        }

        public class FormatTemperatureTestData : TheoryData<Temperature?, string>
        {
            public FormatTemperatureTestData()
            {
                // Null value
                this.Add(null, "-");

                // Round values from 1 to -1
                this.Add(Temperature.FromDegreesCelsius(0.9d), "1 °C");
                this.Add(Temperature.FromDegreesCelsius(0.44d), "0 °C");
                this.Add(Temperature.FromDegreesCelsius(0.1d), "0 °C");
                this.Add(Temperature.FromDegreesCelsius(-0.1d), "0 °C");
                this.Add(Temperature.FromDegreesCelsius(-0.5d), "-1 °C");
            }
        }

        [Theory]
        [ClassData(typeof(FormatWindTestData))]
        [UseCulture("en-US")]
        public void ShouldFormatWind(Speed? windSpeed, Angle? windDirection, string expectedOutput)
        {
            // Act
            var output = MeteoFormatter.FormatWind(windSpeed, windDirection);

            // Assert
            output.Should().Be(expectedOutput);
        }

        public class FormatWindTestData : TheoryData<Speed?, Angle?, string>
        {
            public FormatWindTestData()
            {
                this.Add(null, null, "-");
                this.Add(Speed.FromMetersPerSecond(0), null, "0 m/s");
                this.Add(Speed.FromMetersPerSecond(0.5), null, "< 1 m/s");
                this.Add(Speed.FromKilometersPerHour(0.5), Angle.FromDegrees(0), "< 1 km/h (North)");
                this.Add(Speed.FromKilometersPerHour(0), Angle.FromDegrees(270), "0 km/h (West)");
                this.Add(Speed.FromKilometersPerHour(10), Angle.FromDegrees(90), "10 km/h (East)");
            }
        }

        [Theory]
        [ClassData(typeof(FormatPrecipitationTestData))]
        [UseCulture("en-US")]
        public void ShouldFormatPrecipitation(Length? length, Ratio? pop, string expectedOutput)
        {
            // Act
            var output = MeteoFormatter.FormatPrecipitation(length, pop);

            // Assert
            output.Should().Be(expectedOutput);
        }

        public class FormatPrecipitationTestData : TheoryData<Length?, Ratio?, string>
        {
            public FormatPrecipitationTestData()
            {
                this.Add(null, null, "-");
                this.Add(Length.FromMillimeters(0), null, "0 mm");
                this.Add(Length.FromMillimeters(0), Ratio.FromPercent(50), "0 mm (50 %)");
                this.Add(Length.FromMillimeters(0.5), null, "< 1 mm");
                this.Add(Length.FromMillimeters(0.5), Ratio.FromPercent(1), "< 1 mm (1 %)");
                this.Add(Length.FromMillimeters(10), null, "10 mm");
                this.Add(Length.FromMillimeters(20), Ratio.FromPercent(100), "20 mm (100 %)");
            }
        }
    }
}

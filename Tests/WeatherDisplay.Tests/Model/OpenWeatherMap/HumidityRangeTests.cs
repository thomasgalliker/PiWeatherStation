using System;
using FluentAssertions;
using WeatherDisplay.Model.OpenWeatherMap;
using Xunit;

namespace WeatherDisplay.Tests.Model.OpenWeatherMap.Converters;

public class HumidityRangeTests
{
    [Theory]
    [ClassData(typeof(HumidityRangeTestData))]

    public void ShouldGetFromValue(int humidity, HumidityRange expectedHumidityRange)
    {
        // Act
        var humidityRange = HumidityRange.FromValue(humidity);

        // Assert
        humidityRange.Should().Be(expectedHumidityRange);
    }

    public class HumidityRangeTestData : TheoryData<int, HumidityRange>
    {
        public HumidityRangeTestData()
        {
            this.Add(0, HumidityRange.VeryDry);
            this.Add(30, HumidityRange.VeryDry);
            this.Add(31, HumidityRange.Dry);
            this.Add(39, HumidityRange.Dry);
            this.Add(40, HumidityRange.Average);
            this.Add(70, HumidityRange.Average);
            this.Add(71, HumidityRange.Moist);
            this.Add(79, HumidityRange.Moist);
            this.Add(80, HumidityRange.VeryMoist);
            this.Add(100, HumidityRange.VeryMoist);
        }
    }

    [Fact]
    public void ShouldThrowOutOfRangeException()
    {
        // Arrange
        var humidity = 120;

        // Act
        Action action = () => HumidityRange.FromValue(humidity);

        // Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [ClassData(typeof(HumidityRangeToStringTestData))]

    public void ShouldGetToString(HumidityRange humidityRange, string expectedStringOutput)
    {
        // Act
        var stringOutput = humidityRange.ToString();

        // Assert
        stringOutput.Should().Be(expectedStringOutput);
    }

    public class HumidityRangeToStringTestData : TheoryData<HumidityRange, string>
    {
        public HumidityRangeToStringTestData()
        {
            this.Add(HumidityRange.VeryDry, "Sehr trocken");
            this.Add(HumidityRange.Dry, "Trocken");
            this.Add(HumidityRange.Average, "OK");
            this.Add(HumidityRange.Moist, "Feucht");
            this.Add(HumidityRange.VeryMoist, "Sehr feucht");
        }
    }
}
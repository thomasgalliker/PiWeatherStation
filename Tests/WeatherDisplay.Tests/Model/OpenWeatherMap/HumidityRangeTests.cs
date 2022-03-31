using System;
using FluentAssertions;
using WeatherDisplay.Model.OpenWeatherMap;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Model.OpenWeatherMap.Converters;

public class HumidityRangeTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public HumidityRangeTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

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

    public void ShouldGetToString(HumidityRange humidityRange)
    {
        // Act
        var stringOutput = humidityRange.ToString();

        // Assert
        this.testOutputHelper.WriteLine($"stringOutput={stringOutput}");
        stringOutput.Should().NotBeNullOrEmpty();
    }

    public class HumidityRangeToStringTestData : TheoryData<HumidityRange>
    {
        public HumidityRangeToStringTestData()
        {
            foreach (var humidityRange in HumidityRange.All)
            {
                this.Add(humidityRange);
            }
        }
    }
}
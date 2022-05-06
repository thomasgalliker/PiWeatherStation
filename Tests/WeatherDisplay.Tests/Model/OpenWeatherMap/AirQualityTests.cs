using System;
using FluentAssertions;
using WeatherDisplay.Model.OpenWeatherMap;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Model.OpenWeatherMap;

public class AirQualityTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public AirQualityTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Theory]
    [ClassData(typeof(AirQualityTestData))]

    public void ShouldGetFromValue(int airQualityValue, AirQuality expectedAirQuality)
    {
        // Act
        var airQuality = AirQuality.FromValue(airQualityValue);

        // Assert
        airQuality.Should().Be(expectedAirQuality);
    }

    public class AirQualityTestData : TheoryData<int, AirQuality>
    {
        public AirQualityTestData()
        {
            this.Add(1, AirQuality.Good);
            this.Add(2, AirQuality.Fair);
            this.Add(3, AirQuality.Moderate);
            this.Add(4, AirQuality.Poor);
            this.Add(5, AirQuality.VeryPoor);
        }
    }

    [Fact]
    public void ShouldThrowOutOfRangeException()
    {
        // Arrange
        var airQualityValue = 6;

        // Act
        Action action = () => AirQuality.FromValue(airQualityValue);

        // Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [ClassData(typeof(AirQualityToStringTestData))]

    public void ShouldGetToString(AirQuality airQuality)
    {
        // Act
        var stringOutput = airQuality.ToString();

        // Assert
        this.testOutputHelper.WriteLine($"stringOutput={stringOutput}");
        stringOutput.Should().NotBeNullOrEmpty();
    }

    public class AirQualityToStringTestData : TheoryData<AirQuality>
    {
        public AirQualityToStringTestData()
        {
            foreach (var airQualityRange in AirQuality.All)
            {
                this.Add(airQualityRange);
            }
        }
    }
}
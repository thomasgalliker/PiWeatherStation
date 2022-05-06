using System;
using FluentAssertions;
using WeatherDisplay.Model.OpenWeatherMap;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Model.OpenWeatherMap.Converters;

public class UVIndexRangeTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public UVIndexRangeTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Theory]
    [ClassData(typeof(UVIndexRangeTestData))]

    public void ShouldGetFromValue(double uvIndex, UVIndexRange expectedUVIndexRange)
    {
        // Act
        var uvIndexRange = UVIndexRange.FromValue(uvIndex);

        // Assert
        uvIndexRange.Should().Be(expectedUVIndexRange);
    }

    public class UVIndexRangeTestData : TheoryData<double, UVIndexRange>
    {
        public UVIndexRangeTestData()
        {
            this.Add(0d, UVIndexRange.Low);
            this.Add(2.9999999999d, UVIndexRange.Low);
            this.Add(3d, UVIndexRange.Moderate);
            this.Add(5.9999999999d, UVIndexRange.Moderate);
            this.Add(6d, UVIndexRange.High);
            this.Add(7.9999999999d, UVIndexRange.High);
            this.Add(8d, UVIndexRange.VeryHigh);
            this.Add(10.9999999999d, UVIndexRange.VeryHigh);
            this.Add(11d, UVIndexRange.Extreme);
            this.Add(double.MaxValue, UVIndexRange.Extreme);
        }
    }

    [Fact]
    public void ShouldThrowOutOfRangeException()
    {
        // Arrange
        var uvIndex = -10d;

        // Act
        Action action = () => UVIndexRange.FromValue(uvIndex);

        // Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [ClassData(typeof(UVIndexRangeToStringTestData))]

    public void ShouldGetToString(UVIndexRange humidityRange)
    {
        // Act
        var stringOutput = humidityRange.ToString();

        // Assert
        this.testOutputHelper.WriteLine($"stringOutput={stringOutput}");
        stringOutput.Should().NotBeNullOrEmpty();
    }

    public class UVIndexRangeToStringTestData : TheoryData<UVIndexRange>
    {
        public UVIndexRangeToStringTestData()
        {
            foreach (var humidityRange in UVIndexRange.All)
            {
                this.Add(humidityRange);
            }
        }
    }
}
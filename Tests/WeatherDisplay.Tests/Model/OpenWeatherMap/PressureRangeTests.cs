using System;
using FluentAssertions;
using WeatherDisplay.Model.OpenWeatherMap;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Model.OpenWeatherMap.Converters;

public class PressureRangeTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public PressureRangeTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Theory]
    [ClassData(typeof(PressureRangeTestData))]

    public void ShouldGetFromValue(int pressure, PressureRange expectedPressureRange)
    {
        // Act
        var pressureRange = PressureRange.FromValue(pressure);

        // Assert
        pressureRange.Should().Be(expectedPressureRange);
    }

    public class PressureRangeTestData : TheoryData<int, PressureRange>
    {
        public PressureRangeTestData()
        {
            this.Add(0, PressureRange.VeryLow);
            this.Add(998, PressureRange.VeryLow);
            this.Add(999, PressureRange.Low);
            this.Add(1007, PressureRange.Low);
            this.Add(1008, PressureRange.Average);
            this.Add(1018, PressureRange.Average);
            this.Add(1019, PressureRange.High);
            this.Add(1027, PressureRange.High);
            this.Add(1028, PressureRange.VeryHigh);
            this.Add(1044, PressureRange.VeryHigh);
        }
    }

    [Fact]
    public void ShouldThrowOutOfRangeException()
    {
        // Arrange
        var pressure = -1;

        // Act
        Action action = () => PressureRange.FromValue(pressure);

        // Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [ClassData(typeof(PressureRangeToStringTestData))]

    public void ShouldGetToString(PressureRange pressureRange)
    {
        // Act
        var stringOutput = pressureRange.ToString();

        // Assert
        this.testOutputHelper.WriteLine($"stringOutput={stringOutput}");
        stringOutput.Should().NotBeNullOrEmpty();
    }

    public class PressureRangeToStringTestData : TheoryData<PressureRange>
    {
        public PressureRangeToStringTestData()
        {
            foreach (var pressureRange in PressureRange.All)
            {
                this.Add(pressureRange);
            }
        }
    }
}
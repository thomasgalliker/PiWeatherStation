using FluentAssertions;
using WeatherDisplay.Model.OpenWeatherMap;
using Xunit;

namespace WeatherDisplay.Tests.Model.OpenWeatherMap.Converters;

public class RangeTests
{
    [Theory]
    [ClassData(typeof(InRangeTestData))]

    public void ShouldCheckInRange(Range<double> range, double value, bool expectedInRange)
    {
        // Act
        var inRange = range.InRange(value);

        // Assert
        inRange.Should().Be(expectedInRange);
    }

    public class InRangeTestData : TheoryData<Range<double>, double, bool>
    {
        public InRangeTestData()
        {
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: true, maxInclusive: true), 0d, true);
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: true, maxInclusive: true), 1d, true);
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: true, maxInclusive: true), 9d, true);
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: true, maxInclusive: true), 10d, true);

            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: true, maxInclusive: true), -1d, false);
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: false, maxInclusive: true), 0d, false);
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: true, maxInclusive: false), 10d, false);
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: true, maxInclusive: true), 11d, false);
        }
    }

    [Theory]
    [ClassData(typeof(RangeToStringTestData))]

    public void ShouldGetToString(Range<double> range, string expectedStringOutput)
    {
        // Act
        var stringOutput = range.ToString();

        // Assert
        stringOutput.Should().Be(expectedStringOutput);
    }

    public class RangeToStringTestData : TheoryData<Range<double>, string>
    {
        public RangeToStringTestData()
        {
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: true, maxInclusive: true), "[0, 10]");
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: true, maxInclusive: false), "[0, 10)");
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: false, maxInclusive: true), "(0, 10]");
            this.Add(new Range<double>(min: 0d, max: 10d, minInclusive: false, maxInclusive: false), "(0, 10)");
        }
    }
}
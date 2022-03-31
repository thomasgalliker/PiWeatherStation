using System;
using FluentAssertions;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;
using Xunit;

namespace WeatherDisplay.Tests.Model.OpenWeatherMap.Converters;

public class EpochDateTimeConverterTests
{
    [Theory]
    [ClassData(typeof(EpochDateTimeConverterValidTestData))]
    public void ShouldConvert(string json, DateTime? expectedDateTime)
    {
        // Act
        var testObject = JsonConvert.DeserializeObject<EpochDateTimeTestObject>(json);
        
        // Assert
        testObject.DateTime.Should().Be(expectedDateTime);
    }

    public class EpochDateTimeConverterValidTestData : TheoryData<string, DateTime?>
    {
        public EpochDateTimeConverterValidTestData()
        {
            this.Add("{\"dt\":\"0\"}", new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            this.Add("{\"dt\":\"946684800000\"}", new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            this.Add("{\"dt\":\"1648443641000\"}", new DateTime(2022, 3, 28, 5, 0, 41, DateTimeKind.Utc));
        }
    }

    [Theory]
    [InlineData("{\"dt\":\"not-a-long-value\"}")]
    public void ShouldThrowFormatException(string json)
    {
        // Act
        Action action = () => JsonConvert.DeserializeObject<EpochDateTimeTestObject>(json);
        
        // Assert
        var exception = action.Should().Throw<FormatException>().Which;
        exception.Message.Should().Be("Input string was not in a correct format.");
    }

    private class EpochDateTimeTestObject
    {
        [JsonProperty("dt")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime? DateTime { get; set; }
    }
}
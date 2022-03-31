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
            this.Add("{\"dt\":\"1406080800\"}", new DateTime(2014, 7, 23, 2, 0, 0, DateTimeKind.Utc));
            this.Add("{\"dt\":\"1648710982\"}", new DateTime(2022, 3, 31, 7, 16, 22, DateTimeKind.Utc));
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
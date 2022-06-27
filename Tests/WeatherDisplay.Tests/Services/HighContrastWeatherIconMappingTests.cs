using System.Threading.Tasks;
using FluentAssertions;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Services.OpenWeatherMap;
using WeatherDisplay.Tests.Testdata;
using Xunit;

namespace WeatherDisplay.Tests
{
    public class HighContrastWeatherIconMappingTests
    {
        [Theory]
        [ClassData(typeof(WeatherConditionTestData))]
        public async Task ShouldGetIconAsync(WeatherCondition weatherCondition)
        {
            // Arrange
            IWeatherIconMapping weatherIconMapping = new HighContrastWeatherIconMapping();

            // Act
            var icon = await weatherIconMapping.GetIconAsync(weatherCondition);

            // Assert
            icon.Should().NotBeNull();
        }

        public class WeatherConditionTestData : TheoryData<WeatherCondition>
        {
            public WeatherConditionTestData()
            {
                foreach (var weatherCondition in WeatherConditions.GetTestWeatherConditions())
                {
                    this.Add(weatherCondition);
                }
            }
        }
    }
}
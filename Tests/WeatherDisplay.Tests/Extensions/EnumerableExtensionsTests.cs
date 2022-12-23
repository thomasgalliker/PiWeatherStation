using FluentAssertions;
using WeatherDisplay.Extensions;
using Xunit;

namespace WeatherDisplay.Tests.Extensions
{
    public class EnumerableExtensionsTests
    {
        [Theory]
        [InlineData("item1", null, "item2")]
        [InlineData("item2", null, "item3")]
        [InlineData("item3", null, "item1")]
        [InlineData(null, null, null)]
        [InlineData(null, "item1", "item1")]
        [InlineData(null, "item99", "item99")]
        [InlineData("", null, null)]
        [InlineData("item99", null, null)]
        public void ShouldGetNextElement(string currentItem, string defaultValue, string expectedNextItem)
        {
            // Arrange
            var array = new[] { "item1", "item2", "item3" };

            // Act
            var next = array.GetNextElement(currentItem, defaultValue);

            // Assert
            next.Should().Be(expectedNextItem);
        }
    }
}

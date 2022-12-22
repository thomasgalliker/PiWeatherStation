using FluentAssertions;
using WeatherDisplay.Extensions;
using Xunit;

namespace WeatherDisplay.Tests.Extensions
{
    public class EnumerableExtensionsTests
    {
        [Theory]
        [InlineData("item1", "item2")]
        [InlineData("item2", "item3")]
        [InlineData("item3", "item1")]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("item99", null)]
        public void ShouldGetNextElement(string currentItem, string expectedNextItem)
        {
            // Arrange
            var array = new[] { "item1", "item2", "item3" };

            // Act
            var next = array.GetNextElement(currentItem);

            // Assert
            next.Should().Be(expectedNextItem);
        }
    }
}

using System;
using DisplayService.Services;
using FluentAssertions;
using Moq.AutoMock;
using Xunit;

namespace DisplayService.Tests.Services
{
    public class DateTimeFileNameRandomizerTests
    {
        private readonly AutoMocker autoMocker;

        public DateTimeFileNameRandomizerTests()
        {
            this.autoMocker = new AutoMocker();

            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.SetupGet(r => r.UtcNow)
                .Returns(new DateTime(2001, 2, 3, 11, 12, 14, 567, DateTimeKind.Utc));
        }

        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        public void ShouldThrowArgumentException(string path)
        {
            // Arrange
            IFileNameRandomizer fileNameRandomizer = this.autoMocker.CreateInstance<DateTimeFileNameRandomizer>();

            // Act
            Action action = () => fileNameRandomizer.Next(path);

            // Assert
            var ex = action.Should().ThrowExactly<ArgumentNullException>().Which;
            ex.ParamName.Should().Be("path");
        }

        [Theory]
        [InlineData("filename", "filename-2001-02-03-11-12-14-567")]
        [InlineData("filename.jpg", "filename-2001-02-03-11-12-14-567.jpg")]
        [InlineData(@"\some\path\to\filename.jpg", @"\some\path\to\filename-2001-02-03-11-12-14-567.jpg")]
        public void ShouldGetNextFileName(string path, string expectedPath)
        {
            // Arrange
            IFileNameRandomizer fileNameRandomizer = this.autoMocker.CreateInstance<DateTimeFileNameRandomizer>();

            // Act
            var randomPath = fileNameRandomizer.Next(path);

            // Assert
            randomPath.Should().Be(expectedPath);
        }
    }
}

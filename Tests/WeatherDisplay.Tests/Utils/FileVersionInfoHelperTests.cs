using WeatherDisplay.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Utils
{
    public class FileVersionInfoHelperTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public FileVersionInfoHelperTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldGetProductVersion_WithGitHash()
        {
            // Act
            var productVersion = FileVersionInfoHelper.GetProductVersion(displayGitHash: true);

            // Assert
            this.testOutputHelper.WriteLine($"productVersion={productVersion}");

            productVersion.Should().NotBeNullOrEmpty();
            productVersion.Should().Contain("(").And.Contain(")");
        }

        [Fact]
        public void ShouldGetProductVersion_WithoutGitHash()
        {
            // Act
            var productVersion = FileVersionInfoHelper.GetProductVersion(displayGitHash: false);

            // Assert
            this.testOutputHelper.WriteLine($"productVersion={productVersion}");

            productVersion.Should().NotBeNullOrEmpty();
            productVersion.Should().NotContain("(").And.NotContain(")");
        }
    }
}

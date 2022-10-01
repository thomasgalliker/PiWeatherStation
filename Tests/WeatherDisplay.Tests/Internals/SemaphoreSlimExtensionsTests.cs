using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using WeatherDisplay.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Internals
{
    public class SemaphoreSlimExtensionsTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public SemaphoreSlimExtensionsTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task ShouldExecuteOnceAsync()
        {
            // Arrange
            var counter = 0;
            var parallelTasks = 100;
            var semaphoreSlim = new SemaphoreSlim(1, 1);
            Task action() => Task.Run(() =>
            {
                counter++;
                this.testOutputHelper.WriteLine($"Run: counter={counter}");
                return counter;
            });

            // Act
            var tasks = Enumerable.Range(1, parallelTasks).Select(i => semaphoreSlim.ExecuteOnceAsync(action));
            await Task.WhenAll(tasks);

            // Assert
            counter.Should().Be(1);
        }
    }
}
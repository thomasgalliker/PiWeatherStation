using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using WeatherDisplay.Internals;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Internals
{
    public class SyncHelperTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public SyncHelperTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task ShouldRunOnceAsync_WithoutReturnValue()
        {
            // Arrange
            var counter = 0;
            var parallelTasks = 100000;
            var syncHelper = new SyncHelper();

            Task task(int id)
            {
                return Task.Run(async () =>
                {
                    // Simulate a long running task here
                    await Task.Delay(200);

                    // Access a shared resource, variable counter
                    Interlocked.Increment(ref counter);
                    this.testOutputHelper.WriteLine($"Run #{id}: \t\tcounter={counter}");
                    return counter;
                });
            }

            // Act
            var tasks = Enumerable.Range(1, parallelTasks)
                .Select(id => syncHelper.RunOnceAsync(() => task(id)))
                .ToList();
            await Task.WhenAll(tasks);

            // Assert
            counter.Should().Be(1);
        }

        [Fact]
        public async Task ShouldRunOnceAsync_WithReturnValue()
        {
            // Arrange
            var counter = 0;
            var parallelTasks = 100000;
            var syncHelper = new SyncHelper();

            Task<int> task(int id)
            {
                return Task.Run(async () =>
                {
                    // Simulate a long running task here
                    await Task.Delay(10);

                    // Access a shared resource, variable counter
                    Interlocked.Increment(ref counter);
                    this.testOutputHelper.WriteLine($"Run #{id}: \t\tcounter={counter}");
                    return counter;
                });
            }

            // Act
            var tasks = Enumerable.Range(1, parallelTasks)
                .Select(id => syncHelper.RunOnceAsync(() => task(id)))
                .ToList();
            var results = await Task.WhenAll(tasks);

            // Assert
            //counter.Should().Be(1);
            results.Should().HaveCount(parallelTasks);
            results.Should().AllSatisfy(i => i.Should().Be(1));
        }
    }
}
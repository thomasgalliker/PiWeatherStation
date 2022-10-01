using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WeatherDisplay.Internals;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Internals
{
    public class AsyncLockTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public AsyncLockTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldReturnCompletedTask_IfLockIsFree()
        {
            // Arrange
            var asyncLock = new AsyncLock();

            // Act
            var releaserTask = asyncLock.LockAsync();

            // Assert
            releaserTask.IsCompleted.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnIncompletedTask_IfLockIsAlreadyTaken()
        {
            // Arrange
            var asyncLock = new AsyncLock();
            await asyncLock.LockAsync().ConfigureAwait(false);

            // Act
            var releaserTask2 = asyncLock.LockAsync();

            // Assert
            releaserTask2.IsCompleted.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldLockMultipleTimesSequentially()
        {
            // Arrange
            var result = 0;
            var _lock = new AsyncLock();

            // Act
            using (await _lock.LockAsync())
            {
                this.testOutputHelper.WriteLine("Locked");
                result += 1;
            }
            using (await _lock.LockAsync())
            {
                this.testOutputHelper.WriteLine("Locked again");
                result += 2;
            }

            // Assert
            result.Should().Be(3);
        }

        [Fact]
        public async Task ShouldLockParallelTasks()
        {
            // Arrange
            var parallelTasks = 1000;
            var result = 0;

            var asyncLock = new AsyncLock();

            // Act
            var tasks = Enumerable.Range(1, parallelTasks)
                .Select(i => Task.Run(async () =>
                {
                    using (await asyncLock.LockAsync())
                    {
                        result++;
                    }
                }));
            await Task.WhenAll(tasks);

            // Assert
            result.Should().Be(parallelTasks);
        }

        //[Fact]
        //public async Task ShouldRunOnceParallelTasks()
        //{
        //    // Arrange
        //    var parallelTasks = 100;
        //    var result = 0;

        //    var asyncLock = new AsyncLock();

        //    // Act
        //    var tasks = Enumerable.Range(1, parallelTasks)
        //        .Select(i => Task.Run(async () =>
        //        {
        //            await asyncLock.RunOnceAsync(async () =>
        //            {
        //                await Task.Delay(500).ConfigureAwait(false);
        //                result++;
        //                await Task.Delay(500).ConfigureAwait(false);
        //            });
        //        }));
        //    await Task.WhenAll(tasks);

        //    // Assert
        //    result.Should().Be(1);
        //}

        //[Fact]
        //public async Task Lock_GivenLockIsDisposedWhileAwaitingLock_ShouldThrowTaskCanceledException()
        //{
        //    // Arrange
        //    var sut = new AsyncLock();
        //    var gotLock = await sut.LockAsync().ConfigureAwait(false);
        //    var awaiting = sut.LockAsync();
        //    _ = Task.Run(async () => await sut.DisposeAsync().ConfigureAwait(false));

        //    // Act
        //    var exception = await Record.ExceptionAsync(() => awaiting).ConfigureAwait(false);

        //    // Assert
        //    exception.Should().BeOfType<TaskCanceledException>();

        //    // Annihilate

        //    gotLock.Dispose();
        //}

        //[Fact]
        //public async Task Lock_GivenLockIsTakenAndCancellationTokenIsActivated_ShouldThrowTaskCanceledException()
        //{
        //    // Arrange
        //    var cts = new CancellationTokenSource();
        //    var sut = new AsyncLock();
        //    var gotLock = await sut.LockAsync().ConfigureAwait(false);
        //    var awaiting = sut.LockAsync(cts.Token);

        //    // Act
        //    cts.Cancel();
        //    var exception = await Record.ExceptionAsync(() => awaiting).ConfigureAwait(false);

        //    // Assert
        //    exception.Should().BeOfType<TaskCanceledException>();

        //    // Annihilate
        //    cts.Dispose();
        //    gotLock.Dispose();

        //}

        //[Fact]
        //public async Task Dispose_GivenLockIsDisposedWhileItIsTaken_ShouldNotCompleteBeforeItIsReleased()
        //{
        //    // Arrange
        //    var sut = new AsyncLock();
        //    var gotLock = await sut.LockAsync().ConfigureAwait(false);
        //    var disposeTask = Task.Run(async () => await sut.DisposeAsync().ConfigureAwait(false));
        //    Assert.False(disposeTask.IsCompleted);

        //    // Act
        //    gotLock.Dispose();
        //    await disposeTask.ConfigureAwait(false);

        //    // Assert
        //    disposeTask.IsCompleted.Should().BeTrue();

        //    // Annihilate

        //}

        [Fact]
        public async Task ShouldReturnException()
        {
            // Arrange
            var asyncLock = new AsyncLock();

            // Act
            var exception = await Record.ExceptionAsync(() =>
            {
                using (asyncLock.LockAsync())
                {
                    throw new InvalidOperationException("Test");
                }
            });

            // Assert
            exception.Should().BeOfType<InvalidOperationException>();
        }
    }
}
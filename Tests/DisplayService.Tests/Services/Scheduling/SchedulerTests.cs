using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DisplayService.Services;
using DisplayService.Services.Scheduling;
using DisplayService.Tests.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace DisplayService.Tests.Services.Scheduling
{
    public class SchedulerTests
    {
        private readonly AutoMocker autoMocker;

        public SchedulerTests()
        {
            this.autoMocker = new AutoMocker();
        }

        [Fact]
        public async Task NoScheduledJob()
        {
            // Arrange
            var nextCount = 0;
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.Setup(mock => mock.Now)
                .Returns(new DateTime(2019, 11, 06, 14, 43, 59));

            var scheduler = this.autoMocker.CreateInstance<Scheduler>();
            scheduler.Next += (sender, args) => { nextCount++; };

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(1000))
            {
                await scheduler.StartAsync(cancellationTokenSource.Token);
            }

            // Assert
            nextCount.Should().Be(0);
        }

        [Fact]
        public async Task ScheduleJob()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.Setup(mock => mock.Now).Returns(new DateTime(2019, 11, 06, 14, 43, 59));
            var scheduler = this.autoMocker.CreateInstance<Scheduler>();

            var actionObject = new TestObject();
            scheduler.ScheduleTask("* * * * * *", (cancellationToken) =>
            {
                actionObject.DoWork();
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(1100))
            {
                await scheduler.StartAsync(cancellationTokenSource.Token);
            }

            // Arrange
            actionObject.Modified.Should().BeTrue();
        }

        [Fact]
        public async Task ScheduleAsyncJob()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.Setup(mock => mock.Now).Returns(new DateTime(2019, 11, 06, 14, 43, 59));
            var scheduler = this.autoMocker.CreateInstance<Scheduler>();

            var actionObject = new TestObject();
            scheduler.ScheduleTask("* * * * * *", async (cancellationToken) =>
            {
                await actionObject.DoWorkAsync();
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(1100))
            {
                await scheduler.StartAsync(cancellationTokenSource.Token);
            }

            // Arrange
            actionObject.Modified.Should().BeTrue();
        }

        [Fact]
        public async Task ScheduleMultipleJobs()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.Setup(mock => mock.Now).Returns(new DateTime(2019, 11, 06, 14, 43, 59));
            var scheduler = this.autoMocker.CreateInstance<Scheduler>();

            var actionObject = new TestObject();
            scheduler.ScheduleTask("* * * * * *", (cancellationToken) =>
            {
                actionObject.DoWork();
            });

            var actionObject2 = new TestObject();
            scheduler.ScheduleTask("* * * * *", (cancellationToken) =>
            {
                actionObject2.DoWork();
            });

            var actionObject3 = new TestObject();
            scheduler.ScheduleTask("* * * * *", async (cancellationToken) =>
            {
                await actionObject3.DoWorkAsync();
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(1100))
            {
                await scheduler.StartAsync(cancellationTokenSource.Token);
            }

            // Arrange
            actionObject.Modified.Should().BeTrue();
            actionObject2.Modified.Should().BeTrue();
            actionObject3.Modified.Should().BeTrue();
        }

        [Fact]
        public async Task ScheduleJobButLastRunTimeIsInPast()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.Setup(mock => mock.Now).Returns(new DateTime(2019, 11, 06, 14, 43, 59));
            var scheduler = this.autoMocker.CreateInstance<Scheduler>();

            var actionObject = new TestObject();
            scheduler.ScheduleTask("* * * * * 2018", (cancellationToken) =>
            {
                actionObject.DoWork();
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(1000))
            {
                await scheduler.StartAsync(cancellationTokenSource.Token);
            }

            // Arrange
            Assert.False(actionObject.Modified);
        }

        [Fact]
        public async Task ScheduleJobThatShouldRunInNextMinuteAndNotMore()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.SetupSequence(mock => mock.Now)
                .Returns(new DateTime(2019, 11, 06, 14, 43, 58))
                .Returns(new DateTime(2019, 11, 06, 14, 43, 59))
                .Returns(new DateTime(2019, 11, 06, 14, 44, 01));
            var scheduler = this.autoMocker.CreateInstance<Scheduler>();

            var actionObject = new TestObject();
            scheduler.ScheduleTask("44 14 * * * 2019", (cancellationToken) =>
            {
                actionObject.DoWork();
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(1100))
            {
                await scheduler.StartAsync(cancellationTokenSource.Token);
            }

            // Assert
            Assert.Equal(1, actionObject.ModifiedCount);
        }

        [Fact]
        public async Task ScheduleJobThatShouldRunInNextMinuteButCancelBeforeThatSoNoExecutionHaveOccured()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.SetupSequence(mock => mock.Now)
                .Returns(new DateTime(2019, 11, 06, 14, 43, 48))
                .Returns(new DateTime(2019, 11, 06, 14, 43, 49));
            var scheduler = this.autoMocker.CreateInstance<Scheduler>();

            var actionObject = new TestObject();
            scheduler.ScheduleTask("44 14 * * * 2019", (cancellationToken) =>
            {
                actionObject.DoWork();
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(1))
            {
                await scheduler.StartAsync(cancellationTokenSource.Token);
            }

            // Assert
            Assert.False(actionObject.Modified);
        }

        [Fact]
        public async Task ScheduleJobThatWillTakeMoreThanAMinuteToRunAndLogWarning()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.SetupSequence(mock => mock.Now)
                .Returns(new DateTime(2019, 11, 06, 14, 43, 58))
                .Returns(new DateTime(2019, 11, 06, 14, 43, 59))
                .Returns(new DateTime(2019, 11, 06, 14, 44, 01))
                .Returns(new DateTime(2019, 11, 06, 14, 45, 02));

            var logger = new Mock<ILogger<Scheduler>>();

            var scheduler = new Scheduler(logger.Object, dateTimeMock.Object);

            var actionObject = new TestObject();
            scheduler.ScheduleTask("44 14 * * * 2019", (cancellationToken) =>
            {
                actionObject.DoWork();
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(2000))
            {
                await scheduler.StartAsync(cancellationTokenSource.Token);
            }

            // Assert
            logger.Verify(x => x.Log(LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Execution took more than one minute", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task ScheduleJobThatShouldRunInNextMinuteButChangeThatBeforeThatSoNoExecutionHaveOccured()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.SetupSequence(mock => mock.Now)
                .Returns(new DateTime(2019, 11, 06, 14, 43, 58))
                .Returns(new DateTime(2019, 11, 06, 14, 43, 59));
            var scheduler = this.autoMocker.CreateInstance<Scheduler>();

            var actionObject = new TestObject();
            var id = scheduler.ScheduleTask("44 14 * * * 2019", (cancellationToken) =>
            {
                actionObject.DoWork();
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(1100))
            {
                var task = Task.Run(async () =>
                {
                    await scheduler.StartAsync(cancellationTokenSource.Token);
                });

                scheduler.ChangeScheduleAndResetScheduler(id, "50 14 * * * 2019");

                await task;
            }

            // Assert
            Assert.False(actionObject.Modified);
        }

        [Fact]
        public async Task ScheduleJobThatShouldRunInNextMinuteButChangeThatBeforeThatSoNoExecutionHaveOccuredWithExternalId()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.SetupSequence(mock => mock.Now)
                .Returns(new DateTime(2019, 11, 06, 14, 43, 58))
                .Returns(new DateTime(2019, 11, 06, 14, 43, 59));
            var scheduler = this.autoMocker.CreateInstance<Scheduler>();

            var id = Guid.NewGuid();
            var actionObject = new TestObject();
            scheduler.ScheduleTask(id, "44 14 * * * 2019", (cancellationToken) =>
            {
                actionObject.DoWork();
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(1100))
            {
                var task = Task.Run(async () =>
                {
                    await scheduler.StartAsync(cancellationTokenSource.Token);
                });

                scheduler.ChangeScheduleAndResetScheduler(id, "50 14 * * * 2019");

                await task;
            }

            // Arrange
            Assert.False(actionObject.Modified);
        }

        [Fact]
        public async Task ScheduleJobThatShouldRunInNextMinuteButStopSchedulerBeforeThatSoNoExecutionHaveOccured()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.SetupSequence(mock => mock.Now)
                .Returns(new DateTime(2019, 11, 06, 14, 43, 58))
                .Returns(new DateTime(2019, 11, 06, 14, 43, 59));
            var scheduler = this.autoMocker.CreateInstance<Scheduler>();

            var internalStopInvoked = false;
            var actionObject = new TestObject();
            var id = scheduler.ScheduleTask("44 14 * * * 2019", async (cancellationToken) =>
            {
                var continueExecutionAfterDelay = await Task.Delay(10000, cancellationToken).ContinueWith(task =>
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return false;
                    }
                    return true;
                });

                if (!continueExecutionAfterDelay)
                {
                    internalStopInvoked = true;
                    return;
                }

                actionObject.DoWork();
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(3100))
            {
                var task = Task.Run(async () =>
                {
                    await scheduler.StartAsync(cancellationTokenSource.Token);
                });

                await Task.Delay(2000);

                scheduler.Stop();

                await task;
            }

            // Arrange
            Assert.True(internalStopInvoked);
            Assert.False(actionObject.Modified);
        }

        [Fact]
        public async Task ScheduleAsyncJobsAndOneWillFailTheOtherWillStillRunAndLogWillBeCreated()
        {
            // Arrange
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.Setup(mock => mock.Now).Returns(new DateTime(2019, 11, 06, 14, 43, 59));
            var logger = new Mock<ILogger<Scheduler>>();

            var scheduler = new Scheduler(logger.Object, dateTimeMock.Object);

            var actionObject = new TestObject();
            scheduler.ScheduleTask("* * * * * *", async (cancellationToken) =>
            {
                await actionObject.DoWorkAsync();
            });

            scheduler.ScheduleTask("* * * * * *", (cancellationToken) =>
            {
                throw new Exception("Fail!!");
            });

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(1100))
            {
                await scheduler.StartAsync(cancellationTokenSource.Token);
            }

            // Arrange
            actionObject.Modified.Should().BeTrue();
            logger.Verify(x => x.Log(LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Failed to execute action", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }

    }
}

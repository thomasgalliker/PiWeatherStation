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
using NCrontab;
using Xunit;
using Xunit.Abstractions;

namespace DisplayService.Tests.Services.Scheduling
{
    public class SchedulerTests
    {
        private readonly AutoMocker autoMocker;

        public SchedulerTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();
            this.autoMocker.Use<ILogger<Scheduler>>(new TestOutputHelperLogger<Scheduler>(testOutputHelper));
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
            scheduler.ScheduleTask("* * * * *", (cancellationToken) =>
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
            scheduler.ScheduleTask("* * * * *", async (cancellationToken) =>
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
            scheduler.ScheduleTask("* * * * *", (cancellationToken) =>
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
        public async Task ScheduleMultipleJobs_2()
        {
            // Arrange
            var referenceDate = new DateTime(2000, 1, 1, 22, 59, 58);

            var clockQueue = new DateTimeGenerator(
                referenceDate,
                new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(58),
                });

            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.SetupSequence(d => d.Now, referenceDate, (n) => clockQueue.GetNext());

            var tcs = new TaskCompletionSource();
            var recordedNextEvents = new List<ScheduledEventArgs>();

            var scheduler = this.autoMocker.CreateInstance<Scheduler>();
            scheduler.Next += (s, e) => { recordedNextEvents.Add(e); if (recordedNextEvents.Count == 2) { tcs.SetResult(); } };

            var testObjectDaily = new TestObject();
            scheduler.ScheduleTask("0 0 * * *", _ => testObjectDaily.DoWork());

            var testObjectHourly = new TestObject();
            scheduler.ScheduleTask("0 * * * *", _ => testObjectHourly.DoWork());

            // Act
            using (var cancellationTokenSource = new CancellationTokenSource(60000))
            {
                scheduler.Start(cancellationTokenSource.Token);
                await tcs.Task;
            }

            // Arrange
            recordedNextEvents.Should().HaveCount(2);
            recordedNextEvents[0].SignalTime.Should().Be(new DateTime(2000, 1, 1, 23, 00, 00));
            recordedNextEvents[1].SignalTime.Should().Be(new DateTime(2000, 1, 2, 00, 00, 00));
            testObjectHourly.ModifiedCount.Should().Be(2);
            testObjectDaily.ModifiedCount.Should().Be(1);
        }

        [Fact(Skip = "to be fixed")]
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
            scheduler.ScheduleTask("44 14 * * *", (cancellationToken) =>
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

        [Fact(Skip = "to be fixed")]
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

                scheduler.ChangeScheduleAndResetScheduler(id, CrontabSchedule.Parse("50 14 * * * 2019"));

                await task;
            }

            // Assert
            Assert.False(actionObject.Modified);
        }

        [Fact(Skip = "to be fixed")]
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

                scheduler.ChangeScheduleAndResetScheduler(id, CrontabSchedule.Parse("50 14 * * * 2019"));

                await task;
            }

            // Arrange
            Assert.False(actionObject.Modified);
        }

        [Fact(Skip = "to be fixed")]
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
            scheduler.ScheduleTask(CrontabSchedule.Parse("* * * * *"), async (cancellationToken) =>
            {
                await actionObject.DoWorkAsync();
            });

            scheduler.ScheduleTask(CrontabSchedule.Parse("* * * * *"), (cancellationToken) =>
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

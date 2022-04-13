using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Services;
using DisplayService.Tests.Extensions;
using DisplayService.Services.Scheduling;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using NCrontab;
using NCrontab.Scheduler;
using NCrontab.Scheduler.Internals;
using Xunit;
using Xunit.Abstractions;

namespace DisplayService.Tests.Services
{
    public class DisplayManagerIntegrationTests
    {
        private readonly AutoMocker autoMocker;

        public DisplayManagerIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();

            var renderServiceMock = this.autoMocker.GetMock<IRenderService>();
            renderServiceMock.Setup(r => r.GetScreen())
                .Returns(new MemoryStream());

            this.autoMocker.Use<IScheduler>(this.autoMocker.CreateInstance<Scheduler>());

            this.autoMocker.Use<ILogger<Scheduler>>(new TestOutputHelperLogger<Scheduler>(testOutputHelper));
            this.autoMocker.Use<ILogger<DisplayManager>>(new TestOutputHelperLogger<DisplayManager>(testOutputHelper));
        }

        [Fact]
        public async Task ShouldAddRenderActions_WithSchedule()
        {
            // Arrange
            var referenceDate = new DateTime(2000, 1, 1, 20, 59, 58);

            var clockQueue = new DateTimeGenerator(
                referenceDate,
                new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(58),
                });

            var dateTimeMock = this.autoMocker.GetMock<DisplayService.Services.IDateTime>();
            dateTimeMock.SetupSequence(d => d.Now, referenceDate, (n) => clockQueue.GetNext());

            var displayMock = this.autoMocker.GetMock<IDisplay>();
            var renderServiceMock = this.autoMocker.GetMock<IRenderService>();

            var cronSchedule = CrontabSchedule.Parse("0,1,2 * * * *");

            var tcs = new TaskCompletionSource();
            var recordedNextEvents = new List<ScheduledEventArgs>();

            var scheduler = this.autoMocker.Get<IScheduler>();
            scheduler.Next += (s, e) => { recordedNextEvents.Add(e); if (recordedNextEvents.Count == 2) { tcs.SetResult(); } };

            var displayManager = this.autoMocker.CreateInstance<DisplayManager>();

            // Act
            displayManager.AddRenderActions(() => new List<IRenderAction> { new RenderActions.Text { Value = "Test 1" } }, cronSchedule);
            displayManager.AddRenderActions(() => new List<IRenderAction> { new RenderActions.Text { Value = "Test 2" } }, cronSchedule);

            using (var cancellationTokenSource = new CancellationTokenSource(60000))
            {
                await displayManager.StartAsync(cancellationTokenSource.Token);
                await tcs.Task;
            }

            // Assert
            dateTimeMock.Verify(d => d.Now, Times.Exactly(7));
            renderServiceMock.Verify(r => r.Clear(), Times.Exactly(1));
            renderServiceMock.Verify(r => r.GetScreen(), Times.Exactly(3));
            renderServiceMock.Verify(r => r.Text(It.Is<RenderActions.Text>(t => t.Value == "Test 1")), Times.Exactly(3));
            renderServiceMock.Verify(r => r.Text(It.Is<RenderActions.Text>(t => t.Value == "Test 2")), Times.Exactly(3));
            renderServiceMock.VerifyNoOtherCalls();
            displayMock.Verify(d => d.DisplayImage(It.IsAny<Stream>()), Times.Exactly(3));
            displayMock.VerifyNoOtherCalls();

        }
    }
}
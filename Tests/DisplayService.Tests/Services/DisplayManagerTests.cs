using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Services;
using DisplayService.Services.Scheduling;
using DisplayService.Tests.Extensions;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace DisplayService.Tests.Services
{
    public class DisplayManagerTests
    {
        private readonly AutoMocker autoMocker;

        public DisplayManagerTests()
        {
            this.autoMocker = new AutoMocker();

            var renderServiceMock = this.autoMocker.GetMock<IRenderService>();
            renderServiceMock.Setup(r => r.GetScreen())
                .Returns(new MemoryStream());

            IScheduler scheduler = this.autoMocker.CreateInstance<Scheduler>();
            this.autoMocker.Use(scheduler);
        }

        [Fact]
        public void ShouldClearAsync()
        {
            // Arrange
            var displayMock = this.autoMocker.GetMock<IDisplay>();
            var renderServiceMock = this.autoMocker.GetMock<IRenderService>();

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.AddRenderAction(() => new RenderActions.Text { Value = "Test" });
            displayManager.StartAsync();

            // Act
            displayManager.ClearAsync();
            displayManager.StartAsync();

            // Assert
            displayMock.Verify(d => d.DisplayImage(It.IsAny<Stream>()), Times.Exactly(3));
            renderServiceMock.Verify(r => r.Clear(), Times.Exactly(3));
            renderServiceMock.Verify(r => r.Text(It.IsAny<RenderActions.Text>()), Times.Exactly(2));
        }

        [Fact]
        public void ShouldResetAsync()
        {
            // Arrange
            var displayMock = this.autoMocker.GetMock<IDisplay>();
            var renderServiceMock = this.autoMocker.GetMock<IRenderService>();

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.AddRenderAction(() => new RenderActions.Text { Value = "Test" });
            displayManager.StartAsync();

            // Act
            displayManager.ResetAsync();
            displayManager.StartAsync();

            // Assert
            displayMock.Verify(d => d.DisplayImage(It.IsAny<Stream>()), Times.Exactly(3));
            renderServiceMock.Verify(r => r.Clear(), Times.Exactly(3));
            renderServiceMock.Verify(r => r.Text(It.IsAny<RenderActions.Text>()), Times.Exactly(1));
        }

        [Fact]
        public void ShouldAddRenderAction()
        {
            // Arrange
            var displayMock = this.autoMocker.GetMock<IDisplay>();
            var renderServiceMock = this.autoMocker.GetMock<IRenderService>();

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();

            // Act
            displayManager.AddRenderAction(() => new RenderActions.Text { Value = "Test" });
            displayManager.StartAsync();

            // Assert
            displayMock.Verify(d => d.DisplayImage(It.IsAny<Stream>()), Times.Exactly(1));
            renderServiceMock.Verify(r => r.Text(It.Is<RenderActions.Text>(t => t.Value == "Test")), Times.Exactly(1));
        }

        [Fact]
        public void ShouldAddRenderActionsAsync()
        {
            // Arrange
            var displayMock = this.autoMocker.GetMock<IDisplay>();
            var renderServiceMock = this.autoMocker.GetMock<IRenderService>();

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();

            // Act
            displayManager.AddRenderActionsAsync(async () => new List<IRenderAction> { new RenderActions.Text { Value = "Test" } });
            displayManager.StartAsync();

            // Assert
            displayMock.Verify(d => d.DisplayImage(It.IsAny<Stream>()), Times.Exactly(1));
            renderServiceMock.Verify(r => r.Text(It.Is<RenderActions.Text>(t => t.Value == "Test")), Times.Exactly(1));
        }

        [Fact]
        public void ShouldAddRenderActions()
        {
            // Arrange
            var displayMock = this.autoMocker.GetMock<IDisplay>();
            var renderServiceMock = this.autoMocker.GetMock<IRenderService>();

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();

            // Act
            displayManager.AddRenderActions(() => new List<IRenderAction> { new RenderActions.Text { Value = "Test" } });
            displayManager.StartAsync();

            // Assert
            displayMock.Verify(d => d.DisplayImage(It.IsAny<Stream>()), Times.Exactly(1));
            renderServiceMock.Verify(r => r.Text(It.Is<RenderActions.Text>(t => t.Value == "Test")), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldAddRenderActions_WithSchedule()
        {
            // Arrange
            var referenceDate = new DateTime(2000, 1, 1, 0, 4, 55);
            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.SetupSequence(d => d.Now, referenceDate, n => n.AddSeconds(1));

            var displayMock = this.autoMocker.GetMock<IDisplay>();
            var renderServiceMock = this.autoMocker.GetMock<IRenderService>();

            var cronSchedule = "5,10 * * * * *";

            var displayManager = this.autoMocker.CreateInstance<DisplayManager>();

            // Act
            displayManager.AddRenderActions(() => new List<IRenderAction> { new RenderActions.Text { Value = "Test 1" } }, cronSchedule);
            displayManager.AddRenderActions(() => new List<IRenderAction> { new RenderActions.Text { Value = "Test 2" } }, cronSchedule);

            using (var cancellationTokenSource = new CancellationTokenSource(6000))
            {
                await displayManager.StartAsync(cancellationTokenSource.Token, awaitScheduler: true);
            }

            // Assert
            dateTimeMock.Verify(d => d.Now, Times.Exactly(6));
            renderServiceMock.Verify(r => r.Clear(), Times.Exactly(1));
            renderServiceMock.Verify(r => r.GetScreen(), Times.Exactly(2));
            renderServiceMock.Verify(r => r.Text(It.Is<RenderActions.Text>(t => t.Value == "Test 1")), Times.Exactly(2));
            renderServiceMock.Verify(r => r.Text(It.Is<RenderActions.Text>(t => t.Value == "Test 2")), Times.Exactly(2));
            renderServiceMock.VerifyNoOtherCalls();

        }
    }
}
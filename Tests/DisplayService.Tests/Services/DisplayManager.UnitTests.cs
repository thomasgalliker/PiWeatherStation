using System.Collections.Generic;
using System.IO;
using DisplayService.Model;
using DisplayService.Services;
using DisplayService.Tests.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using Xunit;
using Xunit.Abstractions;

namespace DisplayService.Tests.Services
{
    public class DisplayManagerUnitTests
    {
        private readonly AutoMocker autoMocker;

        public DisplayManagerUnitTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();

            var renderServiceMock = this.autoMocker.GetMock<IRenderService>();
            renderServiceMock.Setup(r => r.GetScreen())
                .Returns(new MemoryStream());

            this.autoMocker.Use<ILogger<DisplayManager>>(new TestOutputHelperLogger<DisplayManager>(testOutputHelper));
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
    }
}
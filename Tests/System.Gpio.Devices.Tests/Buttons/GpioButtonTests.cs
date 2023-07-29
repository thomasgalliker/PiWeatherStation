using System.Device.Buttons;
using System.Device.Gpio;
using FluentAssertions;
using Moq;
using Xunit;

namespace System.Gpio.Devices.Tests.Buttons
{
    public class GpioButtonTests
    {
        private const int ButtonPin = 5;

        private readonly Mock<IGpioController> gpioControllerMock;

        public GpioButtonTests()
        {
            this.gpioControllerMock = new Mock<IGpioController>();
            this.gpioControllerMock.Setup(g => g.IsPinModeSupported(ButtonPin, PinMode.InputPullUp))
                .Returns(true);
        }

        [Fact]
        public void ShouldSetIsHoldingEnabled_IfHoldingEventSubscribed()
        {
            // Arrange
            var button = new GpioButton(ButtonPin, isPullUp: true, gpio: this.gpioControllerMock.Object);

            // Act
            button.Holding += (s, e) => { };

            // Assert
            button.IsHoldingEnabled.Should().BeTrue();
        }

        [Fact]
        public void ShouldSetIsHoldingEnabled_IfHoldingEventUnsubscribed()
        {
            // Arrange
            var button = new GpioButton(ButtonPin, isPullUp: true, gpio: this.gpioControllerMock.Object);

            static void eventHandler1(object s, ButtonHoldingEventArgs e)
            { }
            static void eventHandler2(object s, ButtonHoldingEventArgs e)
            { }

            // Act
            button.Holding += eventHandler1;
            button.Holding += eventHandler2;
            button.Holding -= eventHandler1;
            button.Holding -= eventHandler2;

            // Assert
            button.IsHoldingEnabled.Should().BeFalse();
        }
    }
}

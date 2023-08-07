using System.Device.Buttons;
using System.Device.Gpio;
using System.Threading;
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
            this.gpioControllerMock.Setup(g => g.IsPinModeSupported(ButtonPin, PinMode.InputPullDown))
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

        [Theory]
        [InlineData(false, PinEventTypes.Falling, 0, 0)]
        [InlineData(false, PinEventTypes.Rising, 1, 0)]
        [InlineData(true, PinEventTypes.Falling, 1, 0)]
        [InlineData(true, PinEventTypes.Rising, 0, 0)]
        public void ShouldHandlePinStateChanged_OneWay(bool isPullUp, PinEventTypes pinEventTypes, int expectedButtonDownCounts, int expectedButtonUpCounts)
        {
            // Arrange
            var buttonDownCounter = 0;
            var buttonUpCounter = 0;

            PinChangeEventHandler pinChangeEventHandler = null;
            this.gpioControllerMock.Setup(c => c.RegisterCallbackForPinValueChangedEvent(ButtonPin, It.IsAny<PinEventTypes>(), It.IsAny<PinChangeEventHandler>()))
                .Callback((int _, PinEventTypes _, PinChangeEventHandler eventHandler) => { pinChangeEventHandler = eventHandler; });

            var button = new GpioButton(ButtonPin, isPullUp, gpio: this.gpioControllerMock.Object);

            button.ButtonDown += (sender, e) =>
            {
                Interlocked.Increment(ref buttonDownCounter);
            };
            button.ButtonUp += (sender, e) =>
            {
                Interlocked.Increment(ref buttonUpCounter);
            };

            // Act
            pinChangeEventHandler(button, new PinValueChangedEventArgs(pinEventTypes, ButtonPin));

            // Assert
            buttonDownCounter.Should().Be(expectedButtonDownCounts);
            buttonUpCounter.Should().Be(expectedButtonUpCounts);
        }

        [Theory]
        [InlineData(false, PinEventTypes.Falling, PinEventTypes.Rising, 1, 0)]
        [InlineData(false, PinEventTypes.Rising, PinEventTypes.Falling, 1, 1)]
        [InlineData(true, PinEventTypes.Falling, PinEventTypes.Rising, 1, 1)]
        [InlineData(true, PinEventTypes.Rising, PinEventTypes.Falling, 1, 0)]
        public void ShouldHandlePinStateChanged_TwoWay(bool isPullUp, PinEventTypes pinEventTypes1, PinEventTypes pinEventTypes2, int expectedButtonDownCounts, int expectedButtonUpCounts)
        {
            // Arrange
            var buttonDownCounter = 0;
            var buttonUpCounter = 0;

            PinChangeEventHandler pinChangeEventHandler = null;
            this.gpioControllerMock.Setup(c => c.RegisterCallbackForPinValueChangedEvent(ButtonPin, It.IsAny<PinEventTypes>(), It.IsAny<PinChangeEventHandler>()))
                .Callback((int _, PinEventTypes _, PinChangeEventHandler eventHandler) => { pinChangeEventHandler = eventHandler; });

            var button = new GpioButton(ButtonPin, isPullUp, gpio: this.gpioControllerMock.Object);

            button.ButtonDown += (sender, e) =>
            {
                Interlocked.Increment(ref buttonDownCounter);
            };
            button.ButtonUp += (sender, e) =>
            {
                Interlocked.Increment(ref buttonUpCounter);
            };

            // Act
            pinChangeEventHandler(button, new PinValueChangedEventArgs(pinEventTypes1, ButtonPin));
            pinChangeEventHandler(button, new PinValueChangedEventArgs(pinEventTypes2, ButtonPin));

            // Assert
            buttonDownCounter.Should().Be(expectedButtonDownCounts);
            buttonUpCounter.Should().Be(expectedButtonUpCounts);
        }
    }
}

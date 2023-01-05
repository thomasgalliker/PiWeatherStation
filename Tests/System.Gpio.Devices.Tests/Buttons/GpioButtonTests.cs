﻿using System.Device.Buttons;
using System.Device.Gpio;
using FluentAssertions;
using Moq;
using Xunit;

namespace System.Gpio.Devices.Tests.Buttons
{
    public class GpioButtonTests
    {
        [Fact]
        public void ShouldSetIsHoldingEnabled_IfHoldingEventSubscribed()
        {
            // Arrange
            var gpioControllerMock = new Mock<IGpioController>();
            var button = new GpioButton(5, gpioControllerMock.Object, shouldDispose: false, PinMode.InputPullUp);

            // Act
            button.Holding += (s, e) => { };

            // Assert
            button.IsHoldingEnabled.Should().BeTrue();
        }

        [Fact]
        public void ShouldSetIsHoldingEnabled_IfHoldingEventUnsubscribed()
        {
            // Arrange
            var gpioControllerMock = new Mock<IGpioController>();
            var button = new GpioButton(5, gpioControllerMock.Object, shouldDispose: false, PinMode.InputPullUp);

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

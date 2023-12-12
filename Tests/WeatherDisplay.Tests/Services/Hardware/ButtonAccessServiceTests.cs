using System;
using System.Device.Gpio;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Services;
using WeatherDisplay.Services.Hardware;
using Xunit;

namespace WeatherDisplay.Tests.Services.Hardware
{
    public class ButtonAccessServiceTests
    {
        private readonly AutoMocker autoMocker;

        public ButtonAccessServiceTests()
        {
            this.autoMocker = new AutoMocker();

            this.autoMocker.Use<ILoggerFactory>(new NullLoggerFactory());

            var gpioControllerMock = this.autoMocker.GetMock<IGpioController>();
            gpioControllerMock.Setup(g => g.IsPinModeSupported(It.IsAny<int>(), It.IsAny<PinMode>()))
                .Returns(true);
            gpioControllerMock.Setup(g => g.WaitForEvent(It.IsAny<int>(), It.IsAny<PinEventTypes>(), It.IsAny<TimeSpan>()))
                .Returns((int _, PinEventTypes t, TimeSpan _) => new WaitForEventResult { EventTypes = t, TimedOut = false });
        }

        [Fact]
        public void ShouldInitialize()
        {
            // Arrange
            var gpioControllerMock = this.autoMocker.GetMock<IGpioController>();

            var appSettingsMock = this.autoMocker.GetMock<IOptions<AppSettings>>();
            appSettingsMock.Setup(a => a.Value).Returns(AppSettings.Default);

            var buttonAccessService = this.autoMocker.CreateInstance<ButtonsAccessService>();

            // Act
            buttonAccessService.Initialize();

            // Assert
            gpioControllerMock.Verify(g => g.OpenPin(6, PinMode.InputPullUp), Times.Once);
            gpioControllerMock.Verify(g => g.OpenPin(5, PinMode.InputPullUp), Times.Once);
            gpioControllerMock.Verify(g => g.OpenPin(16, PinMode.InputPullUp), Times.Once);
            gpioControllerMock.Verify(g => g.OpenPin(26, PinMode.InputPullUp), Times.Once);
        }

        [Fact]
        public async void ShouldShutdownIfButton1And2IsHolding()
        {
            // Arrange
            var shutdownServiceMock = this.autoMocker.GetMock<IShutdownService>();

            var gpioControllerMock = this.autoMocker.GetMock<IGpioController>();

            PinChangeEventHandler pinChangeEventHandlerPin6 = null;
            gpioControllerMock.Setup(c => c.RegisterCallbackForPinValueChangedEvent(6, It.IsAny<PinEventTypes>(), It.IsAny<PinChangeEventHandler>()))
                .Callback((int _, PinEventTypes _, PinChangeEventHandler eventHandler) => { pinChangeEventHandlerPin6 = eventHandler; });

            PinChangeEventHandler pinChangeEventHandlerPin5 = null;
            gpioControllerMock.Setup(c => c.RegisterCallbackForPinValueChangedEvent(5, It.IsAny<PinEventTypes>(), It.IsAny<PinChangeEventHandler>()))
                .Callback((int _, PinEventTypes _, PinChangeEventHandler eventHandler) => { pinChangeEventHandlerPin5 = eventHandler; });

            var appSettingsMock = this.autoMocker.GetMock<IOptions<AppSettings>>();
            appSettingsMock.Setup(a => a.Value).Returns(AppSettings.Default);

            var buttonAccessService = this.autoMocker.CreateInstance<ButtonsAccessService>();
            buttonAccessService.Initialize();

            // Act
            pinChangeEventHandlerPin6(null, new PinValueChangedEventArgs(PinEventTypes.Falling, 6));
            await Task.Delay(100);
            pinChangeEventHandlerPin5(null, new PinValueChangedEventArgs(PinEventTypes.Falling, 5));
            await Task.Delay(2100);
            pinChangeEventHandlerPin6(null, new PinValueChangedEventArgs(PinEventTypes.Rising, 6));
            await Task.Delay(100);
            pinChangeEventHandlerPin5(null, new PinValueChangedEventArgs(PinEventTypes.Rising, 5));

            // Assert
            shutdownServiceMock.Verify(s => s.Shutdown(), Times.Once);
        }
    }
}

using System.Device.I2c;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace System.Device.Gpio
{
    [ExcludeFromCodeCoverage]
    internal class GpioControllerMock : IGpioController
    {
        private readonly ILogger logger;

        public GpioControllerMock(ILogger<GpioControllerMock> logger)
        {
            this.logger = logger;
        }

        public void ClosePin(int pinNumber)
        {
            this.logger.LogDebug($"ClosePin({pinNumber})");
        }

        public bool IsPinModeSupported(int pinNumber, PinMode mode)
        {
            this.logger.LogDebug($"IsPinModeSupported({pinNumber}, {mode})");
            return true;
        }

        public void OpenPin(int pinNumber, PinMode mode)
        {
            this.logger.LogDebug($"OpenPin({pinNumber}, {mode})");
        }

        public void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback)
        {
            this.logger.LogDebug($"RegisterCallbackForPinValueChangedEvent({pinNumber}, {eventTypes}, ...)");
        }

        public void UnregisterCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback)
        {
            this.logger.LogDebug($"UnregisterCallbackForPinValueChangedEvent({pinNumber}, ...)");
        }

        public void Write(int pinNumber, PinValue pinValue)
        {
            this.logger.LogDebug($"Write({pinNumber}, {pinValue})");
        }

        public void Dispose()
        {
            this.logger.LogDebug($"Dispose()");
        }
    }
}

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace System.Device.Gpio
{
    [ExcludeFromCodeCoverage]
    internal class GpioControllerMock : IGpioController
    {
        private readonly ILogger logger;

        public PinNumberingScheme NumberingScheme => throw new NotImplementedException();

        public int PinCount => throw new NotImplementedException();

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

        public void OpenPin(int pinNumber)
        {
            this.logger.LogDebug($"OpenPin({pinNumber})");
        }

        public void OpenPin(int pinNumber, PinMode mode)
        {
            this.logger.LogDebug($"OpenPin({pinNumber}, {mode})");
        }

        public void OpenPin(int pinNumber, PinMode mode, PinValue initialValue)
        {
            this.logger.LogDebug($"OpenPin({pinNumber}, {mode}, {initialValue})");
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


        public PinValue Read(int pinNumber)
        {
            throw new NotImplementedException();
        }

        public void SetPinMode(int pinNumber, PinMode mode)
        {
            throw new NotImplementedException();
        }

        public PinMode GetPinMode(int pinNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsPinOpen(int pinNumber)
        {
            throw new NotImplementedException();
        }

        public WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public ValueTask<WaitForEventResult> WaitForEventAsync(int pinNumber, PinEventTypes eventTypes, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public ValueTask<WaitForEventResult> WaitForEventAsync(int pinNumber, PinEventTypes eventTypes, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void Read(Span<PinValuePair> pinValuePairs)
        {
            throw new NotImplementedException();
        }

        public void Write(ReadOnlySpan<PinValuePair> pinValuePairs)
        {
            throw new NotImplementedException();
        }
    }
}

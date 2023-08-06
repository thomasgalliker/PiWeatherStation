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

        public PinValue Read(int pinNumber)
        {
            this.logger.LogDebug($"Read({pinNumber})");
            return new PinValue();
        }

        public void Write(int pinNumber, PinValue pinValue)
        {
            this.logger.LogDebug($"Write({pinNumber}, {pinValue})");
        }

        public void Dispose()
        {
            this.logger.LogDebug($"Dispose()");
        }

        public void SetPinMode(int pinNumber, PinMode mode)
        {
            this.logger.LogDebug($"SetPinMode({pinNumber}, {mode})");
        }

        public PinMode GetPinMode(int pinNumber)
        {
            this.logger.LogDebug($"GetPinMode({pinNumber})");
            return PinMode.InputPullUp;
        }

        public bool IsPinOpen(int pinNumber)
        {
            this.logger.LogDebug($"IsPinOpen({pinNumber})");
            return true;
        }

        public WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, TimeSpan timeout)
        {
            this.logger.LogDebug($"WaitForEvent({pinNumber}, {eventTypes}, {timeout})");
            return new WaitForEventResult();
        }

        public WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken)
        {
            this.logger.LogDebug($"WaitForEvent({pinNumber}, {eventTypes}, CancellationToken)");
            return new WaitForEventResult();
        }

        public ValueTask<WaitForEventResult> WaitForEventAsync(int pinNumber, PinEventTypes eventTypes, TimeSpan timeout)
        {
            this.logger.LogDebug($"WaitForEventAsync({pinNumber}, {eventTypes}, {timeout})");
            return new ValueTask<WaitForEventResult>(new WaitForEventResult());
        }

        public ValueTask<WaitForEventResult> WaitForEventAsync(int pinNumber, PinEventTypes eventTypes, CancellationToken token)
        {
            this.logger.LogDebug($"WaitForEventAsync({pinNumber}, {eventTypes}, CancellationToken)");
            return new ValueTask<WaitForEventResult>(new WaitForEventResult());
        }

        public void Read(Span<PinValuePair> pinValuePairs)
        {
            this.logger.LogDebug($"Read(ReadOnlySpan<PinValuePair>)");
        }

        public void Write(ReadOnlySpan<PinValuePair> pinValuePairs)
        {
            this.logger.LogDebug($"Write(ReadOnlySpan<PinValuePair>)");
        }
    }
}

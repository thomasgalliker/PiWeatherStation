using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace System.Device.Gpio
{
    [ExcludeFromCodeCoverage]
    public class GpioControllerWrapper : IGpioController
    {
        private readonly ILogger<GpioController> logger;
        private readonly GpioController gpioController;

        public GpioControllerWrapper()
            : this(new NullLogger<GpioController>())
        {
        }
        
        public GpioControllerWrapper(ILogger<GpioController> logger)
            : this(logger, new GpioController())
        {
        }

        public GpioControllerWrapper(ILogger<GpioController> logger, GpioController gpioController)
        {
            this.logger = logger;
            this.gpioController = gpioController;
        }

        public PinNumberingScheme NumberingScheme
        {
            get
            {
                this.logger.LogDebug($"NumberingScheme_get)");
                return this.gpioController.NumberingScheme;
            }
        }

        public int PinCount
        {
            get
            {
                this.logger.LogDebug($"PinCount_get)");
                return this.gpioController.PinCount;
            }
        }

        public void OpenPin(int pinNumber)
        {
            this.logger.LogDebug($"OpenPin({pinNumber})");
            this.gpioController.OpenPin(pinNumber);
        }

        public void OpenPin(int pinNumber, PinMode mode)
        {
            this.logger.LogDebug($"OpenPin({pinNumber}, {mode})");
            this.gpioController.OpenPin(pinNumber, mode);
        }

        public void OpenPin(int pinNumber, PinMode mode, PinValue initialValue)
        {
            this.logger.LogDebug($"OpenPin({pinNumber}, {mode}, {initialValue})");
            this.gpioController.OpenPin(pinNumber, mode, initialValue);
        }

        public void ClosePin(int pinNumber)
        {
            this.logger.LogDebug($"ClosePin({pinNumber})");
            this.gpioController.ClosePin(pinNumber);
        }

        public void SetPinMode(int pinNumber, PinMode mode)
        {
            this.logger.LogDebug($"SetPinMode({pinNumber}, {mode})");
            this.gpioController.SetPinMode(pinNumber, mode);
        }
        
        public PinMode GetPinMode(int pinNumber)
        {
            this.logger.LogDebug($"GetPinMode({pinNumber})");
            return this.gpioController.GetPinMode(pinNumber);
        }

        public bool IsPinOpen(int pinNumber)
        {
            this.logger.LogDebug($"IsPinOpen({pinNumber})");
            return this.gpioController.IsPinOpen(pinNumber);
        }

        public bool IsPinModeSupported(int pinNumber, PinMode mode)
        {
            this.logger.LogDebug($"IsPinModeSupported({pinNumber}, {mode})");
            return this.gpioController.IsPinModeSupported(pinNumber, mode);
        }

        public PinValue Read(int pinNumber)
        {
            this.logger.LogDebug($"Read({pinNumber})");
            return this.gpioController.Read(pinNumber);
        }

        public void Write(int pinNumber, PinValue pinValue)
        {
            this.logger.LogDebug($"Write({pinNumber}, {pinValue})");
            this.gpioController.Write(pinNumber, pinValue);
        }

        public WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, TimeSpan timeout)
        {
            this.logger.LogDebug($"WaitForEvent({pinNumber}, {eventTypes}, TimeSpan)");
            return this.gpioController.WaitForEvent(pinNumber, eventTypes, timeout);
        }

        public WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken)
        {
            this.logger.LogDebug($"WaitForEvent({pinNumber}, {eventTypes}, CancellationToken)");
            return this.gpioController.WaitForEvent(pinNumber, eventTypes, cancellationToken);
        }

        public ValueTask<WaitForEventResult> WaitForEventAsync(int pinNumber, PinEventTypes eventTypes, TimeSpan timeout)
        {
            this.logger.LogDebug($"WaitForEventAsync({pinNumber}, {eventTypes}, TimeSpan)");
            return this.gpioController.WaitForEventAsync(pinNumber, eventTypes, timeout);
        }

        public ValueTask<WaitForEventResult> WaitForEventAsync(int pinNumber, PinEventTypes eventTypes, CancellationToken token)
        {
            this.logger.LogDebug($"WaitForEventAsync({pinNumber}, {eventTypes}, CancellationToken)");
            return this.gpioController.WaitForEventAsync(pinNumber, eventTypes, token);
        }

        public void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback)
        {
            this.logger.LogDebug($"RegisterCallbackForPinValueChangedEvent({pinNumber}, {eventTypes}, PinChangeEventHandler)");
            this.gpioController.RegisterCallbackForPinValueChangedEvent(pinNumber, eventTypes, callback);
        }
        
        public void UnregisterCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback)
        {
            this.logger.LogDebug($"UnregisterCallbackForPinValueChangedEvent({pinNumber}, ...)");
            this.gpioController.UnregisterCallbackForPinValueChangedEvent(pinNumber, callback);
        }

        public void Dispose()
        {
            this.logger.LogDebug($"Dispose()");
            this.gpioController.Dispose();
        }

        public void Write(ReadOnlySpan<PinValuePair> pinValuePairs)
        {
            this.logger.LogDebug($"Write(ReadOnlySpan<PinValuePair>)");
            this.gpioController.Write(pinValuePairs);
        }
        
        public void Read(Span<PinValuePair> pinValuePairs)
        {
            this.logger.LogDebug($"Read(ReadOnlySpan<PinValuePair>)");
            this.gpioController.Read(pinValuePairs);
        }
    }
}

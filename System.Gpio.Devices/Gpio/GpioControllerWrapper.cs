using System.Diagnostics.CodeAnalysis;

namespace System.Device.Gpio
{
    [ExcludeFromCodeCoverage]
    public class GpioControllerWrapper : IGpioController
    {
        private readonly GpioController gpioController;

        public GpioControllerWrapper()
            : this(new GpioController())
        {
        }

        public GpioControllerWrapper(GpioController gpioController)
        {
            this.gpioController = gpioController;
        }

        public void ClosePin(int pinNumber)
        {
            this.gpioController.ClosePin(pinNumber);
        }

        public bool IsPinModeSupported(int pinNumber, PinMode mode)
        {
            return this.gpioController.IsPinModeSupported(pinNumber, mode);
        }

        public void OpenPin(int pinNumber, PinMode mode)
        {
            this.gpioController.OpenPin(pinNumber, mode);
        }

        public void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback)
        {
            this.gpioController.RegisterCallbackForPinValueChangedEvent(pinNumber, eventTypes, callback);
        }

        public void UnregisterCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback)
        {
            this.gpioController.UnregisterCallbackForPinValueChangedEvent(pinNumber, callback);
        }

        public void Write(int pinNumber, PinValue pinValue)
        {
            this.gpioController.Write(pinNumber, pinValue);
        }

        public void Dispose()
        {
            this.gpioController.Dispose();
        }
    }
}

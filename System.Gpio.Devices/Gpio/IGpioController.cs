using System.Device.Gpio;

namespace System.Device.I2c
{
    public interface IGpioController : IDisposable
    {
        bool IsPinModeSupported(int pinNumber, PinMode mode);

        void ClosePin(int pinNumber);

        void OpenPin(int pinNumber, PinMode mode);

        void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback);

        void UnregisterCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback);

        void Write(int pinNumber, PinValue pinValue);
    }
}

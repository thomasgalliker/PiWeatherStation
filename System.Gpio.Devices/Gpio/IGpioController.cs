namespace System.Device.Gpio
{
    /// <inheritdoc cref="GpioController" />
    public interface IGpioController : IDisposable
    {
        /// <inheritdoc cref="GpioController.IsPinModeSupported(int, PinMode)" />
        bool IsPinModeSupported(int pinNumber, PinMode mode);

        /// <inheritdoc cref="GpioController.ClosePin(int)" />
        void ClosePin(int pinNumber);

        /// <inheritdoc cref="GpioController.OpenPin(int, PinMode)" />
        void OpenPin(int pinNumber, PinMode mode);

        /// <inheritdoc cref="GpioController.RegisterCallbackForPinValueChangedEvent(int, PinEventTypes, PinChangeEventHandler)" />
        void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback);

        /// <inheritdoc cref="GpioController.UnregisterCallbackForPinValueChangedEvent(int, PinChangeEventHandler)" />
        void UnregisterCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback);

        /// <inheritdoc cref="GpioController.Write(int, PinMode)" />
        void Write(int pinNumber, PinValue pinValue);
    }
}

using System.Threading;
using System.Threading.Tasks;

namespace System.Device.Gpio
{
    /// <inheritdoc cref="GpioController" />
    public interface IGpioController : IDisposable
    {
        /// <inheritdoc cref="GpioController.NumberingScheme" />
        PinNumberingScheme NumberingScheme { get; }

        /// <inheritdoc cref="GpioController.PinCount" />
        int PinCount { get; }

        /// <inheritdoc cref="GpioController.OpenPin(int)" />
        void OpenPin(int pinNumber);

        /// <inheritdoc cref="GpioController.OpenPin(int, PinMode)" />
        void OpenPin(int pinNumber, PinMode mode);

        /// <inheritdoc cref="GpioController.OpenPin(int, PinMode, PinValue)" />
        void OpenPin(int pinNumber, PinMode mode, PinValue initialValue);

        /// <inheritdoc cref="GpioController.ClosePin(int)" />
        void ClosePin(int pinNumber);

        /// <inheritdoc cref="GpioController.SetPinMode(int, PinMode)" />
        void SetPinMode(int pinNumber, PinMode mode);

        /// <inheritdoc cref="GpioController.GetPinMode(int)" />
        PinMode GetPinMode(int pinNumber);

        /// <inheritdoc cref="GpioController.IsPinOpen(int)" />
        bool IsPinOpen(int pinNumber);

        /// <inheritdoc cref="GpioController.IsPinModeSupported(int, PinMode)" />
        bool IsPinModeSupported(int pinNumber, PinMode mode);

        /// <inheritdoc cref="GpioController.Read(int)" />
        PinValue Read(int pinNumber);

        /// <inheritdoc cref="GpioController.Write(int, PinMode)" />
        void Write(int pinNumber, PinValue pinValue);

        /// <inheritdoc cref="GpioController.WaitForEvent(int, PinEventTypes, TimeSpan)" />
        WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, TimeSpan timeout);

        /// <inheritdoc cref="GpioController.WaitForEvent(int, PinEventTypes, CancellationToken)" />
        WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken);

        /// <inheritdoc cref="GpioController.WaitForEventAsync(int, PinEventTypes, TimeSpan)" />
        ValueTask<WaitForEventResult> WaitForEventAsync(int pinNumber, PinEventTypes eventTypes, TimeSpan timeout);

        /// <inheritdoc cref="GpioController.WaitForEventAsync(int, PinEventTypes, CancellationToken)" />
        ValueTask<WaitForEventResult> WaitForEventAsync(int pinNumber, PinEventTypes eventTypes, CancellationToken token);

        /// <inheritdoc cref="GpioController.RegisterCallbackForPinValueChangedEvent(int, PinEventTypes, PinChangeEventHandler)" />
        void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback);

        /// <inheritdoc cref="GpioController.UnregisterCallbackForPinValueChangedEvent(int, PinChangeEventHandler)" />
        void UnregisterCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback);

        /// <inheritdoc cref="GpioController.Read(Span<PinValuePair>)" />
        void Read(Span<PinValuePair> pinValuePairs);

        /// <inheritdoc cref="GpioController.Write(Span<PinValuePair>)" />
        void Write(ReadOnlySpan<PinValuePair> pinValuePairs);
    }
}

using System.Device.Gpio;

namespace System.Device.Buttons
{
    /// <summary>
    /// GPIO implementation of Button.
    /// Inherits from ButtonBase.
    /// </summary>
    public class GpioButton : ButtonBase
    {
        private IGpioController gpioController;
        private readonly PinMode eventPinMode;

        private readonly int buttonPin;
        private readonly bool shouldDispose;

        private bool disposed = false;

        /// <summary>
        /// Specify whether the Gpio associated with the button has an external resistor acting as pull-up or pull-down.
        /// </summary>
        public bool HasExternalResistor { get; private set; } = false;

        /// <summary>
        /// Initialization of the button.
        /// </summary>
        /// <param name="buttonPin">GPIO pin of the button.</param>
        /// <param name="isPullUp">True if the Gpio is either pulled up in hardware or in the Gpio configuration (see <paramref name="hasExternalResistor"/>. False if instead the Gpio is pulled down.</param>
        /// <param name="hasExternalResistor">When False the pull resistor is configured using the Gpio PinMode.InputPullUp or PinMode.InputPullDown (if supported by the board). Otherwise the Gpio is configured as PinMode.Input.</param>
        /// <param name="gpio">Gpio Controller.</param>
        /// <param name="shouldDispose">True to dispose the GpioController.</param>
        /// <param name="debounceTime">The amount of time during which the transitions are ignored, or zero</param>
        public GpioButton(int buttonPin, bool isPullUp = true, bool hasExternalResistor = false,
            IGpioController gpio = null, bool shouldDispose = true, TimeSpan debounceTime = default)
            : this(buttonPin, DefaultDoublePressDuration, DefaultHoldingDuration, isPullUp, hasExternalResistor, gpio, shouldDispose, debounceTime)
        {
        }

        /// <summary>
        /// Initialization of the button.
        /// </summary>
        /// <param name="buttonPin">GPIO pin of the button.</param>
        /// <param name="doublePress">Max ticks between button presses to count as doublepress.</param>
        /// <param name="holding">Min ms a button is pressed to count as holding.</param>
        /// <param name="isPullUp">True if the Gpio is either pulled up in hardware or in the Gpio configuration (see <paramref name="hasExternalResistor"/>. False if instead the Gpio is pulled down.</param>
        /// <param name="hasExternalResistor">When False the pull resistor is configured using the Gpio PinMode.InputPullUp or PinMode.InputPullDown (if supported by the board). Otherwise the Gpio is configured as PinMode.Input.</param>
        /// <param name="gpio">Gpio Controller.</param>
        /// <param name="shouldDispose">True to dispose the GpioController.</param>
        /// <param name="debounceTime">The amount of time during which the transitions are ignored, or zero</param>
        public GpioButton(int buttonPin,
            TimeSpan doublePress,
            TimeSpan holding,
            bool isPullUp = true,
            bool hasExternalResistor = false,
            IGpioController gpio = null,
            bool shouldDispose = true,
            TimeSpan debounceTime = default)
            : base(doublePress, holding, debounceTime)
        {
            this.gpioController = gpio ?? new GpioControllerWrapper();
            this.shouldDispose = gpio == null ? true : shouldDispose;
            this.buttonPin = buttonPin;
            this.HasExternalResistor = hasExternalResistor;

            this.eventPinMode = isPullUp ? PinMode.InputPullUp : PinMode.InputPullDown;
            var gpioPinMode = hasExternalResistor
                ? PinMode.Input
                : this.eventPinMode;

            if (!this.gpioController.IsPinModeSupported(this.buttonPin, gpioPinMode))
            {
                if (gpioPinMode == PinMode.Input)
                {
                    throw new ArgumentException($"The pin {this.buttonPin} cannot be configured as Input");
                }

                throw new ArgumentException($"The pin {this.buttonPin} cannot be configured as {(isPullUp ? "pull-up" : "pull-down")}. Use an external resistor and set {nameof(this.HasExternalResistor)}=true");
            }

            try
            {
                this.gpioController.OpenPin(this.buttonPin, gpioPinMode);
                this.gpioController.RegisterCallbackForPinValueChangedEvent(
                    this.buttonPin,
                    PinEventTypes.Falling | PinEventTypes.Rising,
                    this.PinStateChanged);
            }
            catch (Exception)
            {
                if (shouldDispose)
                {
                    this.gpioController.Dispose();
                }

                throw;
            }
        }

        /// <summary>
        /// Handles changes in GPIO pin, based on whether the system is pullup or pulldown.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="pinValueChangedEventArgs">The pin argument changes.</param>
        private void PinStateChanged(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            switch (pinValueChangedEventArgs.ChangeType)
            {
                case PinEventTypes.Falling:
                    if (this.eventPinMode == PinMode.InputPullUp)
                    {
                        this.HandleButtonPressed();
                    }
                    else
                    {
                        this.HandleButtonReleased();
                    }

                    break;
                case PinEventTypes.Rising:
                    if (this.eventPinMode == PinMode.InputPullUp)
                    {
                        this.HandleButtonReleased();
                    }
                    else
                    {
                        this.HandleButtonPressed();
                    }

                    break;
            }
        }

        /// <summary>
        /// Internal cleanup.
        /// </summary>
        /// <param name="disposing">Disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.gpioController.UnregisterCallbackForPinValueChangedEvent(this.buttonPin, this.PinStateChanged);
                if (this.shouldDispose)
                {
                    this.gpioController?.Dispose();
                    this.gpioController = null;
                }
                else
                {
                    this.gpioController.ClosePin(this.buttonPin);
                }
            }

            base.Dispose(disposing);
            this.disposed = true;
        }
    }
}
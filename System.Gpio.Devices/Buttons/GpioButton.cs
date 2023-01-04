using System.Device.Gpio;
using System.Device.I2c;

namespace System.Gpio.Devices.Buttons
{
    /// <summary>
    /// GPIO implementation of Button.
    /// Inherits from ButtonBase.
    /// </summary>
    public class GpioButton : ButtonBase
    {
        private IGpioController gpioController;
        private readonly PinMode pinMode;

        private readonly int buttonPin;
        private readonly bool shouldDispose;

        private bool disposed = false;

        /// <summary>
        /// Initialization of the button.
        /// </summary>
        /// <param name="buttonPin">GPIO pin of the button.</param>
        /// <param name="pinMode">Pin mode of the system.</param>
        /// <param name="gpio">Gpio Controller.</param>
        /// <param name="shouldDispose">True to dispose the GpioController.</param>
        /// <param name="debounceTime">The amount of time during which the transitions are ignored, or zero</param>
        public GpioButton(int buttonPin, IGpioController gpio = null, bool shouldDispose = true, PinMode pinMode = PinMode.InputPullUp, TimeSpan debounceTime = default)
            : this(buttonPin, DefaultDoublePressDuration, DefaultHoldingDuration, gpio, shouldDispose, pinMode, debounceTime)
        {
        }

        /// <summary>
        /// Initialization of the button.
        /// </summary>
        /// <param name="buttonPin">GPIO pin of the button.</param>
        /// <param name="pinMode">Pin mode of the system.</param>
        /// <param name="doublePress">Max ticks between button presses to count as doublepress.</param>
        /// <param name="holding">Min ms a button is pressed to count as holding.</param>
        /// <param name="gpioController">Gpio Controller.</param>
        /// <param name="shouldDispose">True to dispose the GpioController.</param>
        /// <param name="debounceTime">The amount of time during which the transitions are ignored, or zero</param>
        public GpioButton(int buttonPin, TimeSpan doublePress, TimeSpan holding, IGpioController gpioController = null, bool shouldDispose = true, PinMode pinMode = PinMode.InputPullUp, TimeSpan debounceTime = default)
            : base(doublePress, holding, debounceTime)
        {
            this.gpioController = gpioController ?? new GpioControllerWrapper();
            this.shouldDispose = shouldDispose;
            this.buttonPin = buttonPin;
            this.pinMode = pinMode;

            if (this.pinMode == PinMode.Input | this.pinMode == PinMode.InputPullDown | this.pinMode == PinMode.InputPullUp)
            {
                this.gpioController.OpenPin(this.buttonPin, this.pinMode);
                this.gpioController.RegisterCallbackForPinValueChangedEvent(this.buttonPin, PinEventTypes.Falling | PinEventTypes.Rising, this.PinStateChanged);
            }
            else
            {
                throw new ArgumentException("GPIO pin can only be set to input, not to output.");
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
                    if (this.pinMode == PinMode.InputPullUp)
                    {
                        this.HandleButtonPressed();
                    }
                    else
                    {
                        this.HandleButtonReleased();
                    }

                    break;
                case PinEventTypes.Rising:
                    if (this.pinMode == PinMode.InputPullUp)
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

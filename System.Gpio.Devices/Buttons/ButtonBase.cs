﻿using System.Diagnostics;
using System.Threading;

namespace System.Gpio.Devices.Buttons
{
    /// <summary>
    /// Base implementation of Button logic.
    /// Hardware independent. Inherit for specific hardware handling.
    /// </summary>
    public class ButtonBase : IDisposable
    {
        internal static readonly TimeSpan DefaultDoublePressDuration = TimeSpan.FromSeconds(1.5d);
        internal static readonly TimeSpan DefaultHoldingDuration = TimeSpan.FromSeconds(2d);

        private bool disposed = false;

        private readonly long doublePressTicks;
        private readonly int holdingMs;
        private readonly TimeSpan debounceTime;
        private long debounceStartTicks;

        private ButtonHoldingState holdingState = ButtonHoldingState.Completed;

        private long lastPress = DateTime.MinValue.Ticks;
        private Timer holdingTimer;
        private bool isPressed = false;
        private readonly bool buttonState;             // the current reading from the input pin
        private readonly bool lastButtonState = false;   // the previous reading from the input pin


        /// <summary>
        /// Delegate for button up event.
        /// </summary>
        public event EventHandler<EventArgs> ButtonUp;

        /// <summary>
        /// Delegate for button down event.
        /// </summary>
        public event EventHandler<EventArgs> ButtonDown;

        /// <summary>
        /// Delegate for button pressed event.
        /// </summary>
        public event EventHandler<EventArgs> Press;

        /// <summary>
        /// Delegate for button double pressed event.
        /// </summary>
        public event EventHandler<EventArgs> DoublePress;

        /// <summary>
        /// Delegate for button holding event.
        /// </summary>
        public event EventHandler<ButtonHoldingEventArgs> Holding;

        /// <summary>
        /// Define if holding event is enabled or disabled on the button.
        /// </summary>
        public bool IsHoldingEnabled { get; set; } = false;

        /// <summary>
        /// Define if double press event is enabled or disabled on the button.
        /// </summary>
        public bool IsDoublePressEnabled { get; set; } = false;

        /// <summary>
        /// Define if single press event is enabled or disabled on the button.
        /// </summary>
        public bool IsPressed
        {
            get => this.isPressed;
            set
            {
                if (this.isPressed == value)
                {
                    Debug.WriteLine($"IsPressed is already {value}");
                }

                this.isPressed = value;
            }
        }
        /// <summary>
        /// Initialization of the button.
        /// </summary>
        public ButtonBase()
            : this(DefaultDoublePressDuration, DefaultHoldingDuration)
        {
        }

        /// <summary>
        /// Initialization of the button.
        /// </summary>
        /// <param name="doublePress">Max ticks between button presses to count as doublepress.</param>
        /// <param name="holding">Min ms a button is pressed to count as holding.</param>
        /// <param name="debounceTime">The amount of time during which the transitions are ignored, or zero</param>
        public ButtonBase(TimeSpan doublePress, TimeSpan holding, TimeSpan debounceTime = default)
        {
            if (debounceTime.TotalMilliseconds * 3 > doublePress.TotalMilliseconds)
            {
                throw new ArgumentException($"The parameter {nameof(doublePress)} should be at least three times {nameof(debounceTime)}");
            }

            this.doublePressTicks = doublePress.Ticks;
            this.holdingMs = (int)holding.TotalMilliseconds;
            this.debounceTime = debounceTime;
        }

        /// <summary>
        /// Handler for pressing the button.
        /// </summary>
        protected void HandleButtonPressed()
        {
            if (DateTime.UtcNow.Ticks - this.debounceStartTicks < this.debounceTime.Ticks)
            {
                return;
            }

            if (this.IsPressed == true)
            {
                return;
            }

            this.IsPressed = true;

            ButtonDown?.Invoke(this, EventArgs.Empty);

            if (this.IsHoldingEnabled)
            {
                this.holdingTimer = new Timer(this.StartHoldingHandler, null, this.holdingMs, Timeout.Infinite);
            }
        }

        /// <summary>
        /// Handler for releasing the button.
        /// </summary>
        protected void HandleButtonReleased()
        {
            if (this.debounceTime.Ticks > 0 && !this.IsPressed)
            {
                return;
            }

            this.debounceStartTicks = DateTime.UtcNow.Ticks;
            this.holdingTimer?.Dispose();
            this.holdingTimer = null;

            this.IsPressed = false;

            ButtonUp?.Invoke(this, EventArgs.Empty);
            Press?.Invoke(this, EventArgs.Empty);

            if (this.IsHoldingEnabled && this.holdingState == ButtonHoldingState.Started)
            {
                this.holdingState = ButtonHoldingState.Completed;
                Holding?.Invoke(this, new ButtonHoldingEventArgs { HoldingState = ButtonHoldingState.Completed });
            }

            if (this.IsDoublePressEnabled)
            {
                if (this.lastPress == DateTime.MinValue.Ticks)
                {
                    this.lastPress = DateTime.UtcNow.Ticks;
                }
                else
                {
                    if (DateTime.UtcNow.Ticks - this.lastPress <= this.doublePressTicks)
                    {
                        DoublePress?.Invoke(this, EventArgs.Empty);
                    }

                    this.lastPress = DateTime.MinValue.Ticks;
                }
            }
        }

        /// <summary>
        /// Handler for holding the button.
        /// </summary>
        private void StartHoldingHandler(object state)
        {
            this.holdingTimer?.Dispose();
            this.holdingTimer = null;
            this.holdingState = ButtonHoldingState.Started;

            Holding?.Invoke(this, new ButtonHoldingEventArgs { HoldingState = ButtonHoldingState.Started });
        }

        /// <summary>
        /// Cleanup resources.
        /// </summary>
        /// <param name="disposing">Disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.holdingTimer?.Dispose();
                this.holdingTimer = null;
            }

            this.disposed = true;
        }

        /// <summary>
        /// Public dispose method for IDisposable interface.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}

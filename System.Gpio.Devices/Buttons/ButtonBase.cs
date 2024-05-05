using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace System.Device.Buttons
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
        private readonly long holdingMs;
        private readonly long debounceTicks;
        private long debounceStartTicks;

        private const long Released = 0;
        private const long Pressed = 1;
        private long currentState;

        private ButtonHoldingState holdingState = ButtonHoldingState.Completed;

        private long lastPressTicks = DateTime.MinValue.Ticks;
        private Timer holdingTimer;

        private bool isHoldingEnabledAuto = false;
        private bool? isHoldingEnabledInternal;

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
        private event EventHandler<ButtonHoldingEventArgs> HoldingInternal;
        public event EventHandler<ButtonHoldingEventArgs> Holding
        {
            add
            {
                this.HoldingInternal += value;

                if (this.isHoldingEnabledInternal != true && GetEventSubscribers(this.HoldingInternal) > 0)
                {
                    this.isHoldingEnabledAuto = true;
                }
            }
            remove
            {
                this.HoldingInternal -= value;

                if (this.isHoldingEnabledAuto && GetEventSubscribers(this.HoldingInternal) < 1)
                {
                    this.isHoldingEnabledAuto = false;
                }
            }
        }

        private static int GetEventSubscribers<T>(EventHandler<T> eventHandler)
        {
            return eventHandler?.GetInvocationList().Count() ?? 0;
        }

        /// <summary>
        /// Define if holding event is enabled or disabled on the button.
        /// </summary>
        public bool IsHoldingEnabled
        {
            get => this.isHoldingEnabledInternal ?? this.isHoldingEnabledAuto;
            set => this.isHoldingEnabledInternal = value;
        }
        /// <summary>
        /// Define if double press event is enabled or disabled on the button.
        /// </summary>
        public bool IsDoublePressEnabled { get; set; } = false;

        /// <summary>
        /// Checks if the button is currently pressed.
        /// </summary>
        public bool IsPressed => Interlocked.Read(ref this.currentState) == Pressed;

        /// <summary>
        /// Indicates if the button is corrently in holding state.
        /// </summary>
        public bool IsHolding => this.holdingState == ButtonHoldingState.Started;

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
        /// <param name="doublePressTime">Max ticks between button presses to count as doublepress.</param>
        /// <param name="holdingTime">Min ms a button is pressed to count as holding.</param>
        /// <param name="debounceTime">The amount of time during which the transitions are ignored, or zero</param>
        public ButtonBase(TimeSpan doublePressTime, TimeSpan holdingTime, TimeSpan debounceTime = default)
        {
            if (debounceTime.TotalMilliseconds * 3d > doublePressTime.TotalMilliseconds)
            {
                throw new ArgumentException($"The parameter {nameof(doublePressTime)} should be at least three times {nameof(debounceTime)}");
            }

            this.doublePressTicks = doublePressTime.Ticks;
            this.holdingMs = (long)holdingTime.TotalMilliseconds;
            this.debounceTicks = debounceTime.Ticks;
        }

        /// <summary>
        /// Handler for pressing the button.
        /// </summary>
        protected void HandleButtonPressed()
        {
            if (DateTime.UtcNow.Ticks - this.debounceStartTicks < this.debounceTicks)
            {
                return;
            }

            if (Interlocked.CompareExchange(ref this.currentState, Pressed, Released) == Released)
            {
                this.debounceStartTicks = 0L;

                ButtonDown?.Invoke(this, EventArgs.Empty);

                if (this.IsHoldingEnabled)
                {
                    this.holdingTimer = new Timer(this.StartHoldingHandler, null, this.holdingMs, Timeout.Infinite);
                }
            }
        }

        /// <summary>
        /// Handler for releasing the button.
        /// </summary>
        protected void HandleButtonReleased()
        {
            if (!this.IsPressed)
            {
                return;
            }
            
            this.debounceStartTicks = DateTime.UtcNow.Ticks;
            this.holdingTimer?.Dispose();
            this.holdingTimer = null;

            try
            {
                ButtonUp?.Invoke(this, EventArgs.Empty);

                if (this.holdingState == ButtonHoldingState.Started)
                {
                    this.holdingState = ButtonHoldingState.Completed;

                    if (this.IsHoldingEnabled)
                    {
                        HoldingInternal?.Invoke(this, new ButtonHoldingEventArgs { HoldingState = ButtonHoldingState.Completed });
                    }
                    else
                    {
                        Press?.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {
                    Press?.Invoke(this, EventArgs.Empty);
                }

                if (this.IsDoublePressEnabled)
                {
                    if (this.lastPressTicks == DateTime.MinValue.Ticks)
                    {
                        this.lastPressTicks = DateTime.UtcNow.Ticks;
                    }
                    else
                    {
                        if (DateTime.UtcNow.Ticks - this.lastPressTicks <= this.doublePressTicks)
                        {
                            DoublePress?.Invoke(this, EventArgs.Empty);
                        }

                        this.lastPressTicks = DateTime.MinValue.Ticks;
                    }
                }
            }
            finally
            {
                Interlocked.Exchange(ref this.currentState, Released);
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

            HoldingInternal?.Invoke(this, new ButtonHoldingEventArgs { HoldingState = ButtonHoldingState.Started });
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

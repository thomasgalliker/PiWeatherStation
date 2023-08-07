using System.Diagnostics;

namespace System.Device
{
    /// <summary>
    /// Simple <see cref="Stopwatch"/>-based debouncer.
    /// </summary>
    public class Debouncer : IDisposable
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.stopwatch.Restart();
        }

        /// <summary>
        /// Returns a state indicating whether the last signal was debounced.
        /// </summary>
        /// <returns>The state.</returns>
        public State Debounce(TimeSpan debounce)
        {
            return new State(this, debounce == TimeSpan.Zero || this.stopwatch.Elapsed > debounce);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            this.stopwatch.Stop();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.stopwatch.Stop();
        }

        /// <summary>
        /// A state indicating whether the signal was debounced.
        /// </summary>
        public readonly struct State : IDisposable
        {
            private readonly Debouncer debouncer;

            /// <summary>
            /// Initializes a new instance of the <see cref="State"/> struct.
            /// </summary>
            /// <param name="debouncer">The debouncer.</param>
            /// <param name="isDebounced">if set to <c>true</c> [is debounced].</param>
            public State(Debouncer debouncer, bool isDebounced)
            {
                this.IsDebounced = isDebounced;
                this.debouncer = debouncer;
            }

            /// <summary>
            /// Gets a value indicating whether this instance is debounced.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance is debounced; otherwise, <c>false</c>.
            /// </value>
            public bool IsDebounced { get; }

            /// <summary>
            /// Performs an implicit conversion from <see cref="State"/> to <see cref="bool"/>.
            /// </summary>
            /// <param name="state">The state.</param>
            /// <returns>
            /// The result of the conversion.
            /// </returns>
            public static implicit operator bool(State state)
            {
                return state.IsDebounced;
            }

            /// <summary>
            /// Releases unmanaged and - optionally - managed resources.
            /// </summary>
            public void Dispose()
            {
                if (this.IsDebounced)
                {
                    this.debouncer.stopwatch.Restart();
                }
            }
        }
    }
}
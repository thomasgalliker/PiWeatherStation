using System;

namespace DisplayService.Services
{
    public interface ITimerService : IDisposable
    {
        TimeSpan Interval { get; set; }

        bool AutoReset { get; set; }

        void Start();

        void Stop();

        event EventHandler<TimerElapsedEventArgs> Elapsed;
    }

    public class TimerElapsedEventArgs : EventArgs
    {
        public TimerElapsedEventArgs() : this(DateTime.Now)
        {
        }

        public TimerElapsedEventArgs(DateTime signalTime)
        {
            this.SignalTime = signalTime;
        }

        public DateTime SignalTime { get; private set; }
    }
}
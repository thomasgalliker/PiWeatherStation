using System;
using System.Timers;

namespace DisplayService.Services
{
    public interface ITimerService : IDisposable
    {
        TimeSpan Interval { get; set; }

        void Start();

        void Stop();

        event ElapsedEventHandler Elapsed;
    }
}
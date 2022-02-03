using System;
using System.Timers;

namespace DisplayService.Services
{
    public interface ITimerService
    {
        TimeSpan Interval { get; set; }

        void Start();

        void Stop();

        event ElapsedEventHandler Elapsed;
    }
}
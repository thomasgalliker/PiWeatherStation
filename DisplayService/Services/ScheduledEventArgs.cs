using System;

namespace DisplayService.Services
{
    public class ScheduledEventArgs : EventArgs
    {
        public ScheduledEventArgs(DateTime signalTime, params Guid[] scheduledTaskIds)
        {
            this.SignalTime = signalTime;
            this.Ids = scheduledTaskIds;
        }

        public DateTime SignalTime { get; }

        public Guid[] Ids { get; }
    }
}
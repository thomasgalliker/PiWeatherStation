using System;

namespace DisplayService.Services
{
    public class ScheduledEventArgs : EventArgs
    {
        public ScheduledEventArgs(params Guid[] scheduledTaskIds)
        {
            this.Ids = scheduledTaskIds;
        }

        public Guid[] Ids { get; }
    }
}
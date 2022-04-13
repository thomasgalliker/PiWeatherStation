﻿using System;

namespace DisplayService.Services
{
    public class ScheduledEventArgs : EventArgs
    {
        public ScheduledEventArgs(DateTime signalTime, params Guid[] scheduledTaskIds)
        {
            this.SignalTime = signalTime;
            this.TaskIds = scheduledTaskIds;
        }

        public DateTime SignalTime { get; }

        public Guid[] TaskIds { get; }
    }
}
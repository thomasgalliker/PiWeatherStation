using System;

namespace NCrontab.Scheduler
{
    public interface IDateTime
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }
    }
}
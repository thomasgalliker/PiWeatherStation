using System;

namespace DisplayService.Services.Scheduling
{
    public abstract class ScheduleItem
    {
        public Guid Id { get; }

        public string CronExpression { get; }

        public abstract void Cancel();
    }
}

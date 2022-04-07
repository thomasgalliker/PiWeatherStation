using System;
using System.Threading;
using System.Threading.Tasks;

namespace DisplayService.Services.Scheduling
{
    public class ScheduledTask
    {
        public ScheduledTask(Guid id, CronExpression cronExpression, Action<CancellationToken> action)
        {
            this.Id = id;
            this.CronExpression = cronExpression;
            this.Action = action;
        }

        public ScheduledTask(Guid id, CronExpression cronExpression, Func<CancellationToken, Task> action)
        {
            this.Id = id;
            this.CronExpression = cronExpression;
            this.ActionTask = action;
        }

        internal void SetCronExpression(CronExpression cronExpression)
        {
            this.CronExpression = cronExpression;
        }

        public Guid Id { get; }

        public CronExpression CronExpression { get; private set; }

        internal Action<CancellationToken> Action { get; }

        internal Func<CancellationToken, Task> ActionTask { get; }
    }
}

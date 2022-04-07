using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DisplayService.Services.Scheduling
{
    public interface IScheduler
    {
        void ChangeScheduleAndResetScheduler(Guid id, CronExpression cronExpression);

        void ChangeSchedulesAndResetScheduler(IEnumerable<ScheduleItem> scheduleChanges);

        void Start(CancellationToken cancellationToken = default);

        Task StartAsync(CancellationToken cancellationToken = default);

        Guid ScheduleTask(CronExpression cronExpression, Action<CancellationToken> action);

        void ScheduleTask(Guid id, CronExpression cronExpression, Action<CancellationToken> action);

        Guid ScheduleTask(CronExpression cronExpression, Func<CancellationToken, Task> func);

        void ScheduleTask(Guid id, CronExpression cronExpression, Func<CancellationToken, Task> func);

        event EventHandler<ScheduledEventArgs> Next;

        void Stop();
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NCrontab;

namespace DisplayService.Services.Scheduling
{
    public interface IScheduler
    {
        void ChangeScheduleAndResetScheduler(Guid id, CrontabSchedule cronExpression);

        void ChangeSchedulesAndResetScheduler(IEnumerable<(Guid Id, CrontabSchedule CrontabSchedule)> scheduleChanges);

        void Start(CancellationToken cancellationToken = default);

        Task StartAsync(CancellationToken cancellationToken = default);

        Guid ScheduleTask(CrontabSchedule cronExpression, Action<CancellationToken> action);

        void ScheduleTask(Guid id, CrontabSchedule cronExpression, Action<CancellationToken> action);

        Guid ScheduleTask(CrontabSchedule cronExpression, Func<CancellationToken, Task> func);

        void ScheduleTask(Guid id, CrontabSchedule cronExpression, Func<CancellationToken, Task> func);

        event EventHandler<ScheduledEventArgs> Next;

        void Stop();
    }
}
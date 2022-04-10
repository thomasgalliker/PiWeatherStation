using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DisplayService.Services.Scheduling
{
    public class Scheduler : IScheduler
    {
        private readonly List<ScheduledTask> scheduledTasks = new List<ScheduledTask>();
        private readonly IDateTime dateTime;
        private readonly ILogger<Scheduler> logger;
        private readonly TimeSpan oneMinute = new TimeSpan(0, 1, 0);
        private CancellationTokenSource localCancellationTokenSource = new CancellationTokenSource();
        private CancellationToken externalCancellationToken;

        public Scheduler(
            ILogger<Scheduler> logger,
            IDateTime dateTime)
        {
            this.dateTime = dateTime;
            this.logger = logger;
        }

        public void ScheduleTask(Guid id, CronExpression cronExpression, Func<CancellationToken, Task> func)
        {
            var scheduledTask = new ScheduledTask(id, cronExpression, func);

            this.scheduledTasks.Add(scheduledTask);
        }

        public Guid ScheduleTask(CronExpression cronExpression, Func<CancellationToken, Task> func)
        {
            var id = Guid.NewGuid();

            this.ScheduleTask(id, cronExpression, func);

            return id;
        }

        public void ScheduleTask(Guid id, CronExpression cronExpression, Action<CancellationToken> action)
        {
            var scheduledTask = new ScheduledTask(id, cronExpression, action);

            this.scheduledTasks.Add(scheduledTask);
        }

        public Guid ScheduleTask(CronExpression cronExpression, Action<CancellationToken> action)
        {
            var id = Guid.NewGuid();

            this.ScheduleTask(id, cronExpression, action);

            return id;
        }

        public void Start(CancellationToken cancellationToken = default)
        {
            Task.Run(() => this.StartAsync(cancellationToken));
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            this.externalCancellationToken = cancellationToken;
            this.externalCancellationToken.Register(() => this.localCancellationTokenSource.Cancel());

            while (!cancellationToken.IsCancellationRequested)
            {
                var (lowestNextTimeToRun, lowestIds) = this.GetScheduledTasksToRunAndHowLongToWait();

                var timeToWait = lowestNextTimeToRun.Subtract(this.dateTime.Now);

                var millisecondsDelay = (int)Math.Min(int.MaxValue, timeToWait.TotalMilliseconds);
                var isCancellationRequested = await Task.Delay(millisecondsDelay, this.localCancellationTokenSource.Token).ContinueWith(task =>
                {
                    return cancellationToken.IsCancellationRequested;
                });

                if (isCancellationRequested)
                {
                    return;
                }

                var startTime = this.dateTime.Now;
                var scheduledTasksToRun = this.scheduledTasks.Where(m => lowestIds.Contains(m.Id)).ToArray();

                this.RaiseNextEvent(startTime, scheduledTasksToRun);

                foreach (var scheduledTask in scheduledTasksToRun)
                {
                    if (this.localCancellationTokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    try
                    {
                        if (scheduledTask.Action is object)
                        {
                            scheduledTask.Action.Invoke(this.localCancellationTokenSource.Token);
                        }

                        if (scheduledTask.ActionTask is object)
                        {
                            await scheduledTask.ActionTask.Invoke(this.localCancellationTokenSource.Token);
                        }
                    }
                    catch (Exception e)
                    {
                        this.logger.LogError(e, "Failed to execute action");
                    }
                }

                if (this.dateTime.Now - startTime > this.oneMinute)
                {
                    this.logger.LogWarning("Execution took more than one minute");
                }
            }
        }

        private (DateTime, IReadOnlyCollection<Guid>) GetScheduledTasksToRunAndHowLongToWait()
        {
            var lowestNextTimeToRun = DateTime.MaxValue;
            var lowestIds = new List<Guid>();

            var now = this.dateTime.Now;

            foreach (var scheduledTask in this.scheduledTasks)
            {
                var nextTimeToRun = scheduledTask.CronExpression.GetNextTimeToRun(now);

                if (nextTimeToRun == default)
                {
                    continue;
                }

                if (nextTimeToRun < lowestNextTimeToRun)
                {
                    lowestIds.Clear();
                    lowestIds.Add(scheduledTask.Id);
                    lowestNextTimeToRun = (DateTime)nextTimeToRun;
                }
                else if (nextTimeToRun == lowestNextTimeToRun)
                {
                    lowestIds.Add(scheduledTask.Id);
                }
            }

            return (lowestNextTimeToRun, lowestIds);
        }

        private void RegisterLocalCancelationToken()
        {
            this.localCancellationTokenSource = new CancellationTokenSource();
            this.externalCancellationToken.Register(() => this.localCancellationTokenSource.Cancel());
        }

        public void ChangeScheduleAndResetScheduler(Guid id, CronExpression cronExpression)
        {
            this.scheduledTasks.Single(t => t.Id == id).SetCronExpression(cronExpression);

            this.ResetScheduler();
        }

        public void ChangeSchedulesAndResetScheduler(IEnumerable<(Guid Id, CronExpression CronExpression)> scheduleChanges)
        {
            foreach (var scheduleItem in scheduleChanges)
            {
                this.scheduledTasks.Single(t => t.Id == scheduleItem.Id).SetCronExpression(scheduleItem.CronExpression);
            }

            this.ResetScheduler();
        }

        private void ResetScheduler()
        {
            this.localCancellationTokenSource.Cancel();

            this.RegisterLocalCancelationToken();
        }

        public event EventHandler<ScheduledEventArgs> Next;

        private void RaiseNextEvent(DateTime signalTime, params ScheduledTask[] scheduledTasks)
        {
            try
            {
                Next?.Invoke(this, new ScheduledEventArgs(signalTime, scheduledTasks.Select(t => t.Id).ToArray()));
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "RaiseNextEvent failed with exception");
            }

        }

        public void Stop()
        {
            this.localCancellationTokenSource.Cancel();
        }
    }
}

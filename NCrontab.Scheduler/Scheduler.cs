using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DisplayService.Services;
using DisplayService.Services.Scheduling;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NCrontab.Scheduler.Internals;

namespace NCrontab.Scheduler
{
    public class Scheduler : IScheduler
    {
        private readonly List<ScheduledTaskInternal> scheduledTasks = new List<ScheduledTaskInternal>();
        private readonly IDateTime dateTime;
        private readonly ILogger<Scheduler> logger;
        private readonly TimeSpan oneMinute = new TimeSpan(0, 1, 0);
        private CancellationTokenSource localCancellationTokenSource = new CancellationTokenSource();
        private CancellationToken externalCancellationToken;

        public Scheduler()
            : this(new NullLogger<Scheduler>(), new SystemDateTime())
        {
        }

        public Scheduler(
            ILogger<Scheduler> logger,
            IDateTime dateTime)
        {
            this.dateTime = dateTime;
            this.logger = logger;
        }

        public void ScheduleTask(Guid id, string cronExpression, Func<CancellationToken, Task> func)
        {
            this.AddTask(id, CrontabSchedule.Parse(cronExpression), func);
        }

        public void AddTask(Guid id, CrontabSchedule cronExpression, Func<CancellationToken, Task> func)
        {
            var scheduledTask = new ScheduledTaskInternal(id, cronExpression, func);

            this.scheduledTasks.Add(scheduledTask);
        }

        public Guid ScheduleTask(string cronExpression, Func<CancellationToken, Task> func)
        {
            return this.AddTask(CrontabSchedule.Parse(cronExpression), func);
        }

        public Guid AddTask(CrontabSchedule cronExpression, Func<CancellationToken, Task> func)
        {
            var id = Guid.NewGuid();

            this.AddTask(id, cronExpression, func);

            return id;
        }

        public void ScheduleTask(Guid id, string cronExpression, Action<CancellationToken> action)
        {
            this.AddTask(id, CrontabSchedule.Parse(cronExpression), action);
        }

        public void AddTask(Guid id, CrontabSchedule cronExpression, Action<CancellationToken> action)
        {
            var scheduledTask = new ScheduledTaskInternal(id, cronExpression, action);

            this.scheduledTasks.Add(scheduledTask);
        }

        public Guid ScheduleTask(string cronExpression, Action<CancellationToken> action)
        {
            return this.AddTask(CrontabSchedule.Parse(cronExpression), action);
        }

        public Guid AddTask(CrontabSchedule cronExpression, Action<CancellationToken> action)
        {
            var id = Guid.NewGuid();

            this.AddTask(id, cronExpression, action);

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
                var now = this.dateTime.Now;
                var (nextOccurrence, taskIds) = this.GetScheduledTasksToRunAndHowLongToWait(now);

                if (taskIds.Count == 0)
                {
                    throw new InvalidOperationException(
                        $"Scheduler must have at leaste one future task." +
                        $"Use {nameof(this.AddTask)} methods to add tasks.");
                }

                var timeToWait = nextOccurrence.Subtract(now);

                this.logger.LogInformation(
                    $"Scheduling next event:{Environment.NewLine}" +
                    $" --> nextOccurrence={nextOccurrence:O}{Environment.NewLine}" +
                    $" --> timeToWait={timeToWait}{Environment.NewLine}" +
                    $" --> tasks: count=[{taskIds.Count}], ids={string.Join(", ", taskIds.Select(id => $"{id:B}"))}");

                var isCancellationRequested = await Task.Delay(timeToWait, this.localCancellationTokenSource.Token).ContinueWith(task =>
                {
                    return cancellationToken.IsCancellationRequested;
                });

                if (isCancellationRequested)
                {
                    return;
                }

                var startTime = this.dateTime.Now;
                this.logger.LogDebug($"Starting scheduled event... @ {startTime:O})");
                var scheduledTasksToRun = this.scheduledTasks.Where(m => taskIds.Contains(m.Id)).ToArray();

                this.RaiseNextEvent(startTime, scheduledTasksToRun);

                foreach (var scheduledTask in scheduledTasksToRun)
                {
                    this.logger.LogDebug($"Starting task with Id={scheduledTask.Id:B}...");
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
                        this.logger.LogError(e, $"Task with Id={scheduledTask.Id:B} failed with exception");
                    }
                }

                var endTime = this.dateTime.Now;
                var duration = endTime - startTime;
                this.logger.Log(
                    duration > this.oneMinute ? LogLevel.Warning : LogLevel.Debug,
                    $"Execution finished after {duration}");
            }
        }

        private (DateTime, IReadOnlyCollection<Guid>) GetScheduledTasksToRunAndHowLongToWait(DateTime now)
        {
            var lowestNextTimeToRun = DateTime.MaxValue;
            var lowestIds = new List<Guid>();

            foreach (var scheduledTask in this.scheduledTasks)
            {
                var nextTimeToRun = scheduledTask.CronExpression.GetNextOccurrence(now);

                if (nextTimeToRun == default)
                {
                    continue;
                }

                if (nextTimeToRun < lowestNextTimeToRun)
                {
                    lowestIds.Clear();
                    lowestIds.Add(scheduledTask.Id);
                    lowestNextTimeToRun = nextTimeToRun;
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

        public void ChangeScheduleAndResetScheduler(Guid id, CrontabSchedule cronExpression)
        {
            this.scheduledTasks.Single(t => t.Id == id).SetCronExpression(cronExpression);

            this.ResetScheduler();
        }

        public void ChangeSchedulesAndResetScheduler(IEnumerable<(Guid Id, CrontabSchedule CrontabSchedule)> scheduleChanges)
        {
            foreach (var scheduleItem in scheduleChanges)
            {
                this.scheduledTasks.Single(t => t.Id == scheduleItem.Id).SetCronExpression(scheduleItem.CrontabSchedule);
            }

            this.ResetScheduler();
        }

        private void ResetScheduler()
        {
            this.localCancellationTokenSource.Cancel();

            this.RegisterLocalCancelationToken();
        }

        public event EventHandler<ScheduledEventArgs> Next;

        private void RaiseNextEvent(DateTime signalTime, params ScheduledTaskInternal[] scheduledTasks)
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

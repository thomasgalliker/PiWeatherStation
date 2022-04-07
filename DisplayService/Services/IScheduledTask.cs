using System;
using System.Threading;
using System.Threading.Tasks;
using DisplayService.Services.Scheduling;

namespace DisplayService.Services
{
    public interface IScheduledTask
    {
        public CronExpression Schedule { get; }

        Task ExecuteAsync(CancellationToken cancellationToken);
    }

    public class ScheduledTask : IScheduledTask
    {
        private readonly Func<Task> taskFactory;

        public ScheduledTask(CronExpression schedule, Func<object> taskFactory)
        {
            this.Schedule = schedule;
            //this.taskFactory = taskFactory;
        }

        public ScheduledTask(CronExpression schedule, Func<Task<object>> taskFactory)
        {
            this.Schedule = schedule;
            this.taskFactory = taskFactory;
        }

        public CronExpression Schedule { get; }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await this.taskFactory();
        }
    }
}
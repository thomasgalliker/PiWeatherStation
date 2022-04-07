﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DisplayService.Services.Scheduling
{
    public class JobHandler
    {
        public Guid JobId { get; }

        private ICronJob cronJob;
        private IDateTime dateTime;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private Task runningTask;

        private CancellationTokenSource cancellationTokenSource;

        private CronExpression cronExpression;

        public JobHandler(Guid jobId, ICronJob cronJob, string cronExpression, IServiceScopeFactory serviceScopeFactory)
        {
            this.JobId = jobId;
            this.cronJob = cronJob;
            this.serviceScopeFactory = serviceScopeFactory;
            this.cronExpression = cronExpression;
        }

        public void Create<T>() where T : ICronJob
        {
            this.cancellationTokenSource = new CancellationTokenSource();

            this.runningTask = Task.Run(async () =>
            {
                await this.RunCronJob<T>();
            }
            , this.cancellationTokenSource.Token);
        }

        private async Task RunCronJob<T>() where T : ICronJob
        {
            using (var serviceScope = this.serviceScopeFactory.CreateScope())
            {
                this.cronJob = serviceScope.ServiceProvider.GetService<T>();
                this.dateTime = serviceScope.ServiceProvider.GetService<IDateTime>();

                while (!this.cancellationTokenSource.Token.IsCancellationRequested)
                {
                    var now = this.dateTime.Now;
                    var timeToNext = this.cronExpression.GetTimeToNext(now);

                    if (!timeToNext.HasValue)
                    {
                        return;
                    }

                    await Task.Delay((int)timeToNext.Value.TotalMilliseconds, this.cancellationTokenSource.Token);

                    await this.cronJob.RunJob(this.cancellationTokenSource.Token);
                }
            }
        }

        public void ChangeSchedule<T>(string cronExpression) where T : ICronJob
        {
            this.Stop();

            this.cronExpression = cronExpression;

            this.Create<T>();
        }


        public void Stop()
        {
            this.cancellationTokenSource.Cancel();

            this.runningTask.Wait(); // Really safe to do this??!!

            this.cancellationTokenSource.Dispose();
        }


    }
}
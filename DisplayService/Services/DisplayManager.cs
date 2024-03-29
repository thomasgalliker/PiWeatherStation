﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DisplayService.Devices;
using DisplayService.Model;
using Microsoft.Extensions.Logging;
using NCrontab;
using NCrontab.Scheduler;

namespace DisplayService.Services
{
    public class DisplayManager : IDisplayManager
    {
        private readonly IDictionary<Guid, IRenderActionFactory> renderingSetup = new Dictionary<Guid, IRenderActionFactory>();
        private readonly ILogger logger;
        private readonly IRenderService renderService;
        private readonly IDisplay display;
        private readonly IScheduler scheduler;
        private readonly ICacheService cacheService;

        private bool disposed;

        public DisplayManager(
            ILogger<DisplayManager> logger,
            IRenderService renderService,
            IDisplay display,
            ISchedulerFactory schedulerFactory,
            ICacheService cacheService)
        {
            this.logger = logger;
            this.renderService = renderService;
            this.display = display;
            this.scheduler = schedulerFactory.Create();
            this.cacheService = cacheService;

            this.scheduler.Next += this.OnNextSchedule;
        }

        public void AddRenderAction(Func<IRenderAction> renderAction)
        {
            var id = Guid.NewGuid();
            this.renderingSetup.Add(id, new SyncSingleRenderActionFactory(renderAction));
        }

        public void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions)
        {
            this.AddRenderActions(renderActions, null);
        }

        public void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions, CrontabSchedule cronExpression)
        {
            var id = this.ScheduleTaskIfNotNull(cronExpression);
            this.renderingSetup.Add(id, new SyncListRenderActionFactory(renderActions));
        }

        public void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions)
        {
            this.AddRenderActionsAsync(renderActions, null);
        }

        public void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions, CrontabSchedule cronExpression)
        {
            var id = this.ScheduleTaskIfNotNull(cronExpression);
            this.renderingSetup.Add(id, new AsyncRenderActionFactory(renderActions));
        }

        private Guid ScheduleTaskIfNotNull(CrontabSchedule cronExpression)
        {
            Guid id;
            if (cronExpression != null)
            {
                id = this.scheduler.AddTask(cronExpression, (c) => { });
            }
            else
            {
                id = Guid.NewGuid();
            }

            return id;
        }

        private async void OnNextSchedule(object sender, ScheduledEventArgs e)
        {
            this.logger.LogDebug($"OnNextSchedule with TaskIds=[{string.Join(", ", e.TaskIds.Select(t => $"{t:B}"))}]");

            try
            {
                var renderActions = await this.GetRenderActionsAsync(e.TaskIds);
                if (renderActions.Any())
                {
                    await this.UpdateDisplayAsync(renderActions);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"OnNextSchedule failed with exception: {ex.Message}");
            }
        }

        private async Task UpdateDisplayAsync(IReadOnlyCollection<IRenderAction> renderActions)
        {
            this.RunRenderActions(renderActions);

            await this.UpdateDisplayAsync();
        }

        private void RunRenderActions(IEnumerable<IRenderAction> renderActions)
        {
            // TODO: Lock parallel renderings

            var exceptions = new List<(Exception Exception, Type RenderActionType)>();

            foreach (var renderAction in renderActions)
            {
                try
                {
                    renderAction.Render(this.renderService);
                }
                catch (Exception ex)
                {
                    exceptions.Add((ex, renderAction.GetType()));
                }
            }

            if (exceptions.Any())
            {
                var ex = new AggregateException($"Rendering failed with exception: {string.Join(", ", exceptions.Select(e => e.RenderActionType.Name))}", exceptions.Select(e => e.Exception));
                this.logger.LogError(ex, "RunRenderActions failed with exception");
            }
        }

        private async Task UpdateDisplayAsync()
        {
            try
            {
                // Get rendered image from rendering service
                // and send it to the display
                using (var bitmapStream = this.renderService.GetScreen())
                {
                    this.display.DisplayImage(bitmapStream);
                    await this.cacheService.SaveToCache(bitmapStream);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"UpdateDisplay failed with exception: {ex.Message}");
            }
        }

        private async Task<IReadOnlyCollection<IRenderAction>> GetRenderActionsAsync(IReadOnlyCollection<Guid> scheduledTaskIds = null)
        {
            IEnumerable<KeyValuePair<Guid, IRenderActionFactory>> renderActionFactories;

            if (scheduledTaskIds == null)
            {
                renderActionFactories = this.renderingSetup;
            }
            else
            {
                renderActionFactories = this.renderingSetup.Where(s => scheduledTaskIds.Contains(s.Key));
            }

            var results = await Task.WhenAll(renderActionFactories.Select(x => x.Value.GetRenderActionsAsync()));
            var renderActions = results.SelectMany(x => x).ToList();
            return renderActions;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"StartAsync");

            try
            {
                this.renderService.Clear();

                var renderActions = await this.GetRenderActionsAsync();
                await this.UpdateDisplayAsync(renderActions);

                var tasks = this.scheduler.GetTasks().ToList();
                this.logger.LogInformation($"Starting scheduler with tasks.Count={tasks.Count}");
                this.scheduler.Start(cancellationToken);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"StartAsync failed with exception: {ex.Message}");

                if (Debugger.IsAttached)
                {
                    throw ex;
                }
            }
        }

        public void RemoveRenderingActions()
        {
            this.logger.LogInformation("RemoveRenderingActions");

            this.scheduler.Stop();

            var scheduledTasks = this.scheduler.GetTasks()
                .Where(t => this.renderingSetup.ContainsKey(t.Id))
                .ToArray();

            this.scheduler.RemoveTasks(scheduledTasks);

            // Remove existing rendering setups
            this.renderingSetup.Clear();

            // Clear display
            this.renderService.Clear();
        }

        public async Task ResetAsync()
        {
            this.logger.LogInformation("ResetAsync");

            this.RemoveRenderingActions();

            // Update display
            await this.UpdateDisplayAsync();
        }

        protected void Dispose(bool disposing)
        {
            this.logger.LogDebug("Dispose");

            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.scheduler.Next -= this.OnNextSchedule;
                this.scheduler.Dispose();

                this.renderingSetup.Clear();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

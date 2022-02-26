using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using DisplayService.Model;
using Microsoft.Extensions.Logging;

namespace DisplayService.Services
{
    public class DisplayManager : IDisplayManager
    {
        private readonly IDictionary<Guid, (ITimerService Timer, object RenderActions)> renderingSetup = new Dictionary<Guid, (ITimerService, object)>();
        private readonly ILogger<DisplayManager> logger;
        private readonly ICacheService cacheService;
        private readonly IRenderService renderService;
        private readonly IDisplay display;
        private readonly ITimerServiceFactory timerServiceFactory;
        private bool disposed;

        public DisplayManager(ILogger<DisplayManager> logger, IRenderService renderService, IDisplay display, ITimerServiceFactory timerServiceFactory)
        {
            this.logger = logger;
            this.display = display;
            this.timerServiceFactory = timerServiceFactory;
            this.cacheService = new CacheService();
            this.renderService = renderService;
        }

        public void AddRenderAction(Func<IRenderAction> renderAction)
        {
            this.AddRenderAction(renderAction, TimeSpan.Zero);
        }

        public void AddRenderAction(Func<IRenderAction> renderAction, TimeSpan updateInterval)
        {
            var updateTimer = this.CreateUpdateTimer(updateInterval);
            this.renderingSetup.Add(Guid.NewGuid(), new(updateTimer, renderAction));
        }

        public void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions)
        {
            this.AddRenderActions(renderActions, TimeSpan.Zero);
        }

        public void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions, TimeSpan updateInterval)
        {
            var updateTimer = this.CreateUpdateTimer(updateInterval);
            this.renderingSetup.Add(Guid.NewGuid(), new(updateTimer, renderActions));
        }

        public void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions)
        {
            this.AddRenderActionsAsync(renderActions, TimeSpan.Zero);
        }

        public void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions, TimeSpan updateInterval)
        {
            var updateTimer = this.CreateUpdateTimer(updateInterval);
            this.renderingSetup.Add(Guid.NewGuid(), new(updateTimer, renderActions));
        }

        private ITimerService CreateUpdateTimer(TimeSpan? updateInterval)
        {
            ITimerService updateTimer = null;

            if (updateInterval is TimeSpan updateIntervalValue && updateIntervalValue > TimeSpan.Zero)
            {
                updateTimer = this.timerServiceFactory.Create();
                updateTimer.Interval = updateIntervalValue;
                updateTimer.Elapsed += this.OnUpdateTimerElapsed;
            }

            return updateTimer;
        }

        private async void OnUpdateTimerElapsed(object sender, TimerElapsedEventArgs e)
        {
            try
            {
                var timer = (ITimerService)sender;
                var setup = this.renderingSetup.Single(r => r.Value.Timer == timer);
                var renderActionFactory = setup.Value.RenderActions;
                var renderActions = await GetRenderActionsAsync(renderActionFactory);
                this.UpdateDisplay(renderActions);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"OnUpdateTimerElapsed failed with exception: {ex.Message}");
            }
        }

        private void UpdateDisplay(IEnumerable<IRenderAction> renderActions)
        {
            try
            {
                // TODO: Lock parallel renderings

                // Send render actions to rendering service
                // in order to draw on canvas
                foreach (var renderAction in renderActions)
                {
                    renderAction.Render(this.renderService);
                }

                this.UpdateDisplay();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"UpdateDisplay failed with exception: {ex.Message}");
            }
        }

        private void UpdateDisplay()
        {
            try
            {
                // Get rendered image from rendering service
                // and send it to the display
                var bitmapStream = this.renderService.GetScreen(); // TODO: Use using/Dispose
                this.display.DisplayImage(bitmapStream);
                this.cacheService.SaveToCache(bitmapStream);
                bitmapStream.Close();
                bitmapStream.Dispose();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"UpdateDisplay failed with exception: {ex.Message}");
            }
        }

        private static async Task<IEnumerable<IRenderAction>> GetRenderActionsAsync(object renderActionFactory)
        {
            IEnumerable<IRenderAction> renderActions;

            if (renderActionFactory is Func<Task<IEnumerable<IRenderAction>>> asyncRenderActionsFactory)
            {
                renderActions = await asyncRenderActionsFactory();
            }
            else if (renderActionFactory is Func<IEnumerable<IRenderAction>> syncRenderActionsFactory)
            {
                renderActions = syncRenderActionsFactory();
            }
            else if (renderActionFactory is Func<IRenderAction> syncRenderActionFactory)
            {
                renderActions = new List<IRenderAction> { syncRenderActionFactory() };
            }
            //else if (renderActionFactory is IEnumerable<Func<IRenderAction>> syncEnumerableFuncRenderActions)
            //{
            //    renderActions = syncEnumerableFuncRenderActions.Select(func => func());
            //}
            else
            {
                throw new NotSupportedException($"Render action factory of type {renderActionFactory?.GetType().Name ?? "<null>"} is not supported");
            }

            return renderActions;
        }

        public async Task StartAsync()
        {
            this.logger.LogInformation($"Start rendering...");

            try
            {
                var timers = this.GetAllTimers();
                StopTimers(timers);

                var renderActionFactories = this.renderingSetup.Select(r => r.Value.RenderActions).ToList();
                var renderActionFactoryTasks = renderActionFactories.Select(renderActionFactory => GetRenderActionsAsync(renderActionFactory));
                var renderActions = (await Task.WhenAll(renderActionFactoryTasks)).SelectMany(ra => ra).ToList();
                this.renderService.Clear();
                this.UpdateDisplay(renderActions);

                if (TryForEach(timers, t => t.Start(), out var exceptions))
                {
                    throw new AggregateException("Start failed with exception", exceptions);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"StartAsync failed with exception: {ex.Message}");
            }
        }

        public void StopTimers()
        {
            this.logger.LogDebug("StopTimers");

            var timers = this.GetAllTimers();
            StopTimers(timers);
        }

        private static void StopTimers(IEnumerable<ITimerService> timers)
        {
            if (TryForEach(timers, t => t.Stop(), out var exceptions))
            {
                throw new AggregateException("Stop failed with exception", exceptions);
            }
        }

        public void Clear()
        {
            using (this.logger.BeginScope("Clear"))
            {
                this.logger.LogInformation("Clear");

                // Stop all timers
                this.StopTimers();

                // Remove existing rendering setups
                this.renderingSetup.Clear();

                // Clear display
                this.renderService.Clear();
                this.UpdateDisplay();
            }
        }

        private static bool TryForEach<T>(IEnumerable<T> items, Action<T> action, out IEnumerable<Exception> exceptions)
        {
            exceptions = new List<Exception>();

            foreach (var item in items)
            {
                try
                {
                    action(item);
                }
                catch (Exception ex)
                {
                    ((List<Exception>)exceptions).Add(ex);
                }
            }

            return exceptions.Any();
        }

        private IEnumerable<ITimerService> GetAllTimers()
        {
            return this.renderingSetup.Select(r => r.Value.Timer).Where(t => t != null);
        }

        protected void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                var timers = this.GetAllTimers();
                TryForEach(timers, t => t.Elapsed -= this.OnUpdateTimerElapsed, out _);
                TryForEach(timers, t => t.Dispose(), out _);
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisplayManager() => this.Dispose(false);
    }
}

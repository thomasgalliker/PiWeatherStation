using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using DisplayService.Model;
using DisplayService.Settings;

namespace DisplayService.Services
{
    public class DisplayManager : IDisplayManager
    {
        private readonly IDictionary<Guid, (ITimerService Timer, IEnumerable<Func<IRenderAction>> RenderActions)> renderingSetup = new Dictionary<Guid, (ITimerService, IEnumerable<Func<IRenderAction>>)>();
        private readonly ICacheService cacheService;
        private readonly IRenderService renderService;
        private readonly IDisplay display;
        private bool disposed;

        public DisplayManager(IRenderSettings renderSettings, IDisplay display)
        {
            this.cacheService = new CacheService();
            this.renderService = new RenderService((RenderSettings)renderSettings);
            this.display = display;
        }

        public void AddRenderActions(IEnumerable<Func<IRenderAction>> renderActions, TimeSpan? updateInterval = null)
        {
            if (updateInterval is TimeSpan updateIntervalValue)
            {
                ITimerService updateTimer = new TimerService
                {
                    Interval = updateIntervalValue,
                };
                updateTimer.Elapsed += this.OnUpdateTimerElapsed;

                this.renderingSetup.Add(Guid.NewGuid(), new(updateTimer, renderActions));
            }

        }

        private void OnUpdateTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var timer = (ITimerService)sender;
            var setup = this.renderingSetup.Single(r => r.Value.Timer == timer);
            var renderActions = setup.Value.RenderActions;
            this.UpdateDisplay(renderActions);
        }

        private void UpdateDisplay(IEnumerable<Func<IRenderAction>> renderActions)
        {
            try
            {
                // TODO: Lock parallel renderings
                foreach (var renderAction in renderActions)
                {
                    renderAction().Render(this.renderService);
                }

                var bitmapStream = this.renderService.GetScreen(); // TODO: Use using/Dispose
                this.display.DisplayImage(bitmapStream);
                this.cacheService.SaveToCache(bitmapStream);
                bitmapStream.Close(); // TODO Remove?
                bitmapStream.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occurred trying to update display. " + ex.Message);
            }
        }

        public void Start()
        {
            var renderActions = this.renderingSetup.SelectMany(r => r.Value.RenderActions).ToList();
            this.UpdateDisplay(renderActions);

            var timers = this.GetAllTimers();
            if (TryForEach(timers, t => t.Start(), out var exceptions))
            {
                throw new AggregateException("Start failed with exception", exceptions);
            }
        }

        public void Stop()
        {
            var timers = this.GetAllTimers();
            if (TryForEach(timers, t => t.Stop(), out var exceptions))
            {
                throw new AggregateException("Stop failed with exception", exceptions);
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
            return this.renderingSetup.Select(r => r.Value.Timer);
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

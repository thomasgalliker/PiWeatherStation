using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DisplayService.Model;

namespace DisplayService.Services
{
    public interface IDisplayManager : IDisposable
    {
        //void AddRenderAction(Func<IRenderAction> renderAction);

        //void AddRenderAction(TimeSpan? updateInterval, Func<IRenderAction> renderAction);

        void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions);

        void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions, TimeSpan updateInterval);

        void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions);

        void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions, TimeSpan updateInterval);

        /// <summary>
        /// Starts rendering the defined render actions and schedules the update timers (if defined).
        /// </summary>
        Task StartAsync();

        void Stop();
    }
}
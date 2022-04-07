using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Services.Scheduling;

namespace DisplayService.Services
{
    public interface IDisplayManager : IDisposable
    {
        void AddRenderAction(Func<IRenderAction> renderAction);

        void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions);

        void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions, CronExpression cronExpression);

        void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions);

        void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions, CronExpression cronExpression);

        /// <summary>
        /// Starts rendering the defined render actions and schedules the update timers (if defined).
        /// </summary>
        Task StartAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Clears the display.
        /// </summary>
        Task ClearAsync();

        /// <summary>
        /// Removes all render actions and clears the display.
        /// </summary>
        Task ResetAsync();
    }
}
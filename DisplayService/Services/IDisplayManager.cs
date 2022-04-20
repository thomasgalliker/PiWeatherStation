using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DisplayService.Model;
using NCrontab;

namespace DisplayService.Services
{
    public interface IDisplayManager : IDisposable
    {
        void AddRenderAction(Func<IRenderAction> renderAction);

        void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions);

        void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions, CrontabSchedule cronExpression);

        void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions);

        void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions, CrontabSchedule cronExpression);

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
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
        /// <summary>
        /// Adds a new rendering action.
        /// </summary>
        void AddRenderAction(Func<IRenderAction> renderAction);

        /// <summary>
        /// Adds a new collection of rendering actions.
        /// </summary>
        void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions);

        /// <summary>
        /// Adds a new collection of rendering actions with a schedule.
        /// </summary>
        void AddRenderActions(Func<IEnumerable<IRenderAction>> renderActions, CrontabSchedule cronExpression);

        /// <summary>
        /// Adds a new collection of rendering actions.
        /// </summary>
        void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions);

        /// <summary>
        /// Adds a new collection of rendering actions with a schedule.
        /// </summary>
        void AddRenderActionsAsync(Func<Task<IEnumerable<IRenderAction>>> renderActions, CrontabSchedule cronExpression);

        /// <summary>
        /// Starts rendering the defined render actions and schedules the update timers (if defined).
        /// </summary>
        Task StartAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes all rendering actions.
        /// </summary>
        void RemoveRenderingActions();

        /// <summary>
        /// Removes all rendering actions and clears the display.
        /// </summary>
        Task ResetAsync();
    }
}
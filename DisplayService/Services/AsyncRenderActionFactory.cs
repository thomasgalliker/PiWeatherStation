using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DisplayService.Model;

namespace DisplayService.Services
{
    internal class AsyncRenderActionFactory : IRenderActionFactory
    {
        private readonly Func<Task<IEnumerable<IRenderAction>>> renderActions;

        public AsyncRenderActionFactory(Func<Task<IEnumerable<IRenderAction>>> renderActions)
        {
            this.renderActions = renderActions;
        }

        public Task<IEnumerable<IRenderAction>> GetRenderActionsAsync()
        {
            return this.renderActions();
        }
    }
}
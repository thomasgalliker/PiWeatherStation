using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DisplayService.Model;

namespace DisplayService.Services
{
    internal class SyncSingleRenderActionFactory : IRenderActionFactory
    {
        private readonly Func<IRenderAction> renderAction;

        public SyncSingleRenderActionFactory(Func<IRenderAction> renderActions)
        {
            this.renderAction = renderActions;
        }

        public Task<IEnumerable<IRenderAction>> GetRenderActionsAsync()
        {
            var renderActions = new List<IRenderAction> { this.renderAction() };
            return Task.FromResult<IEnumerable<IRenderAction>>(renderActions);
        }
    }
}
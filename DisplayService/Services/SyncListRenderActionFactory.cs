using System;
using DisplayService.Model;

namespace DisplayService.Services
{
    internal class SyncListRenderActionFactory : IRenderActionFactory
    {
        private readonly Func<IEnumerable<IRenderAction>> renderActions;

        public SyncListRenderActionFactory(Func<IEnumerable<IRenderAction>> renderActions)
        {
            this.renderActions = renderActions;
        }

        public Task<IEnumerable<IRenderAction>> GetRenderActionsAsync()
        {
            return Task.FromResult(this.renderActions());
        }
    }
}
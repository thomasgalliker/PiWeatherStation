using System.Collections.Generic;
using DisplayService.Model;

namespace DisplayService.Services
{
    internal interface IRenderActionFactory
    {
        Task<IEnumerable<IRenderAction>> GetRenderActionsAsync();
    }
}
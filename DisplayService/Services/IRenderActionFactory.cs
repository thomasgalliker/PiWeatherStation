using System.Collections.Generic;
using System.Threading.Tasks;
using DisplayService.Model;

namespace DisplayService.Services
{
    internal interface IRenderActionFactory
    {
        Task<IEnumerable<IRenderAction>> GetRenderActionsAsync();
    }
}
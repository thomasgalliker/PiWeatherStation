using DisplayService.Services;

namespace DisplayService.Model
{
    public interface IRenderAction
    {
        void Render(IRenderService renderService);
    }
}
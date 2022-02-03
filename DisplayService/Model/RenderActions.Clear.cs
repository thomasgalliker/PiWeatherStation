using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        public class Clear : IRenderAction
        {
            public void Render(IRenderService renderService)
            {
                renderService.Clear();
            }
        }
    }
}

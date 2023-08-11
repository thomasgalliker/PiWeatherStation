using System.Diagnostics;
using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        [DebuggerDisplay("BitmapImage: X={X}, Y={Y}")]
        public class BitmapImage : StreamImage
        {
            public override void Render(IRenderService renderService)
            {
                renderService.Image(this);
            }
        }
    }
}

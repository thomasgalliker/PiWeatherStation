using System.Diagnostics;
using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        [DebuggerDisplay("SvgImage: X={X}, Y={Y}")]
        public class SvgImage : StreamImage
        {
            public override void Render(IRenderService renderService)
            {
                renderService.SvgImage(this);
            }
        }
    }
}

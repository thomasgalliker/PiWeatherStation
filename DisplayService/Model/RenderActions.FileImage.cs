using System.Diagnostics;
using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        [DebuggerDisplay("StreamImage: X={X}, Y={Y}")]
        public class FileImage : Image
        {
            /// <summary>
            /// Filename of the image to place on the screen.
            /// </summary>
            /// <example>/home/pi/welcome.png</example>
            public string Filename { get; set; }

            public override void Render(IRenderService renderService)
            {
                renderService.Image(this);
            }
        }
    }
}

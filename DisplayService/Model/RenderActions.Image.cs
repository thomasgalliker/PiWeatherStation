using System.Diagnostics;
using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        [DebuggerDisplay("StreamImage: X={X}, Y={Y}")]
        public abstract class Image : IRenderAction, ISurface
        {
            /// <summary>
            /// X coordinate to place the image
            /// </summary>
            /// <example>10</example>
            public int X { get; set; }

            /// <summary>
            /// Y coordinate to place the image
            /// </summary>
            /// <example>100</example>
            public int Y { get; set; }

            public int Width { get; set; } = -1;

            public int Height { get; set; } = -1;

            /// <summary>
            /// Text horizontal alignment.
            /// </summary>
            public HorizontalAlignment HorizontalAlignment { get; set; }

            /// <summary>
            /// Text vertical alignment.
            /// </summary>
            public VerticalAlignment VerticalAlignment { get; set; }

            public string BackgroundColor { get; set; }

            public void Render(IRenderService renderService)
            {
                renderService.Image(this);
            }
        }
    }
}

using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        public class Image : IRenderAction
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

            /// <summary>
            /// Filename of the image to place on the screen
            /// </summary>
            /// <example>/home/pi/welcome.png</example>
            public string Filename { get; set; }

            /// <summary>
            /// Delay screen update (optional)
            /// </summary>
            /// <example>false</example>
            public bool Delay { get; set; } = false;

            public void Render(IRenderService renderService)
            {
                renderService.Image(this);
            }
        }
    }
}

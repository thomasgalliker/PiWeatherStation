using System.IO;
using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        /// <summary>
        /// Loads a graphical image on the screen
        /// </summary>
        public class Graphic : IRenderAction
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
            /// Data stream to decode
            /// </summary>
            public Stream Data { get; set; }

            /// <summary>
            /// Delay screen update (optional)
            /// </summary>
            /// <example>false</example>
            public bool Delay { get; set; } = false;

            public void Render(IRenderService renderService)
            {
                renderService.Graphic(this);
            }
        }
    }
}

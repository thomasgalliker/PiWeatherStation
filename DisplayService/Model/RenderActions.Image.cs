using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        public abstract class Image : IRenderAction
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

            public void Render(IRenderService renderService)
            {
                renderService.Image(this);
            }
        }
    }
}

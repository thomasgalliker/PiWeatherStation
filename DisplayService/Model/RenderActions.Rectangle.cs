using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        public class Rectangle : IRenderAction
        {
            public int X { get; set; }

            public int Y { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }

            public HorizontalAlignment HorizontalAlignment { get; set; }

            public VerticalAlignment VerticalAlignment { get; set; }

            public string StrokeColor { get; set; } = "#FF000000";

            public float StrokeWidth { get; set; }

            public string BackgroundColor { get; set; } = "#00000000";

            public void Render(IRenderService renderService)
            {
                renderService.Rectangle(this);
            }
        }
    }
}

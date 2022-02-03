using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        /// <summary>
        /// Places text on the screen
        /// </summary>
        public class Text : IRenderAction
        {
            /// <summary>
            /// X coordinate to place the text.
            /// </summary>
            /// <example>10</example>
            public int X { get; set; }

            /// <summary>
            /// Y coordinate to place the text.
            /// </summary>
            /// <example>100</example>
            public int Y { get; set; }

            /// <summary>
            /// Text value to place
            /// </summary>
            /// <example>Welcome Home</example>
            public string Value { get; set; }

            /// <summary>
            /// Text horizontal alignment (-1 = Left, 0 = Center, 1 = Right, optional)
            /// </summary>
            /// <example>0</example>
            public int HorizAlign { get; set; } = -1;

            /// <summary>
            /// Text vertical alignment (-1 = Top, 0 = Middle, 1 = Bottom, optional)
            /// </summary>
            /// <example>0</example>
            public int VertAlign { get; set; } = -1;

            /// <summary>
            /// Filename or font family of the font to use (optional)
            /// </summary>
            /// <example>/home/pi/NotoSans-Black.ttf</example>
            public string Font { get; set; }

            public bool Bold { get; set; }

            /// <summary>
            /// Font size of the text (optional)
            /// </summary>
            /// <example>60</example>
            public float FontSize { get; set; } = 32;

            /// <summary>
            /// Font weight of the text (100 - 900, optional)
            /// </summary>
            /// <example>400</example>
            public int FontWeight { get; set; } = 400;

            /// <summary>
            /// Font width of the text (1 - 9, optional)
            /// </summary>
            /// <example>5</example>
            public int FontWidth { get; set; } = 5;

            /// <summary>
            /// Hex color string representing the color of the text (optional)
            /// </summary>
            /// <example>#000000</example>
            public string HexColor { get; set; } = "#000000";

            /// <summary>
            /// Delay screen update (optional)
            /// </summary>
            /// <example>false</example>
            public bool Delay { get; set; } = false;

            public void Render(IRenderService renderService)
            {
                renderService.Text(this);
            }
        }
    }
}

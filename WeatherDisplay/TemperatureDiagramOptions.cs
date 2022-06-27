using SkiaSharp;

namespace WeatherDisplay
{
    public class TemperatureDiagramOptions
    {
        public static readonly TemperatureDiagramOptions Default = new TemperatureDiagramOptions();

        public TemperatureDiagramOptions()
        {
            this.CircleRadius = 0;
            this.TextPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 12,
                IsAntialias = true,
                FakeBoldText = false,
                IsStroke = false,
                TextAlign = SKTextAlign.Left,
            };
        }

        public int CircleRadius { get; set; }

        public SKPaint TextPaint { get; set; }
    }
}
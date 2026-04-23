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
                IsAntialias = true,
                IsStroke = false,
            };
            this.TextFont = new SKFont
            {
                Size = 12,
                Embolden = false,
            };
        }

        public int CircleRadius { get; set; }

        public SKPaint TextPaint { get; set; }

        public SKFont TextFont { get; set; }
    }
}

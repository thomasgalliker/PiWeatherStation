using System;
using SkiaSharp;

namespace DisplayService.Settings
{
    public class RenderSettings : IRenderSettings
    {
        public RenderSettings()
        {
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int Rotation { get; private set; }

        public bool IsPortrait { get; private set; }

        public SKColor Background { get; set; }

        public SKColor Foreground { get; set; }

        public void Resize(int width, int height)
        {
            if (width < 1 || width > 9999)
            {
                // throw new ArgumentException("Width must be greater than 0 and less than 10000", nameof(width));
            }

            if (height < 1 || height > 9999)
            {
                // throw new ArgumentException("Height must be greater than 0 and less than 10000", nameof(height));
            }

            if (Rotation == 0 || Rotation == 180)
            {
                this.Width = width;
                this.Height = height;
                IsPortrait = false;
            }
            else if (Rotation == 90 || Rotation == 270)
            {
                this.Width = height;
                this.Height = width;
                IsPortrait = true;
            }
            else
            {
                throw new ArgumentException("Rotation must be 0, 90, 180 or 270");
            }
        }
    }
}

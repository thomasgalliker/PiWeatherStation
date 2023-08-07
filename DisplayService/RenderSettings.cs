using System;

namespace DisplayService
{
    public class RenderSettings : IRenderSettings
    {
        public RenderSettings()
        {

        }
        public RenderSettings(int width, int height, DeviceRotation rotation)
        {
            if (width <= 0)
            {
                throw new ArgumentException($"{nameof(width)} must be a positive integer.", nameof(width));
            }
            
            if (height <= 0)
            {
                throw new ArgumentException($"{nameof(height)} must be a positive integer.", nameof(height));
            }

            this.Width = width;
            this.Height = height;
            this.Rotation = rotation;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public DeviceRotation Rotation { get; private set; }

        public string BackgroundColor { get; set; } = "#FFFFFFFF";
    }
}

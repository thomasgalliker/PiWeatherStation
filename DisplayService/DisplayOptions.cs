using System;

namespace DisplayService
{
    public class DisplayOptions
    {
        private int width;
        private int height;

        public string DriverType { get; set; }

        public string Driver { get; set; }

        public int Width
        {
            get => this.width;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(this.Width)} must be a positive integer.", nameof(this.Width));
                }

                this.width = value;
            }
        }
        
        public int Height
        {
            get => this.height;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(this.Height)} must be a positive integer.", nameof(this.Height));
                }

                this.height = value;
            }
        }

        public DeviceRotation Rotation { get; set; }
    }
}

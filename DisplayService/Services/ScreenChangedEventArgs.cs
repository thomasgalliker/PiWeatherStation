using System;

namespace DisplayService.Services
{
    public class ScreenChangedEventArgs : EventArgs
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool Delay { get; set; }
    }
}

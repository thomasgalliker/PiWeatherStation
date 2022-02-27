using System.IO;
using DisplayService.Services;
using DisplayService.Extensions;

namespace WeatherDisplay.Tests
{
    public class TestDisplay : IDisplay
    {
        private Stream bitmapStream;

        public TestDisplay(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; }

        public int Height { get; }

        public void Clear()
        {
            this.bitmapStream = null;
        }

        public void DisplayImage(Stream bitmapStream)
        {
            this.bitmapStream = bitmapStream.CopyToAndRewindAsync().Result;
        }

        public void Dispose()
        {
            this.bitmapStream = null;
        }

        public Stream GetDisplayImage()
        {
            return this.bitmapStream.Rewind();
        }
    }
}
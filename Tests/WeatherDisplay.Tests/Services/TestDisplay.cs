using System.IO;
using DisplayService.Services;

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
            var memoryStream = new MemoryStream();
            bitmapStream.CopyTo(memoryStream);
            bitmapStream.Position = 0;

            this.bitmapStream = memoryStream;
        }

        public void Dispose()
        {
            this.bitmapStream = null;
        }

        public Stream GetDisplayImage()
        {
            this.bitmapStream.Position = 0;
            return this.bitmapStream;
        }
    }
}
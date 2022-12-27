using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DisplayService.Extensions;

namespace WeatherDisplay.Extensions
{
    internal static class ImageExtensions
    {
        public static Stream ToStream(this Image image)
        {
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Png);
            memoryStream.Rewind();
            return memoryStream;
        }
    }
}

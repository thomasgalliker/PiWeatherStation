using System.IO;
using SkiaSharp;

namespace WeatherDisplay.Tests.Extensions
{
    internal static class SKBitmapExtensions
    {
        internal static Stream ToStream(this SKBitmap bitmap)
        {
            var memoryStream = new MemoryStream();
            using (var wStream = new SKManagedWStream(memoryStream))
            {
                _ = bitmap.Encode(wStream, SKEncodedImageFormat.Png, 100);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}

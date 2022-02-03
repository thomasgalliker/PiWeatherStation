using System.IO;

namespace DisplayService.Services
{
    public class NullDisplayService : IDisplay
    {
        public NullDisplayService()
        {
        }

        public int Width => 800;

        public int Height => 480;

        public void Clear()
        {
        }

        public void DisplayImage(Stream bitmapStream)
        {
        }

        public void Dispose()
        {
        }
    }
}

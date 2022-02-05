
using SkiaSharp;

namespace DisplayService.Settings
{
    public interface IRenderSettings
    {
        int Width { get; }

        int Height { get; }

        bool IsPortrait { get; }

        int Rotation { get; }

        SKColor Background { get; }

        void Resize(int width, int height);
    }
}
namespace DisplayService.Settings
{
    public interface IRenderSettings
    {
        int Width { get; }

        int Height { get; }

        bool IsPortrait { get; }

        int Rotation { get; }

        string BackgroundColor { get; }

        void Resize(int width, int height);
    }
}
namespace DisplayService
{
    public interface IRenderSettings
    {
        int Width { get; }

        int Height { get; }

        DeviceRotation Rotation { get; }

        string BackgroundColor { get; }
    }
}
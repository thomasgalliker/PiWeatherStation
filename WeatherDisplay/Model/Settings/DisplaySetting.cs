namespace WeatherDisplay.Model.Settings
{
    public class DisplaySetting
    {
        public string DriverType { get; set; }

        public string Driver { get; set; }

        public int Width { get; set; } = 800;

        public int Height { get; set; } = 480;

        public int Rotation { get; set; }
    }
}
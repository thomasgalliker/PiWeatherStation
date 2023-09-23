using System.IO;
using System.Reflection;

namespace WeatherDisplay.Resources
{
    public static class Images
    {
        private static readonly Assembly Assembly = typeof(Images).Assembly;

        public static Stream GetSystemInfoPageBackground()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, "SystemInfoPageBackground.png");
        }

        public static Stream GetWeatherStation()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, "WeatherStation.png");
        }
    }
}

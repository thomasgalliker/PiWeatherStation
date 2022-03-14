using System.IO;
using System.Reflection;
using WeatherDisplay.Services;

namespace WeatherDisplay.Resources
{
    public static class Icons
    {
        private static readonly Assembly Assembly = typeof(HighContrastWeatherIconMapping).Assembly;

        public static Stream GetSunshine()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, "sunshine.png");
        }
        
        public static Stream GetFog()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, "fog.png");
        }
    }
}

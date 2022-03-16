using System.IO;
using System.Reflection;
using WeatherDisplay.Services;

namespace WeatherDisplay.Resources
{
    public static class Icons
    {
        private static readonly Assembly Assembly = typeof(HighContrastWeatherIconMapping).Assembly;

        public static Stream Sun()
        {
            return GetIcon("sun_96.png");
        }

        public static Stream Clouds1()
        {
            return GetIcon("clouds_1.png");
        }
        
        public static Stream Clouds2()
        {
            return GetIcon("clouds_2.png");
        }
        
        public static Stream Clouds3()
        {
            return GetIcon("clouds_3.png");
        }

        public static Stream Clouds4()
        {
            return GetIcon("clouds_4.png");
        }

        public static Stream Fog()
        {
            return GetIcon("fog.png");
        }
        
        public static Stream Mist()
        {
            return GetIcon("clouds_mist.png");
        }

        public static Stream Placeholder()
        {
            return GetIcon("placeholder.png");
        }
        
        public static Stream RainHeavy()
        {
            return GetIcon("rain_heavy.png");
        }
        
        public static Stream RainLight()
        {
            return GetIcon("rain_light.png");
        }
        
        public static Stream Snow()
        {
            return GetIcon("clouds_snow.png");
        }
        
        public static Stream SnowRain()
        {
            return GetIcon("snow_rain.png");
        }
        
        public static Stream Sunrise72()
        {
            return GetIcon("sunrise_72.png");
        }
        
        public static Stream Sunset72()
        {
            return GetIcon("sunset_72.png");
        }
        
        public static Stream TemperatureMinus()
        {
            return GetIcon("temp_minus.png");
        }
        
        public static Stream TemperaturePlus()
        {
            return GetIcon("temp_plus.png");
        }

        public static Stream Thunderstorm()
        {
            return GetIcon("thunderstorm.png");
        }
        
        public static Stream ThunderstormWithRain()
        {
            return GetIcon("thunderstorm_rain.png");
        }
        
        public static Stream Tornado()
        {
            return GetIcon("tornado.png");
        }
        
        public static Stream Wind()
        {
            return GetIcon("wind.png");
        }

        public static Stream GetIcon(string icon)
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, icon);
        }
    }
}

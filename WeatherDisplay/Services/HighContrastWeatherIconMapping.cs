using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DisplayService.Extensions;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Resources;

namespace WeatherDisplay.Services
{
    public class HighContrastWeatherIconMapping : IWeatherIconMapping
    {
        private readonly IDictionary<int, Func<Stream>> iconMapping;

        public HighContrastWeatherIconMapping()
        {
            this.iconMapping = new Dictionary<int, Func<Stream>>
            {
                // Group 2xx: Thunderstorm
                { 200, /*thunderstorm with light rain*/ () => Icons.ThunderstormWithRain() },
                { 201, /*thunderstorm with rain*/ () => Icons.ThunderstormWithRain() },
                { 202, /*thunderstorm with heavy rain*/ () => Icons.ThunderstormWithRain() },
                { 210, /*light thunderstorm*/ () => Icons.Thunderstorm() },
                { 211, /*thunderstorm*/ () => Icons.Thunderstorm() },
                { 212, /*heavy thunderstorm*/ () => Icons.Thunderstorm() },
                { 221, /*ragged thunderstorm*/ () => Icons.Thunderstorm() },
                { 230, /*thunderstorm with light drizzle*/ () => Icons.ThunderstormWithRain() },
                { 231, /*thunderstorm with drizzle*/ () => Icons.ThunderstormWithRain() },
                { 232, /*thunderstorm with heavy drizzle*/ () => Icons.ThunderstormWithRain() },

                // Group 3xx: Drizzle
                { 300, /*Drizzle light intensity drizzle*/ () => Icons.RainLight() },
                { 301, /*Drizzle drizzle*/ () => Icons.RainLight() },
                { 302, /*Drizzle heavy intensity drizzle*/ () => Icons.RainHeavy() },
                { 310, /*Drizzle light intensity drizzle rain*/ () => Icons.RainLight() },
                { 311, /*Drizzle drizzle rain*/ () => Icons.RainLight() },
                { 312, /*Drizzle heavy intensity drizzle rain*/ () => Icons.RainHeavy() },
                { 313, /*Drizzle shower rain and drizzle*/ () => Icons.RainLight() },
                { 314, /*Drizzle heavy shower rain and drizzle*/ () => Icons.RainHeavy() },
                { 321, /*Drizzle shower drizzle*/ () => Icons.RainLight() },
                    
                // Group 5xx: Rain
                { 500, /*Rain light rain*/ () => Icons.RainLight() },
                { 501, /*Rain moderate rain */ () => Icons.RainLight() },
                { 502, /*Rain heavy intensity rain  */ () => Icons.RainHeavy() },
                { 503, /*Rain very heavy rain   */ () => Icons.RainHeavy() },
                { 504, /*Rain extreme rain  */ () => Icons.RainHeavy() },
                { 511, /*Rain freezing rain */ () => Icons.RainHeavy() },
                { 520, /*Rain light intensity shower rain*/ () => Icons.RainHeavy() },
                { 521, /*Rain shower rain*/ () => Icons.RainHeavy() },
                { 522, /*Rain heavy intensity shower rain*/ () => Icons.RainHeavy() },
                { 531, /*Rain ragged shower rain*/ () => Icons.RainHeavy() },
                    
                // Group 6xx: Snow
                { 600, /*Snow light snow*/ () => Icons.Snow() },
                { 601, /*Snow Snow  */ () => Icons.Snow() },
                { 602, /*Snow Heavy snow*/ () => Icons.Snow() },
                { 611, /*Snow Sleet */ () => Icons.SnowRain() },
                { 612, /*Snow Light shower sleet*/ () => Icons.SnowRain() },
                { 613, /*Snow Shower sleet  */ () => Icons.SnowRain() },
                { 615, /*Snow Light rain and snow   */ () => Icons.SnowRain() },
                { 616, /*Snow Rain and snow */ () => Icons.SnowRain() },
                { 620, /*Snow Light shower snow */ () => Icons.Snow() },
                { 621, /*Snow Shower snow   */ () => Icons.Snow() },
                { 622, /*Snow Heavy shower snow */ () => Icons.Snow() },
                   
                // Group 7xx: Atmosphere
                { 701, /* Mist mist*/ () => Icons.Mist() },
                { 711, /* Smoke Smoke*/ () => Icons.Fog() },
                { 721, /* Haze Haze*/ () => Icons.Fog() },
                { 731, /* Dust sand/ dust whirls*/ () => Icons.Fog() },
                { 741, /* Fog fog*/ () => Icons.Fog() },
                { 751, /* Sand sand*/ () => Icons.Fog() },
                { 761, /* Dust dust*/ () => Icons.Fog() },
                { 762, /* Ash volcanic ash */ () => Icons.Fog() },
                { 771, /* Squall squalls*/ () => Icons.Wind() },
                { 781, /* Tornado tornado*/ () => Icons.Tornado() },

                // Group 800: Clear
                { 800, /* Clear clear sky */ () => Icons.Sun() },

                // Group 80x: Clouds
                { 801, /* Clouds few clouds: 11-25% */ () => Icons.Clouds1() },
                { 802, /* Clouds scattered clouds: 25-50% */ () => Icons.Clouds2() },
                { 803, /* Clouds broken clouds: 51-84% */ () => Icons.Clouds3() },
                { 804, /* Clouds overcast clouds: 85-100% */ () => Icons.Clouds4() },
            };
        }

        public Task<Stream> GetIconAsync(WeatherCondition weatherCondition)
        {
            Stream selectedImageStream;

            if (this.iconMapping.TryGetValue(weatherCondition.Id, out var imageFactory))
            {
                selectedImageStream = imageFactory();
            }
            else
            {
                selectedImageStream = Icons.Placeholder();
            }

            return Task.FromResult(selectedImageStream.Rewind());
        }
    }
}
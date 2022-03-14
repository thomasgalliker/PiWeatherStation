using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DisplayService.Extensions;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Resources;

namespace WeatherDisplay.Services
{
    public class HighContrastWeatherIconMapping : IWeatherIconMapping
    {
        private readonly IDictionary<WeatherConditionType, Func<Stream>> icons;

        public HighContrastWeatherIconMapping()
        {
            this.icons = new Dictionary<WeatherConditionType, Func<Stream>>
            {
                { WeatherConditionType.Fog, () => Icons.GetFog() },
                { WeatherConditionType.Clear, () => Icons.GetSunshine() },
            };
        }

        public async Task<Stream> GetIconAsync(WeatherCondition weatherCondition)
        {
            Stream selectedImageStream;

            if (this.icons.TryGetValue(weatherCondition.Type, out var imageFactory))
            {
                selectedImageStream = imageFactory();
            }
            else
            {
                selectedImageStream = Icons.GetSunshine();
            }

            return selectedImageStream.Rewind();
        }
    }
}
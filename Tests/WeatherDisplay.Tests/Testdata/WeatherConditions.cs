using System.Collections.Generic;
using OpenWeatherMap.Models;

namespace WeatherDisplay.Tests.Testdata
{
    internal static class WeatherConditions
    {
        internal static IEnumerable<WeatherCondition> GetTestWeatherConditions()
        {
            for (var id = 200; id <= 804; id++)
            {
                yield return GetTestWeatherCondition(id);
            }
        }

        internal static WeatherCondition GetTestWeatherCondition(int id)
        {
            return new WeatherCondition
            {
                Id = id,
                IconId = $"{id}",
                Type = WeatherConditionType.Mist,
                Description = "Mist",
            };
        }

    }
}

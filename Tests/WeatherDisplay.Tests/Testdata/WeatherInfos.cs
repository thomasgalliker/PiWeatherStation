using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenWeatherMap;
using OpenWeatherMap.Models;
using UnitsNet;

namespace WeatherDisplay.Tests.Testdata
{
    internal static class WeatherInfos
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = OpenWeatherMapService.GetJsonSerializerSettings("metric");

        internal static WeatherInfo GetTestWeatherInfo()
        {
            return GetTestWeatherInfo(Temperature.FromDegreesCelsius(5.5d));
        }

        internal static string GetTestWeatherInfoJson()
        {
            var weatherInfo = GetTestWeatherInfo();
            var weatherInfoJson = JsonConvert.SerializeObject(weatherInfo, JsonSerializerSettings);
            return weatherInfoJson;
        }

        internal static WeatherInfo GetTestWeatherInfo(Temperature mainTemperature)
        {
            return new WeatherInfo
            {
                CityName = "Test Location",
                Date = new DateTime(2000, 1, 1, 12, 13, 14, DateTimeKind.Local),
                Main = new TemperatureInfo
                {
                    Temperature = mainTemperature,
                    Humidity = RelativeHumidity.FromPercent(35),
                    Pressure = Pressure.FromHectopascals(998),
                    FeelsLike = mainTemperature,
                    MinimumTemperature = new Temperature(mainTemperature.Value - 10, mainTemperature.Unit),
                    MaximumTemperature = new Temperature(mainTemperature.Value + 10, mainTemperature.Unit),
                },
                Weather = new List<WeatherCondition>
                {
                    new WeatherCondition
                    {
                        Id = 1,
                        Description = "Klarer Himmel",
                        IconId = "09d",
                        Type = WeatherConditionType.Clear,
                    },
                },
                AdditionalInformation = new AdditionalWeatherInfo
                {
                    Sunrise = new DateTime(2000, 1, 1, 7, 0, 0, DateTimeKind.Local),
                    Sunset = new DateTime(2000, 1, 1, 20, 0, 0, DateTimeKind.Local),
                }
            };
        }

    }
}

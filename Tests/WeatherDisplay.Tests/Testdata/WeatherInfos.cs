﻿using System;
using System.Collections.Generic;
using WeatherDisplay.Model.OpenWeatherMap;

namespace WeatherDisplay.Tests.Testdata
{
    internal static class WeatherInfos
    {
        internal static WeatherInfo GetTestWeatherInfo()
        {
            return GetTestWeatherInfo(Temperature.FromCelsius(5.5d));
        }

        internal static WeatherInfo GetTestWeatherInfo(Temperature mainTemperature)
        {
            return new WeatherInfo
            {
                Name = "Test Location",
                Date = new DateTime(2000, 1, 1, 12, 13, 14, DateTimeKind.Local),
                Main = new TemperatureInfo
                {
                    Temperature = mainTemperature,
                    MinimumTemperature = mainTemperature - 10,
                    MaximumTemperature = mainTemperature + 10,
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

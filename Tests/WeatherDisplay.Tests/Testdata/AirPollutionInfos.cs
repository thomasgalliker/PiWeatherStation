using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using OpenWeatherMap.Models;

namespace WeatherDisplay.Tests.Testdata
{
    internal static class AirPollutionInfos
    {
        internal static string GetTestAirPollutionInfoJson()
        {
            var weatherInfo = GetTestAirPollutionInfo();
            var weatherInfoJson = JsonConvert.SerializeObject(weatherInfo);
            return weatherInfoJson;
        }

        internal static AirPollutionInfo GetTestAirPollutionInfo()
        {
            return new AirPollutionInfo
            {
                Coordinates = new Coordinates
                {
                    Latitude = 47.0907d,
                    Longitude = 8.0559d
                },
                Items = new List<AirPollutionInfoItem>
                {
                    new AirPollutionInfoItem
                    {
                        DateTime = DateTime.ParseExact("2022-05-05T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Main = new AirQualitySummary
                        {
                            AirQuality =AirQuality.Good
                        },
                        Components = new AirConcentrationComponents
                        {
                            CarbonMonoxide = 230.31d,
                            NitrogenMonoxide = 0d,
                            NitrogenDioxide = 4.54d,
                            Ozone = 76.53d,
                            SulphurDioxide = 1.74d,
                            FineParticulateMatter = 5.87d,
                            CoarseParticulateMatter = 6.05d,
                            Ammonia = 0.91d
                        }
                    }
                }
            };
        }
    }
}

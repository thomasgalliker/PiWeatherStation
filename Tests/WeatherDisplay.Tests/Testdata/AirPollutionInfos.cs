using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using OpenWeatherMap.Models;
using UnitsNet;
using UnitsNet.Units;

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
                            CarbonMonoxide = new MassConcentration(230.31d, MassConcentrationUnit.MicrogramPerCubicMeter),
                            NitrogenMonoxide = new MassConcentration(0d, MassConcentrationUnit.MicrogramPerCubicMeter),
                            NitrogenDioxide = new MassConcentration(4.54d, MassConcentrationUnit.MicrogramPerCubicMeter),
                            Ozone = new MassConcentration(76.53d, MassConcentrationUnit.MicrogramPerCubicMeter),
                            SulphurDioxide = new MassConcentration(1.74d, MassConcentrationUnit.MicrogramPerCubicMeter),
                            FineParticulateMatter = new MassConcentration(5.87d, MassConcentrationUnit.MicrogramPerCubicMeter),
                            CoarseParticulateMatter = new MassConcentration(6.05d, MassConcentrationUnit.MicrogramPerCubicMeter),
                            Ammonia = new MassConcentration( 0.91d, MassConcentrationUnit.MicrogramPerCubicMeter),
                        }
                    }
                }
            };
        }
    }
}

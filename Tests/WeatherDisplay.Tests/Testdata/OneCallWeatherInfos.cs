using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Tests.Testdata
{
    internal static class OneCallWeatherInfos
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = CreateMetricJsonSerializerSettings();

        private static JsonSerializerSettings CreateMetricJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new CelsiusTemperatureJsonConverter());
            return settings;
        }

        internal static string GetTestWeatherInfoJson()
        {
            var weatherInfo = GetTestWeatherInfo();
            var weatherInfoJson = JsonConvert.SerializeObject(weatherInfo, JsonSerializerSettings);
            return weatherInfoJson;
        }

        internal static OneCallWeatherInfo GetTestWeatherInfo()
        {
            return new OneCallWeatherInfo
            {
                Latitude = 47.1824d,
                Longitude = 8.4611d,
                Timezone = "Europe/Zurich",
                TimezoneOffset = 3600,
                CurrentWeather = new CurrentWeatherForecast
                {
                    Temperature = new Temperature(25, TemperatureUnit.Celsius),
                    FeelsLike = new Temperature(26, TemperatureUnit.Celsius),
                    Pressure = new Pressure(1000),
                    Humidity = new Humidity(50),
                    DewPoint = new Temperature(3.4, TemperatureUnit.Celsius),
                    UVIndex = new UVIndex(6.5d),
                    Weather = WeatherConditions.GetTestWeatherConditions().Take(1).ToList(),
                },
                DailyForecasts = new List<DailyWeatherForecast>
                {
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-17T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-17T06:35:39.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-17T18:33:32.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-17T17:38:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-17T06:41:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = 0.47d,
                        Temperature = new DailyTemperatureForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                            Min = new Temperature(6.32d, TemperatureUnit.Celsius),
                            Max = new Temperature(15.85d, TemperatureUnit.Celsius),
                            Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                            Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                            Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                            Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        Pressure = 1025,
                        Humidity = 63,
                        DewPoint = new Temperature(7.7d, TemperatureUnit.Celsius),
                        WindSpeed = 3.85d,
                        WindDirectionDegrees = 42,
                        WindGust = 7.82d,
                        Weather = new List<WeatherCondition>
                        {
                            new WeatherCondition
                            {
                                Description = "Überwiegend bewölkt",
                                IconId = "04d",
                                Id = 803,
                                Type = WeatherConditionType.Clouds
                            }
                        },
                        Clouds = 68,
                        Pop = 1d / 3d,
                        Rain = 11.923423d,
                        UVIndex = 2.51d,
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-18T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-18T06:33:38.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-18T18:34:57.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-18T18:52:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-18T07:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = 0.5d,
                        Temperature = new DailyTemperatureForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Min = new Temperature(6.32d, TemperatureUnit.Celsius),
                        Max = new Temperature(15.85d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        Pressure = 1034,
                        Humidity = 77,
                        DewPoint = new Temperature(3.45d, TemperatureUnit.Celsius),
                        WindSpeed = 5.02d,
                        WindDirectionDegrees = 60,
                        WindGust = 9.32d,
                        Weather = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Description = "Bedeckt",
                            IconId = "04d",
                            Id = 804,
                            Type = WeatherConditionType.Clouds
                        }
                        },
                        Clouds = 100,
                        Pop = 0.08d,
                        UVIndex = 2.37d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-19T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-19T06:31:37.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-19T18:36:21.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-19T20:08:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-19T07:19:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = 0.54d,
                        Temperature = new DailyTemperatureForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Min = new Temperature(6.32d, TemperatureUnit.Celsius),
                        Max = new Temperature(15.85d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        Pressure = 1029,
                        Humidity = 59,
                        DewPoint = new Temperature(1.55d, TemperatureUnit.Celsius),
                        WindSpeed = 3.58d,
                        WindDirectionDegrees = 61,
                        WindGust = 7.48d,
                        Weather = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Description = "Mäßig bewölkt",
                            IconId = "03d",
                            Id = 802,
                            Type = WeatherConditionType.Clouds
                        }
                        },
                        Clouds = 32,
                        Pop = 0.32d,
                        UVIndex = 2.99d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-20T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-20T06:29:36.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-20T18:37:44.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-20T21:26:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-20T07:38:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = 0.58d,
                        Temperature = new DailyTemperatureForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Min = new Temperature(6.32d, TemperatureUnit.Celsius),
                        Max = new Temperature(15.85d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        Pressure = 1026,
                        Humidity = 34,
                        DewPoint = new Temperature(-2.23d, TemperatureUnit.Celsius),
                        WindSpeed = 2.38d,
                        WindDirectionDegrees = 155,
                        WindGust = 3.04d,
                        Weather = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Description = "Überwiegend bewölkt",
                            IconId = "04d",
                            Id = 803,
                            Type = WeatherConditionType.Clouds
                        }
                        },
                        Clouds = 69,
                        Pop = 0.18d,
                        UVIndex = 2.24d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-21T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-21T06:27:35.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-21T18:39:08.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-21T22:45:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-21T07:59:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = 0.61d,
                        Temperature = new DailyTemperatureForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                            Min = new Temperature(6.32d, TemperatureUnit.Celsius),
                            Max = new Temperature(15.85d, TemperatureUnit.Celsius),
                            Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                            Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                            Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                            Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        Pressure = 1030,
                        Humidity = 35,
                        DewPoint = new Temperature(-1.93d, TemperatureUnit.Celsius),
                        WindSpeed = 2.11d,
                        WindDirectionDegrees = 158,
                        WindGust = 2.51d,
                        Weather = new List<WeatherCondition>
                        {
                            new WeatherCondition
                            {
                                Description = "Klarer Himmel",
                                IconId = "01d",
                                Id = 800,
                                Type = WeatherConditionType.Clear
                            }
                        },
                        Clouds = 1,
                        Pop = 0d,
                        UVIndex = 3.58d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-22T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-22T06:25:34.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-22T18:40:32.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("1970-01-01T01:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-22T08:24:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = 0.65d,
                        Temperature = new DailyTemperatureForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Min = new Temperature(6.32d, TemperatureUnit.Celsius),
                        Max = new Temperature(15.85d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        Pressure = 1027,
                        Humidity = 41,
                        DewPoint = new Temperature(-0.3d, TemperatureUnit.Celsius),
                        WindSpeed = 2d,
                        WindDirectionDegrees = 163,
                        WindGust = 1.92d,
                        Weather = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Description = "Klarer Himmel",
                            IconId = "01d",
                            Id = 800,
                            Type = WeatherConditionType.Clear
                        }
                        },
                        Clouds = 0,
                        Pop = 0d,
                        UVIndex = 4d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-23T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-23T06:23:33.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-23T18:41:55.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-23T00:06:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-23T08:56:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = 0.69d,
                        Temperature = new DailyTemperatureForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Min = new Temperature(6.32d, TemperatureUnit.Celsius),
                        Max = new Temperature(15.85d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        Pressure = 1025,
                        Humidity = 30,
                        DewPoint = new Temperature(-3.29d, TemperatureUnit.Celsius),
                        WindSpeed = 1.9d,
                        WindDirectionDegrees = 163,
                        WindGust = 1.85d,
                        Weather = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Description = "Klarer Himmel",
                            IconId = "01d",
                            Id = 800,
                            Type = WeatherConditionType.Clear
                        }
                        },
                        Clouds = 0,
                        Pop = 0d,
                        UVIndex = 4d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-24T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-24T06:21:32.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-24T18:43:19.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-24T01:26:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-24T09:38:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = 0.72d,
                        Temperature = new DailyTemperatureForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Min = new Temperature(6.32d, TemperatureUnit.Celsius),
                        Max = new Temperature(15.85d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Eve = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morn = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        Pressure = 1025,
                        Humidity = 37,
                        DewPoint = new Temperature(0.06d, TemperatureUnit.Celsius),
                        WindSpeed = 1.81d,
                        WindDirectionDegrees = 173,
                        WindGust = 1.62d,
                        Weather = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Description = "Klarer Himmel",
                            IconId = "01d",
                            Id = 800,
                            Type = WeatherConditionType.Clear
                        }
                        },
                        Clouds = 0,
                        Pop = 0d,
                        UVIndex = 4d
                    }
                },
                Alerts = new List<AlertInfo>
                {
                    new AlertInfo
                      {
                        SenderName = "DHMZ Državni hidrometeorološki zavod",
                        EventName = "Yellow wind warning",
                        StartTime = DateTime.ParseExact("2022-04-21T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        EndTime = DateTime.ParseExact("2022-04-21T21:59:59.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Description = "Gale force gusts of SE wind. maximum gust speed 65-90 km/h"
                      },
                      new AlertInfo
                      {
                        SenderName = "DHMZ Državni hidrometeorološki zavod",
                        EventName = "Yellow wind warning",
                        StartTime = DateTime.ParseExact("2022-04-21T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        EndTime = DateTime.ParseExact("2022-04-22T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Description = "Gale force jugo (SE wind).  maximum gust speed 55-90 km/h"
                      },
                      new AlertInfo
                      {
                        SenderName = "DHMZ Državni hidrometeorološki zavod",
                        EventName = "Yellow thunderstorm warning",
                        StartTime = DateTime.ParseExact("2022-04-22T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        EndTime = DateTime.ParseExact("2022-04-22T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Description = "Thundershowers. lightning risk > 80 %"
                      }
                }
            };
        }
    }
}

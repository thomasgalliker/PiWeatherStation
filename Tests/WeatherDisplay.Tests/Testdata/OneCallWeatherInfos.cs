using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using OpenWeatherMap;
using OpenWeatherMap.Models;

namespace WeatherDisplay.Tests.Testdata
{
    internal static class OneCallWeatherInfos
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = OpenWeatherMapService.GetJsonSerializerSettings("metric");

        internal static string GetTestWeatherInfoJson()
        {
            var weatherInfo = GetTestWeatherInfo();
            var weatherInfoJson = JsonConvert.SerializeObject(weatherInfo, JsonSerializerSettings);
            return weatherInfoJson;
        }

        internal static OneCallWeatherInfo GetTestWeatherInfo2()
        {
            return new OneCallWeatherInfo
            {
                Latitude = 47.0907d,
                Longitude = 8.0559d,
                Timezone = "Europe/Zurich",
                TimezoneOffset = 7200,
                CurrentWeather = new CurrentWeatherForecast
                {
                    DateTime = DateTime.ParseExact("2022-06-14T20:05:49.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                    Sunrise = DateTime.ParseExact("2022-06-14T03:31:53.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                    Sunset = DateTime.ParseExact("2022-06-14T19:24:17.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                    Temperature = new Temperature(18.01, TemperatureUnit.Celsius),
                    FeelsLike = new Temperature(17.54, TemperatureUnit.Celsius),
                    Pressure = new Pressure(1017),
                    Humidity = new Humidity(64),
                    DewPoint = new Temperature(11.11, TemperatureUnit.Celsius),
                    UVIndex = new UVIndex(0),
                    Clouds = 73,
                    Visibility = 10000,
                    WindSpeed = 0d,
                    WindDirectionDegrees = 0,
                    WindGust = 0d,
                    Weather = new List<WeatherCondition>
                    {
                      new WeatherCondition
                      {
                        Id = 803,
                        Type = WeatherConditionType.Clouds,
                        Description = "Überwiegend bewölkt",
                        IconId = "04n"
                      }
                    }
                },
                MinutelyForecasts = new List<MinutelyWeatherForecast>
                  {
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:06:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:07:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:08:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:09:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:10:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:11:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:12:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:13:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:14:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:15:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:16:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:17:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:18:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:19:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:20:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:21:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:22:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:23:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:24:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:25:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:26:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:27:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:28:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:29:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:30:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:31:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:32:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:33:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:34:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:35:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:36:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:37:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:38:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:39:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:40:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:41:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:42:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:43:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:44:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:45:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:46:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:47:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:48:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:49:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:50:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:51:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:52:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:53:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:54:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:55:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:56:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:57:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:58:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:59:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:01:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:02:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:03:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:04:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:05:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:06:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = 0d
                    }
                  },
                HourlyForecasts = new List<HourlyWeatherForecast>
                  {
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(18.01, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(17.54, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1017),
                      Humidity = new Humidity(64),
                      DewPoint = new Temperature(11.11, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 73,
                      Visibility = 10000,
                      WindSpeed = 1.2d,
                      WindDirectionDegrees = 142,
                      WindGust = 1.33d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 803,
                          Type = WeatherConditionType.Clouds,
                          Description = "Überwiegend bewölkt",
                          IconId = "04n"
                        }
                      },
                      Pop = 0.03d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(17.32, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(16.88, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1017),
                      Humidity = new Humidity(68),
                      DewPoint = new Temperature(11.36, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 70,
                      Visibility = 10000,
                      WindSpeed = 1.29d,
                      WindDirectionDegrees = 160,
                      WindGust = 1.35d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 803,
                          Type = WeatherConditionType.Clouds,
                          Description = "Überwiegend bewölkt",
                          IconId = "04n"
                        }
                      },
                      Pop = 0.03d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(16.49, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(16.07, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1017),
                      Humidity = new Humidity(72),
                      DewPoint = new Temperature(11.43, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 61,
                      Visibility = 10000,
                      WindSpeed = 1.38d,
                      WindDirectionDegrees = 167,
                      WindGust = 1.34d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 803,
                          Type = WeatherConditionType.Clouds,
                          Description = "Überwiegend bewölkt",
                          IconId = "04n"
                        }
                      },
                      Pop = 0.01d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(15.6, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(15.17, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1018),
                      Humidity = new Humidity(75),
                      DewPoint = new Temperature(11.19, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 50,
                      Visibility = 10000,
                      WindSpeed = 1.45d,
                      WindDirectionDegrees = 176,
                      WindGust = 1.34d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 802,
                          Type = WeatherConditionType.Clouds,
                          Description = "Mäßig bewölkt",
                          IconId = "03n"
                        }
                      },
                      Pop = 0d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14.66, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(14.19, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1018),
                      Humidity = new Humidity(77),
                      DewPoint = new Temperature(10.68, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 38,
                      Visibility = 10000,
                      WindSpeed = 1.52d,
                      WindDirectionDegrees = 198,
                      WindGust = 1.36d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 802,
                          Type = WeatherConditionType.Clouds,
                          Description = "Mäßig bewölkt",
                          IconId = "03n"
                        }
                      },
                      Pop = 0d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.71, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(13.23, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1019),
                      Humidity = new Humidity(80),
                      DewPoint = new Temperature(9.43, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 17,
                      Visibility = 10000,
                      WindSpeed = 1.75d,
                      WindDirectionDegrees = 196,
                      WindGust = 1.58d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 801,
                          Type = WeatherConditionType.Clouds,
                          Description = "Ein paar Wolken",
                          IconId = "02n"
                        }
                      },
                      Pop = 0.03d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.7, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(13.19, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1019),
                      Humidity = new Humidity(79),
                      DewPoint = new Temperature(9.18, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 11,
                      Visibility = 10000,
                      WindSpeed = 1.9d,
                      WindDirectionDegrees = 189,
                      WindGust = 1.7d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 801,
                          Type = WeatherConditionType.Clouds,
                          Description = "Ein paar Wolken",
                          IconId = "02n"
                        }
                      },
                      Pop = 0.01d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.77, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(13.21, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1019),
                      Humidity = new Humidity(77),
                      DewPoint = new Temperature(8.99, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 8,
                      Visibility = 10000,
                      WindSpeed = 1.72d,
                      WindDirectionDegrees = 179,
                      WindGust = 1.47d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 800,
                          Type = WeatherConditionType.Clear,
                          Description = "Klarer Himmel",
                          IconId = "01n"
                        }
                      },
                      Pop = 0d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.96, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(13.42, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1018),
                      Humidity = new Humidity(77),
                      DewPoint = new Temperature(9.14, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 6,
                      Visibility = 10000,
                      WindSpeed = 1.51d,
                      WindDirectionDegrees = 190,
                      WindGust = 1.4d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 800,
                          Type = WeatherConditionType.Clear,
                          Description = "Klarer Himmel",
                          IconId = "01d"
                        }
                      },
                      Pop = 0d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(15.86, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(15.51, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1018),
                      Humidity = new Humidity(77),
                      DewPoint = new Temperature(10.83, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0.34),
                      Clouds = 6,
                      Visibility = 10000,
                      WindSpeed = 1.27d,
                      WindDirectionDegrees = 199,
                      WindGust = 1.29d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 800,
                          Type = WeatherConditionType.Clear,
                          Description = "Klarer Himmel",
                          IconId = "01d"
                        }
                      },
                      Pop = 0d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(19.04, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(18.75, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1018),
                      Humidity = new Humidity(67),
                      DewPoint = new Temperature(11.93, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(1.01),
                      Clouds = 6,
                      Visibility = 10000,
                      WindSpeed = 0.73d,
                      WindDirectionDegrees = 216,
                      WindGust = 1.17d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 800,
                          Type = WeatherConditionType.Clear,
                          Description = "Klarer Himmel",
                          IconId = "01d"
                        }
                      },
                      Pop = 0d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(21.42, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(21.18, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1018),
                      Humidity = new Humidity(60),
                      DewPoint = new Temperature(12.48, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(2.18),
                      Clouds = 19,
                      Visibility = 10000,
                      WindSpeed = 0.88d,
                      WindDirectionDegrees = 286,
                      WindGust = 1.46d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 801,
                          Type = WeatherConditionType.Clouds,
                          Description = "Ein paar Wolken",
                          IconId = "02d"
                        }
                      },
                      Pop = 0.14d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(23.33, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(23.13, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1018),
                      Humidity = new Humidity(54),
                      DewPoint = new Temperature(12.54, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(3.82),
                      Clouds = 26,
                      Visibility = 10000,
                      WindSpeed = 0.98d,
                      WindDirectionDegrees = 289,
                      WindGust = 3.1d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 0.3d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(25.57, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(25.36, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1018),
                      Humidity = new Humidity(45),
                      DewPoint = new Temperature(12.1, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(5.63),
                      Clouds = 29,
                      Visibility = 10000,
                      WindSpeed = 1.07d,
                      WindDirectionDegrees = 299,
                      WindGust = 3.32d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 802,
                          Type = WeatherConditionType.Clouds,
                          Description = "Mäßig bewölkt",
                          IconId = "03d"
                        }
                      },
                      Pop = 0.27d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(27.13, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(26.96, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1017),
                      Humidity = new Humidity(40),
                      DewPoint = new Temperature(11.68, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(7.22),
                      Clouds = 22,
                      Visibility = 10000,
                      WindSpeed = 1.6d,
                      WindDirectionDegrees = 292,
                      WindGust = 4.76d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 801,
                          Type = WeatherConditionType.Clouds,
                          Description = "Ein paar Wolken",
                          IconId = "02d"
                        }
                      },
                      Pop = 0.19d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(28.26, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(27.69, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1017),
                      Humidity = new Humidity(37),
                      DewPoint = new Temperature(11.53, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(8.11),
                      Clouds = 20,
                      Visibility = 10000,
                      WindSpeed = 2.36d,
                      WindDirectionDegrees = 284,
                      WindGust = 5.7d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 801,
                          Type = WeatherConditionType.Clouds,
                          Description = "Ein paar Wolken",
                          IconId = "02d"
                        }
                      },
                      Pop = 0.15d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(29, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(28.27, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1016),
                      Humidity = new Humidity(36),
                      DewPoint = new Temperature(11.68, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(8.08),
                      Clouds = 25,
                      Visibility = 10000,
                      WindSpeed = 3.2d,
                      WindDirectionDegrees = 280,
                      WindGust = 6.55d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 802,
                          Type = WeatherConditionType.Clouds,
                          Description = "Mäßig bewölkt",
                          IconId = "03d"
                        }
                      },
                      Pop = 0.07d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(29.44, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(28.61, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1016),
                      Humidity = new Humidity(35),
                      DewPoint = new Temperature(11.82, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(7.08),
                      Clouds = 44,
                      Visibility = 10000,
                      WindSpeed = 3.76d,
                      WindDirectionDegrees = 272,
                      WindGust = 6.65d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 802,
                          Type = WeatherConditionType.Clouds,
                          Description = "Mäßig bewölkt",
                          IconId = "03d"
                        }
                      },
                      Pop = 0.06d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(29.64, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(28.72, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1016),
                      Humidity = new Humidity(34),
                      DewPoint = new Temperature(11.63, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(5.49),
                      Clouds = 51,
                      Visibility = 10000,
                      WindSpeed = 3.74d,
                      WindDirectionDegrees = 268,
                      WindGust = 6.72d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 803,
                          Type = WeatherConditionType.Clouds,
                          Description = "Überwiegend bewölkt",
                          IconId = "04d"
                        }
                      },
                      Pop = 0.06d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(29.38, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(28.64, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1016),
                      Humidity = new Humidity(36),
                      DewPoint = new Temperature(12.37, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(3.68),
                      Clouds = 37,
                      Visibility = 10000,
                      WindSpeed = 3.24d,
                      WindDirectionDegrees = 285,
                      WindGust = 5.8d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 802,
                          Type = WeatherConditionType.Clouds,
                          Description = "Mäßig bewölkt",
                          IconId = "03d"
                        }
                      },
                      Pop = 0.08d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(28.28, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(28.23, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1016),
                      Humidity = new Humidity(44),
                      DewPoint = new Temperature(14.37, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(2.09),
                      Clouds = 30,
                      Visibility = 10000,
                      WindSpeed = 2.98d,
                      WindDirectionDegrees = 315,
                      WindGust = 4.65d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 802,
                          Type = WeatherConditionType.Clouds,
                          Description = "Mäßig bewölkt",
                          IconId = "03d"
                        }
                      },
                      Pop = 0.08d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(26.64, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(26.64, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1016),
                      Humidity = new Humidity(57),
                      DewPoint = new Temperature(16.83, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0.94),
                      Clouds = 25,
                      Visibility = 10000,
                      WindSpeed = 2.3d,
                      WindDirectionDegrees = 342,
                      WindGust = 4.49d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 802,
                          Type = WeatherConditionType.Clouds,
                          Description = "Mäßig bewölkt",
                          IconId = "03d"
                        }
                      },
                      Pop = 0.12d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(23.48, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(23.82, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1016),
                      Humidity = new Humidity(74),
                      DewPoint = new Temperature(17.73, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0.31),
                      Clouds = 28,
                      Visibility = 10000,
                      WindSpeed = 1.24d,
                      WindDirectionDegrees = 17,
                      WindGust = 1.6d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 0.28d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(20.36, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(20.62, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1017),
                      Humidity = new Humidity(83),
                      DewPoint = new Temperature(16.48, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 10,
                      Visibility = 10000,
                      WindSpeed = 1.42d,
                      WindDirectionDegrees = 176,
                      WindGust = 1.46d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 0.53d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(18.12, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(18.29, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1019),
                      Humidity = new Humidity(88),
                      DewPoint = new Temperature(15.35, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 18,
                      Visibility = 10000,
                      WindSpeed = 2.88d,
                      WindDirectionDegrees = 194,
                      WindGust = 4.2d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10n"
                        }
                      },
                      Pop = 0.75d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(16.64, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(16.76, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1019),
                      Humidity = new Humidity(92),
                      DewPoint = new Temperature(14.54, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 39,
                      Visibility = 10000,
                      WindSpeed = 2.94d,
                      WindDirectionDegrees = 201,
                      WindGust = 4.93d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 501,
                          Type = WeatherConditionType.Rain,
                          Description = "Mäßiger Regen",
                          IconId = "10n"
                        }
                      },
                      Pop = 0.74d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14.59, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(14.59, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1021),
                      Humidity = new Humidity(95),
                      DewPoint = new Temperature(12.89, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 31,
                      Visibility = 7351,
                      WindSpeed = 2.94d,
                      WindDirectionDegrees = 161,
                      WindGust = 4.55d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 501,
                          Type = WeatherConditionType.Rain,
                          Description = "Mäßiger Regen",
                          IconId = "10n"
                        }
                      },
                      Pop = 0.9d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(13.88, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1021),
                      Humidity = new Humidity(93),
                      DewPoint = new Temperature(12.09, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 26,
                      Visibility = 10000,
                      WindSpeed = 2.7d,
                      WindDirectionDegrees = 179,
                      WindGust = 2.96d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10n"
                        }
                      },
                      Pop = 0.96d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.87, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(13.69, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(91),
                      DewPoint = new Temperature(11.63, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 23,
                      Visibility = 10000,
                      WindSpeed = 2.63d,
                      WindDirectionDegrees = 197,
                      WindGust = 2.87d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10n"
                        }
                      },
                      Pop = 0.96d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.99, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(13.79, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(90),
                      DewPoint = new Temperature(11.63, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 9,
                      Visibility = 10000,
                      WindSpeed = 2.52d,
                      WindDirectionDegrees = 202,
                      WindGust = 2.25d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 800,
                          Type = WeatherConditionType.Clear,
                          Description = "Klarer Himmel",
                          IconId = "01n"
                        }
                      },
                      Pop = 0.11d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14.15, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(14, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(91),
                      DewPoint = new Temperature(11.91, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 18,
                      Visibility = 10000,
                      WindSpeed = 2.44d,
                      WindDirectionDegrees = 201,
                      WindGust = 2.5d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 801,
                          Type = WeatherConditionType.Clouds,
                          Description = "Ein paar Wolken",
                          IconId = "02n"
                        }
                      },
                      Pop = 0.07d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14.29, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(14.15, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(91),
                      DewPoint = new Temperature(11.98, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 14,
                      Visibility = 10000,
                      WindSpeed = 1.99d,
                      WindDirectionDegrees = 203,
                      WindGust = 1.89d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 801,
                          Type = WeatherConditionType.Clouds,
                          Description = "Ein paar Wolken",
                          IconId = "02n"
                        }
                      },
                      Pop = 0.04d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14.45, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(14.3, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(90),
                      DewPoint = new Temperature(12.08, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 13,
                      Visibility = 10000,
                      WindSpeed = 1.8d,
                      WindDirectionDegrees = 203,
                      WindGust = 1.62d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 801,
                          Type = WeatherConditionType.Clouds,
                          Description = "Ein paar Wolken",
                          IconId = "02d"
                        }
                      },
                      Pop = 0.01d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(16.1, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(16.09, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(89),
                      DewPoint = new Temperature(13.5, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0.34),
                      Clouds = 14,
                      Visibility = 10000,
                      WindSpeed = 1.74d,
                      WindDirectionDegrees = 213,
                      WindGust = 1.82d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 801,
                          Type = WeatherConditionType.Clouds,
                          Description = "Ein paar Wolken",
                          IconId = "02d"
                        }
                      },
                      Pop = 0.08d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(18.79, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(18.89, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(83),
                      DewPoint = new Temperature(14.93, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(1.01),
                      Clouds = 13,
                      Visibility = 10000,
                      WindSpeed = 1.2d,
                      WindDirectionDegrees = 218,
                      WindGust = 2.03d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 0.36d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(20.93, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(20.98, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(73),
                      DewPoint = new Temperature(15.05, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(2.21),
                      Clouds = 2,
                      Visibility = 10000,
                      WindSpeed = 1.26d,
                      WindDirectionDegrees = 274,
                      WindGust = 3.46d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 800,
                          Type = WeatherConditionType.Clear,
                          Description = "Klarer Himmel",
                          IconId = "01d"
                        }
                      },
                      Pop = 0.38d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(21.6, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(21.8, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(76),
                      DewPoint = new Temperature(16.42, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(3.87),
                      Clouds = 7,
                      Visibility = 10000,
                      WindSpeed = 1.71d,
                      WindDirectionDegrees = 286,
                      WindGust = 3.79d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 0.7d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(23.13, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(23.3, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1021),
                      Humidity = new Humidity(69),
                      DewPoint = new Temperature(16.45, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(5.7),
                      Clouds = 7,
                      Visibility = 10000,
                      WindSpeed = 1.76d,
                      WindDirectionDegrees = 286,
                      WindGust = 4.1d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 0.74d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(25.26, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(25.2, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(52),
                      DewPoint = new Temperature(13.95, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(6.88),
                      Clouds = 8,
                      Visibility = 10000,
                      WindSpeed = 2.12d,
                      WindDirectionDegrees = 286,
                      WindGust = 4.34d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 0.74d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(26.27, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(26.27, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(46),
                      DewPoint = new Temperature(12.92, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(7.72),
                      Clouds = 24,
                      Visibility = 10000,
                      WindSpeed = 2.4d,
                      WindDirectionDegrees = 294,
                      WindGust = 4.9d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 801,
                          Type = WeatherConditionType.Clouds,
                          Description = "Ein paar Wolken",
                          IconId = "02d"
                        }
                      },
                      Pop = 0.69d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(26.88, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(26.92, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(43),
                      DewPoint = new Temperature(12.45, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(7.7),
                      Clouds = 37,
                      Visibility = 10000,
                      WindSpeed = 2.81d,
                      WindDirectionDegrees = 302,
                      WindGust = 5.14d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 802,
                          Type = WeatherConditionType.Clouds,
                          Description = "Mäßig bewölkt",
                          IconId = "03d"
                        }
                      },
                      Pop = 0.64d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(27.28, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(27.07, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(40),
                      DewPoint = new Temperature(12.05, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(6.96),
                      Clouds = 100,
                      Visibility = 10000,
                      WindSpeed = 3.25d,
                      WindDirectionDegrees = 313,
                      WindGust = 4.88d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 804,
                          Type = WeatherConditionType.Clouds,
                          Description = "Bedeckt",
                          IconId = "04d"
                        }
                      },
                      Pop = 0.2d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(26.53, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(26.53, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1021),
                      Humidity = new Humidity(44),
                      DewPoint = new Temperature(12.64, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(5.4),
                      Clouds = 97,
                      Visibility = 10000,
                      WindSpeed = 3.58d,
                      WindDirectionDegrees = 328,
                      WindGust = 4.29d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 804,
                          Type = WeatherConditionType.Clouds,
                          Description = "Bedeckt",
                          IconId = "04d"
                        }
                      },
                      Pop = 0.2d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(24.58, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(24.53, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1021),
                      Humidity = new Humidity(55),
                      DewPoint = new Temperature(13.71, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(3.62),
                      Clouds = 87,
                      Visibility = 10000,
                      WindSpeed = 1.98d,
                      WindDirectionDegrees = 338,
                      WindGust = 3.32d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 804,
                          Type = WeatherConditionType.Clouds,
                          Description = "Bedeckt",
                          IconId = "04d"
                        }
                      },
                      Pop = 0.18d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(25.67, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(25.49, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1021),
                      Humidity = new Humidity(46),
                      DewPoint = new Temperature(12.28, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(2.15),
                      Clouds = 74,
                      Visibility = 10000,
                      WindSpeed = 2.2d,
                      WindDirectionDegrees = 340,
                      WindGust = 2.63d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 803,
                          Type = WeatherConditionType.Clouds,
                          Description = "Überwiegend bewölkt",
                          IconId = "04d"
                        }
                      },
                      Pop = 0.14d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(24.81, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(24.76, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1021),
                      Humidity = new Humidity(54),
                      DewPoint = new Temperature(14.28, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0.97),
                      Clouds = 65,
                      Visibility = 10000,
                      WindSpeed = 1.84d,
                      WindDirectionDegrees = 338,
                      WindGust = 3.38d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 803,
                          Type = WeatherConditionType.Clouds,
                          Description = "Überwiegend bewölkt",
                          IconId = "04d"
                        }
                      },
                      Pop = 0.1d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(22.4, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(22.5, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1021),
                      Humidity = new Humidity(69),
                      DewPoint = new Temperature(15.58, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0.32),
                      Clouds = 62,
                      Visibility = 10000,
                      WindSpeed = 1.29d,
                      WindDirectionDegrees = 335,
                      WindGust = 1.56d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 803,
                          Type = WeatherConditionType.Clouds,
                          Description = "Überwiegend bewölkt",
                          IconId = "04d"
                        }
                      },
                      Pop = 0.1d
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(20.11, TemperatureUnit.Celsius),
                      FeelsLike = new Temperature(20.06, TemperatureUnit.Celsius),
                      Pressure = new Pressure(1022),
                      Humidity = new Humidity(72),
                      DewPoint = new Temperature(14.21, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(0),
                      Clouds = 97,
                      Visibility = 10000,
                      WindSpeed = 0.39d,
                      WindDirectionDegrees = 327,
                      WindGust = 0.66d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 804,
                          Type = WeatherConditionType.Clouds,
                          Description = "Bedeckt",
                          IconId = "04d"
                        }
                      },
                      Pop = 0.05d
                    }
                  },
                DailyForecasts = new List<DailyWeatherForecast>
                  {
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-14T03:31:53.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-14T19:24:17.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-14T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-14T03:02:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = 0.5d,
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(23.42, TemperatureUnit.Celsius),
                        Min = new Temperature(9.96, TemperatureUnit.Celsius),
                        Max = new Temperature(25.01, TemperatureUnit.Celsius),
                        Night = new Temperature(17.32, TemperatureUnit.Celsius),
                        Evening = new Temperature(20.7, TemperatureUnit.Celsius),
                        Morning = new Temperature(12.16, TemperatureUnit.Celsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(22.94, TemperatureUnit.Celsius),
                        Night = new Temperature(16.88, TemperatureUnit.Celsius),
                        Evening = new Temperature(20.34, TemperatureUnit.Celsius),
                        Morning = new Temperature(11.55, TemperatureUnit.Celsius)
                      },
                      Pressure = new Pressure(1019),
                      Humidity = new Humidity(43),
                      DewPoint = new Temperature(9.49, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(7.88),
                      Clouds = 0,
                      Visibility = 0,
                      WindSpeed = 2.95d,
                      WindDirectionDegrees = 48,
                      WindGust = 3.51d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 800,
                          Type = WeatherConditionType.Clear,
                          Description = "Klarer Himmel",
                          IconId = "01d"
                        }
                      },
                      Pop = 0.06d,
                      Rain = 0d,
                      Snow = 0d
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-15T03:31:48.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-15T19:24:45.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-15T21:09:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-15T03:57:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = 0.54d,
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(28.26, TemperatureUnit.Celsius),
                        Min = new Temperature(13.7, TemperatureUnit.Celsius),
                        Max = new Temperature(29.64, TemperatureUnit.Celsius),
                        Night = new Temperature(16.64, TemperatureUnit.Celsius),
                        Evening = new Temperature(26.64, TemperatureUnit.Celsius),
                        Morning = new Temperature(15.86, TemperatureUnit.Celsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(27.69, TemperatureUnit.Celsius),
                        Night = new Temperature(16.76, TemperatureUnit.Celsius),
                        Evening = new Temperature(26.64, TemperatureUnit.Celsius),
                        Morning = new Temperature(15.51, TemperatureUnit.Celsius)
                      },
                      Pressure = new Pressure(1017),
                      Humidity = new Humidity(37),
                      DewPoint = new Temperature(11.53, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(8.11),
                      Clouds = 20,
                      Visibility = 0,
                      WindSpeed = 3.76d,
                      WindDirectionDegrees = 272,
                      WindGust = 6.72d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 501,
                          Type = WeatherConditionType.Rain,
                          Description = "Mäßiger Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 0.75d,
                      Rain = 2.77d,
                      Snow = 0d
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-16T03:31:46.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-16T19:25:11.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("1970-01-01T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-16T05:08:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = 0.58d,
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(26.27, TemperatureUnit.Celsius),
                        Min = new Temperature(13.87, TemperatureUnit.Celsius),
                        Max = new Temperature(27.28, TemperatureUnit.Celsius),
                        Night = new Temperature(17.66, TemperatureUnit.Celsius),
                        Evening = new Temperature(24.81, TemperatureUnit.Celsius),
                        Morning = new Temperature(16.1, TemperatureUnit.Celsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(26.27, TemperatureUnit.Celsius),
                        Night = new Temperature(17.49, TemperatureUnit.Celsius),
                        Evening = new Temperature(24.76, TemperatureUnit.Celsius),
                        Morning = new Temperature(16.09, TemperatureUnit.Celsius)
                      },
                      Pressure = new Pressure(1020),
                      Humidity = new Humidity(46),
                      DewPoint = new Temperature(12.92, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(7.72),
                      Clouds = 24,
                      Visibility = 0,
                      WindSpeed = 3.58d,
                      WindDirectionDegrees = 328,
                      WindGust = 5.14d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 501,
                          Type = WeatherConditionType.Rain,
                          Description = "Mäßiger Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 0.96d,
                      Rain = 3.39d,
                      Snow = 0d
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-17T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-17T03:31:47.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-17T19:25:35.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-16T22:01:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-17T06:28:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = 0.62d,
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(27.47, TemperatureUnit.Celsius),
                        Min = new Temperature(14.53, TemperatureUnit.Celsius),
                        Max = new Temperature(28.3, TemperatureUnit.Celsius),
                        Night = new Temperature(18.61, TemperatureUnit.Celsius),
                        Evening = new Temperature(23.73, TemperatureUnit.Celsius),
                        Morning = new Temperature(16.47, TemperatureUnit.Celsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(27.17, TemperatureUnit.Celsius),
                        Night = new Temperature(18.43, TemperatureUnit.Celsius),
                        Evening = new Temperature(23.91, TemperatureUnit.Celsius),
                        Morning = new Temperature(16.31, TemperatureUnit.Celsius)
                      },
                      Pressure = new Pressure(1022),
                      Humidity = new Humidity(39),
                      DewPoint = new Temperature(11.72, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(8.52),
                      Clouds = 74,
                      Visibility = 0,
                      WindSpeed = 2.43d,
                      WindDirectionDegrees = 8,
                      WindGust = 2.34d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 803,
                          Type = WeatherConditionType.Clouds,
                          Description = "Überwiegend bewölkt",
                          IconId = "04d"
                        }
                      },
                      Pop = 0d,
                      Rain = 0d,
                      Snow = 0d
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-18T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-18T03:31:50.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-18T19:25:56.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-17T22:40:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-18T07:52:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = 0.65d,
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(30.82, TemperatureUnit.Celsius),
                        Min = new Temperature(16.34, TemperatureUnit.Celsius),
                        Max = new Temperature(31.58, TemperatureUnit.Celsius),
                        Night = new Temperature(21.31, TemperatureUnit.Celsius),
                        Evening = new Temperature(25.86, TemperatureUnit.Celsius),
                        Morning = new Temperature(20.74, TemperatureUnit.Celsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(29.84, TemperatureUnit.Celsius),
                        Night = new Temperature(21.3, TemperatureUnit.Celsius),
                        Evening = new Temperature(26.17, TemperatureUnit.Celsius),
                        Morning = new Temperature(20.57, TemperatureUnit.Celsius)
                      },
                      Pressure = new Pressure(1017),
                      Humidity = new Humidity(33),
                      DewPoint = new Temperature(12.28, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(8.56),
                      Clouds = 1,
                      Visibility = 0,
                      WindSpeed = 3.19d,
                      WindDirectionDegrees = 51,
                      WindGust = 4.24d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 800,
                          Type = WeatherConditionType.Clear,
                          Description = "Klarer Himmel",
                          IconId = "01d"
                        }
                      },
                      Pop = 0.03d,
                      Rain = 0d,
                      Snow = 0d
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-19T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-19T03:31:56.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-19T19:26:15.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-18T23:09:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-19T09:13:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = 0.69d,
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(31.74, TemperatureUnit.Celsius),
                        Min = new Temperature(17.84, TemperatureUnit.Celsius),
                        Max = new Temperature(31.74, TemperatureUnit.Celsius),
                        Night = new Temperature(17.84, TemperatureUnit.Celsius),
                        Evening = new Temperature(20.36, TemperatureUnit.Celsius),
                        Morning = new Temperature(21.82, TemperatureUnit.Celsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(31.01, TemperatureUnit.Celsius),
                        Night = new Temperature(17.9, TemperatureUnit.Celsius),
                        Evening = new Temperature(20.8, TemperatureUnit.Celsius),
                        Morning = new Temperature(21.75, TemperatureUnit.Celsius)
                      },
                      Pressure = new Pressure(1010),
                      Humidity = new Humidity(34),
                      DewPoint = new Temperature(13.19, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(8.57),
                      Clouds = 0,
                      Visibility = 0,
                      WindSpeed = 3.47d,
                      WindDirectionDegrees = 149,
                      WindGust = 7.32d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 500,
                          Type = WeatherConditionType.Rain,
                          Description = "Leichter Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 0.85d,
                      Rain = 4.93d,
                      Snow = 0d
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-20T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-20T03:32:03.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-20T19:26:31.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-19T23:32:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-20T10:30:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = 0.72d,
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(23.92, TemperatureUnit.Celsius),
                        Min = new Temperature(14.68, TemperatureUnit.Celsius),
                        Max = new Temperature(24.37, TemperatureUnit.Celsius),
                        Night = new Temperature(14.68, TemperatureUnit.Celsius),
                        Evening = new Temperature(18.14, TemperatureUnit.Celsius),
                        Morning = new Temperature(20.58, TemperatureUnit.Celsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(23.88, TemperatureUnit.Celsius),
                        Night = new Temperature(14.76, TemperatureUnit.Celsius),
                        Evening = new Temperature(18.52, TemperatureUnit.Celsius),
                        Morning = new Temperature(20.5, TemperatureUnit.Celsius)
                      },
                      Pressure = new Pressure(1012),
                      Humidity = new Humidity(58),
                      DewPoint = new Temperature(14.37, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(9),
                      Clouds = 76,
                      Visibility = 0,
                      WindSpeed = 3.32d,
                      WindDirectionDegrees = 359,
                      WindGust = 7.06d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 501,
                          Type = WeatherConditionType.Rain,
                          Description = "Mäßiger Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 1d,
                      Rain = 9.69d,
                      Snow = 0d
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-21T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-21T03:32:14.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-21T19:26:45.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-20T23:51:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-21T11:43:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = 0.75d,
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(7.46, TemperatureUnit.Celsius),
                        Min = new Temperature(7.46, TemperatureUnit.Celsius),
                        Max = new Temperature(14.02, TemperatureUnit.Celsius),
                        Night = new Temperature(8.01, TemperatureUnit.Celsius),
                        Evening = new Temperature(12.57, TemperatureUnit.Celsius),
                        Morning = new Temperature(10.98, TemperatureUnit.Celsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(5.04, TemperatureUnit.Celsius),
                        Night = new Temperature(6.87, TemperatureUnit.Celsius),
                        Evening = new Temperature(11.68, TemperatureUnit.Celsius),
                        Morning = new Temperature(10.69, TemperatureUnit.Celsius)
                      },
                      Pressure = new Pressure(1015),
                      Humidity = new Humidity(96),
                      DewPoint = new Temperature(6.06, TemperatureUnit.Celsius),
                      UVIndex = new UVIndex(9),
                      Clouds = 100,
                      Visibility = 0,
                      WindSpeed = 3.7d,
                      WindDirectionDegrees = 211,
                      WindGust = 8.22d,
                      Weather = new List<WeatherCondition>
                      {
                        new WeatherCondition
                        {
                          Id = 502,
                          Type = WeatherConditionType.Rain,
                          Description = "Starker Regen",
                          IconId = "10d"
                        }
                      },
                      Pop = 1d,
                      Rain = 50.16d,
                      Snow = 0d
                    }
                },
                Alerts = new List<AlertInfo>
                {
                }
            };
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
                            Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                            Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
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
                            Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                            Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
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
                            Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                            Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
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
                        Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
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
                            Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                            Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
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
                        Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
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
                        Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
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
                        Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.Celsius),
                        Night = new Temperature(8.95d, TemperatureUnit.Celsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.Celsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.Celsius),
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

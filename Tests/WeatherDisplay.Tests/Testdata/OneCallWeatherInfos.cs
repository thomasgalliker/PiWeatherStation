using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenWeatherMap.Models;
using UnitsNet;
using UnitsNet.Units;

namespace WeatherDisplay.Tests.Testdata
{
    internal static class OneCallWeatherInfos
    {
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
                    Temperature = new Temperature(18.01, TemperatureUnit.DegreeCelsius),
                    FeelsLike = new Temperature(17.54, TemperatureUnit.DegreeCelsius),
                    Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                    Humidity = new RelativeHumidity(64, RelativeHumidityUnit.Percent),
                    DewPoint = new Temperature(11.11, TemperatureUnit.DegreeCelsius),
                    UVIndex = new UVIndex(0),
                    Clouds = new Ratio(73d, RatioUnit.Percent),
                    Visibility = new Length(10000d, LengthUnit.Meter),
                    WindSpeed = new Speed(0d, SpeedUnit.MeterPerSecond),
                    WindDirection = new Angle(0, AngleUnit.Degree),
                    WindGust = new Speed(0d, SpeedUnit.MeterPerSecond),
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
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:07:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:08:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:09:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:10:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:11:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:12:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:13:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:14:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:15:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:16:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:17:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:18:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:19:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:20:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:21:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:22:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:23:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:24:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:25:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:26:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:27:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:28:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:29:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:30:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:31:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:32:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:33:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:34:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:35:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:36:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:37:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:38:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:39:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:40:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:41:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:42:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:43:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:44:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:45:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:46:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:47:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:48:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:49:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:50:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:51:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:52:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:53:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:54:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:55:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:56:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:57:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:58:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:59:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:01:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:02:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:03:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:04:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:05:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    },
                    new MinutelyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:06:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Precipitation = new Speed(0d, SpeedUnit.MillimeterPerHour)
                    }
                  },
                HourlyForecasts = new List<HourlyWeatherForecast>
                  {
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(18.01, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(17.54, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(64, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.11, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(73d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.2d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(142, AngleUnit.Degree),
                      WindGust = new Speed(1.33d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.03d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(17.32, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(16.88, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(68, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.36, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(70d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.29d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(160, AngleUnit.Degree),
                      WindGust = new Speed(1.35d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.03d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(16.49, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(16.07, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(72, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.43, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(61d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.38d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(167, AngleUnit.Degree),
                      WindGust = new Speed(1.34d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.01d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-14T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(15.6, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(15.17, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(75, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.19, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(50d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.45d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(176, AngleUnit.Degree),
                      WindGust = new Speed(1.34d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14.66, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(14.19, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(10.68, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(38d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.52d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(198, AngleUnit.Degree),
                      WindGust = new Speed(1.36d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.71, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(13.23, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(80, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(9.43, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(17d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.75d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(196, AngleUnit.Degree),
                      WindGust = new Speed(1.58d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.03d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.7, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(13.19, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(79, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(9.18, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(11d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.9d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(189, AngleUnit.Degree),
                      WindGust = new Speed(1.7d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.01d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.77, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(13.21, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(8.99, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(8d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.72d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(179, AngleUnit.Degree),
                      WindGust = new Speed(1.47d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.96, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(13.42, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(9.14, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(6d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.51d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(190, AngleUnit.Degree),
                      WindGust = new Speed(1.4d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(15.86, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(15.51, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(10.83, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0.34),
                      Clouds = new Ratio(6d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.27d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(199, AngleUnit.Degree),
                      WindGust = new Speed(1.29d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(19.04, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(18.75, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(67, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.93, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(1.01),
                      Clouds = new Ratio(6d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(0.73d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(216, AngleUnit.Degree),
                      WindGust = new Speed(1.17d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(21.42, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(21.18, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(60, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.48, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(2.18),
                      Clouds = new Ratio(19d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(0.88d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(286, AngleUnit.Degree),
                      WindGust = new Speed(1.46d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.14d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(23.33, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(23.13, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(54, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.54, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(3.82),
                      Clouds = new Ratio(26d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(0.98d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(289, AngleUnit.Degree),
                      WindGust = new Speed(3.1d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.3d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(25.57, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(25.36, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(45, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.1, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(5.63),
                      Clouds = new Ratio(29d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.07d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(299, AngleUnit.Degree),
                      WindGust = new Speed(3.32d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.27d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(27.13, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(26.96, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(40, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.68, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(7.22),
                      Clouds = new Ratio(22d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.6d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(292, AngleUnit.Degree),
                      WindGust = new Speed(4.76d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.19d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(28.26, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(27.69, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(37, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.53, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(8.11),
                      Clouds = new Ratio(20d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.36d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(284, AngleUnit.Degree),
                      WindGust = new Speed(5.7d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.15d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(29, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(28.27, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(36, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.68, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(8.08),
                      Clouds = new Ratio(25d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.2d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(280, AngleUnit.Degree),
                      WindGust = new Speed(6.55d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.07d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(29.44, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(28.61, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(35, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.82, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(7.08),
                      Clouds = new Ratio(44d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.76d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(272, AngleUnit.Degree),
                      WindGust = new Speed(6.65d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.06d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(29.64, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(28.72, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(34, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.63, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(5.49),
                      Clouds = new Ratio(51d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.74d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(268, AngleUnit.Degree),
                      WindGust = new Speed(6.72d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.06d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(29.38, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(28.64, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(36, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.37, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(3.68),
                      Clouds = new Ratio(37d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.24d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(285, AngleUnit.Degree),
                      WindGust = new Speed(5.8d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.08d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(28.28, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(28.23, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(44, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(14.37, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(2.09),
                      Clouds = new Ratio(30d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.98d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(315, AngleUnit.Degree),
                      WindGust = new Speed(4.65d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.08d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(26.64, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(26.64, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(57, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(16.83, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0.94),
                      Clouds = new Ratio(25d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.3d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(342, AngleUnit.Degree),
                      WindGust = new Speed(4.49d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.12d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(23.48, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(23.82, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(74, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(17.73, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0.31),
                      Clouds = new Ratio(28d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.24d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(17, AngleUnit.Degree),
                      WindGust = new Speed(1.6d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.28d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(20.36, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(20.62, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(83, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(16.48, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(10d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.42d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(176, AngleUnit.Degree),
                      WindGust = new Speed(1.46d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.53d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(18.12, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(18.29, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(88, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(15.35, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(18d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.88d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(194, AngleUnit.Degree),
                      WindGust = new Speed(4.2d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.75d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(16.64, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(16.76, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(92, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(14.54, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(39d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.94d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(201, AngleUnit.Degree),
                      WindGust = new Speed(4.93d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.74d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(95, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.89, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(31d, RatioUnit.Percent),
                      Visibility = new Length(7351d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.94d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(161, AngleUnit.Degree),
                      WindGust = new Speed(4.55d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.9d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(13.88, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(93, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.09, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(26d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.7d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(179, AngleUnit.Degree),
                      WindGust = new Speed(2.96d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.96d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.87, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(13.69, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(91, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.63, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(23d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.63d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(197, AngleUnit.Degree),
                      WindGust = new Speed(2.87d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.96d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(13.99, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(13.79, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(90, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.63, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(9d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.52d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(202, AngleUnit.Degree),
                      WindGust = new Speed(2.25d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.11d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14.15, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(14, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(91, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.91, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(18d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.44d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(201, AngleUnit.Degree),
                      WindGust = new Speed(2.5d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.07d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14.29, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(14.15, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(91, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.98, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(14d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.99d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(203, AngleUnit.Degree),
                      WindGust = new Speed(1.89d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.04d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(14.45, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(14.3, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(90, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.08, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(13d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.8d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(203, AngleUnit.Degree),
                      WindGust = new Speed(1.62d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.01d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(16.1, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(16.09, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(89, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(13.5, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0.34),
                      Clouds = new Ratio(14d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.74d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(213, AngleUnit.Degree),
                      WindGust = new Speed(1.82d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.08d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(18.79, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(18.89, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(83, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(14.93, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(1.01),
                      Clouds = new Ratio(13d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.2d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(218, AngleUnit.Degree),
                      WindGust = new Speed(2.03d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.36d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(20.93, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(20.98, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(73, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(15.05, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(2.21),
                      Clouds = new Ratio(2d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.26d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(274, AngleUnit.Degree),
                      WindGust = new Speed(3.46d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.38d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(21.6, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(21.8, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(76, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(16.42, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(3.87),
                      Clouds = new Ratio(7d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.71d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(286, AngleUnit.Degree),
                      WindGust = new Speed(3.79d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.7d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(23.13, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(23.3, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(16.45, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(5.7),
                      Clouds = new Ratio(7d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.76d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(286, AngleUnit.Degree),
                      WindGust = new Speed(4.1d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.74d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(25.26, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(25.2, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(52, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(13.95, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(6.88),
                      Clouds = new Ratio(8d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.12d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(286, AngleUnit.Degree),
                      WindGust = new Speed(4.34d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.74d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(26.27, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(26.27, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(46, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.92, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(7.72),
                      Clouds = new Ratio(24d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.4d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(294, AngleUnit.Degree),
                      WindGust = new Speed(4.9d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.69d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(26.88, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(26.92, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(43, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.45, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(7.7),
                      Clouds = new Ratio(37d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.81d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(302, AngleUnit.Degree),
                      WindGust = new Speed(5.14d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.64d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(27.28, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(27.07, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(40, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.05, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(6.96),
                      Clouds = new Ratio(100d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.25d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(313, AngleUnit.Degree),
                      WindGust = new Speed(4.88d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.2d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(26.53, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(26.53, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(44, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.64, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(5.4),
                      Clouds = new Ratio(97d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.58d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(328, AngleUnit.Degree),
                      WindGust = new Speed(4.29d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.2d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(24.58, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(24.53, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(55, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(13.71, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(3.62),
                      Clouds = new Ratio(87d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.98d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(338, AngleUnit.Degree),
                      WindGust = new Speed(3.32d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.18d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(25.67, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(25.49, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(46, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.28, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(2.15),
                      Clouds = new Ratio(74d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.2d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(340, AngleUnit.Degree),
                      WindGust = new Speed(2.63d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.14d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(24.81, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(24.76, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(54, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(14.28, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0.97),
                      Clouds = new Ratio(65d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.84d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(338, AngleUnit.Degree),
                      WindGust = new Speed(3.38d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.1d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(22.4, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(22.5, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(15.58, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0.32),
                      Clouds = new Ratio(62d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(1.29d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(335, AngleUnit.Degree),
                      WindGust = new Speed(1.56d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.1d, RatioUnit.Percent)
                    },
                    new HourlyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Temperature = new Temperature(20.11, TemperatureUnit.DegreeCelsius),
                      FeelsLike = new Temperature(20.06, TemperatureUnit.DegreeCelsius),
                      Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(72, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(14.21, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(0),
                      Clouds = new Ratio(97d, RatioUnit.Percent),
                      Visibility = new Length(10000d, LengthUnit.Meter),
                      WindSpeed = new Speed(0.39d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(327, AngleUnit.Degree),
                      WindGust = new Speed(0.66d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.05d, RatioUnit.Percent)
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
                      MoonPhase = new Ratio(0.5d * 100d, RatioUnit.Percent),
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(23.42, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(9.96, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(25.01, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(17.32, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(20.7, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(12.16, TemperatureUnit.DegreeCelsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(22.94, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(16.88, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(20.34, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(11.55, TemperatureUnit.DegreeCelsius)
                      },
                      Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(43, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(9.49, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(7.88),
                      Clouds = new Ratio(0d, RatioUnit.Percent),
                      Visibility = new Length(0d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.95d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(48, AngleUnit.Degree),
                      WindGust = new Speed(3.51d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.06d, RatioUnit.Percent),
                      Rain = new Length(0d, LengthUnit.Millimeter),
                      Snow = new Length(0d, LengthUnit.Millimeter)
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-15T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-15T03:31:48.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-15T19:24:45.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-15T21:09:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-15T03:57:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = new Ratio(0.54d * 100d, RatioUnit.Percent),
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(28.26, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(13.7, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(29.64, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(16.64, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(26.64, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(15.86, TemperatureUnit.DegreeCelsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(27.69, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(16.76, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(26.64, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(15.51, TemperatureUnit.DegreeCelsius)
                      },
                      Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(37, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.53, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(8.11),
                      Clouds = new Ratio(20d, RatioUnit.Percent),
                      Visibility = new Length(0d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.76d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(272, AngleUnit.Degree),
                      WindGust = new Speed(6.72d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.75d, RatioUnit.Percent),
                      Rain = new Length(2.77d, LengthUnit.Millimeter),
                      Snow = new Length(0d, LengthUnit.Millimeter)
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-16T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-16T03:31:46.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-16T19:25:11.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("1970-01-01T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-16T05:08:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = new Ratio(0.58d * 100d, RatioUnit.Percent),
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(26.27, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(13.87, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(27.28, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(17.66, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(24.81, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(16.1, TemperatureUnit.DegreeCelsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(26.27, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(17.49, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(24.76, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(16.09, TemperatureUnit.DegreeCelsius)
                      },
                      Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(46, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.92, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(7.72),
                      Clouds = new Ratio(24d, RatioUnit.Percent),
                      Visibility = new Length(0d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.58d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(328, AngleUnit.Degree),
                      WindGust = new Speed(5.14d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.96d, RatioUnit.Percent),
                      Rain = new Length(3.39d, LengthUnit.Millimeter),
                      Snow = new Length(0d, LengthUnit.Millimeter)
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-17T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-17T03:31:47.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-17T19:25:35.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-16T22:01:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-17T06:28:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = new Ratio(0.62d * 100d, RatioUnit.Percent),
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(27.47, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(14.53, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(28.3, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(18.61, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(23.73, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(16.47, TemperatureUnit.DegreeCelsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(27.17, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(18.43, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(23.91, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(16.31, TemperatureUnit.DegreeCelsius)
                      },
                      Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(39, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(11.72, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(8.52),
                      Clouds = new Ratio(74d, RatioUnit.Percent),
                      Visibility = new Length(0d, LengthUnit.Meter),
                      WindSpeed = new Speed(2.43d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(8, AngleUnit.Degree),
                      WindGust = new Speed(2.34d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0d, RatioUnit.Percent),
                      Rain = new Length(0d, LengthUnit.Millimeter),
                      Snow = new Length(0d, LengthUnit.Millimeter)
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-18T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-18T03:31:50.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-18T19:25:56.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-17T22:40:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-18T07:52:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = new Ratio(0.65d * 100d, RatioUnit.Percent),
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(30.82, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(16.34, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(31.58, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(21.31, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(25.86, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(20.74, TemperatureUnit.DegreeCelsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(29.84, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(21.3, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(26.17, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(20.57, TemperatureUnit.DegreeCelsius)
                      },
                      Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(33, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(12.28, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(8.56),
                      Clouds = new Ratio(1d, RatioUnit.Percent),
                      Visibility = new Length(0d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.19d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(51, AngleUnit.Degree),
                      WindGust = new Speed(4.24d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.03d, RatioUnit.Percent),
                      Rain = new Length(0d, LengthUnit.Millimeter),
                      Snow = new Length(0d, LengthUnit.Millimeter)
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-19T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-19T03:31:56.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-19T19:26:15.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-18T23:09:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-19T09:13:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = new Ratio(0.69d * 100d, RatioUnit.Percent),
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(31.74, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(17.84, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(31.74, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(17.84, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(20.36, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(21.82, TemperatureUnit.DegreeCelsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(31.01, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(17.9, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(20.8, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(21.75, TemperatureUnit.DegreeCelsius)
                      },
                      Pressure = new Pressure(1010, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(34, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(13.19, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(8.57),
                      Clouds = new Ratio(0d, RatioUnit.Percent),
                      Visibility = new Length(0d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.47d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(149, AngleUnit.Degree),
                      WindGust = new Speed(7.32d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(0.85d, RatioUnit.Percent),
                      Rain = new Length(4.93d, LengthUnit.Millimeter),
                      Snow = new Length(0d, LengthUnit.Millimeter)
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-20T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-20T03:32:03.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-20T19:26:31.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-19T23:32:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-20T10:30:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = new Ratio(0.72d * 100d, RatioUnit.Percent),
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(23.92, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(14.68, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(24.37, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(14.68, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(18.14, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(20.58, TemperatureUnit.DegreeCelsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(23.88, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(14.76, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(18.52, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(20.5, TemperatureUnit.DegreeCelsius)
                      },
                      Pressure = new Pressure(1012, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(58, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(14.37, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(9),
                      Clouds = new Ratio(76d, RatioUnit.Percent),
                      Visibility = new Length(0d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.32d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(359, AngleUnit.Degree),
                      WindGust = new Speed(7.06d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(1d, RatioUnit.Percent),
                      Rain = new Length(9.69d, LengthUnit.Millimeter),
                      Snow = new Length(0d, LengthUnit.Millimeter)
                    },
                    new DailyWeatherForecast
                    {
                      DateTime = DateTime.ParseExact("2022-06-21T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunrise = DateTime.ParseExact("2022-06-21T03:32:14.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Sunset = DateTime.ParseExact("2022-06-21T19:26:45.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonrise = DateTime.ParseExact("2022-06-20T23:51:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      Moonset = DateTime.ParseExact("2022-06-21T11:43:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                      MoonPhase = new Ratio(0.75d * 100d, RatioUnit.Percent),
                      Temperature = new DailyTemperatureForecast
                      {
                        Day = new Temperature(7.46, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(7.46, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(14.02, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(8.01, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(12.57, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(10.98, TemperatureUnit.DegreeCelsius)
                      },
                      FeelsLike = new DailyFeelsLikeForecast
                      {
                        Day = new Temperature(5.04, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(6.87, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(11.68, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(10.69, TemperatureUnit.DegreeCelsius)
                      },
                      Pressure = new Pressure(1015, PressureUnit.Hectopascal),
                      Humidity = new RelativeHumidity(96, RelativeHumidityUnit.Percent),
                      DewPoint = new Temperature(6.06, TemperatureUnit.DegreeCelsius),
                      UVIndex = new UVIndex(9),
                      Clouds = new Ratio(100d, RatioUnit.Percent),
                      Visibility = new Length(0d, LengthUnit.Meter),
                      WindSpeed = new Speed(3.7d, SpeedUnit.MeterPerSecond),
                      WindDirection = new Angle(211, AngleUnit.Degree),
                      WindGust = new Speed(8.22d, SpeedUnit.MeterPerSecond),
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
                      Pop = new Ratio(1d, RatioUnit.Percent),
                      Rain = new Length(50.16d, LengthUnit.Millimeter),
                      Snow = new Length(0d, LengthUnit.Millimeter)
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
                    Temperature = new Temperature(25, TemperatureUnit.DegreeCelsius),
                    FeelsLike = new Temperature(26, TemperatureUnit.DegreeCelsius),
                    Pressure = new Pressure(1000, PressureUnit.Hectopascal),
                    Humidity = new RelativeHumidity(50, RelativeHumidityUnit.Percent),
                    DewPoint = new Temperature(3.4, TemperatureUnit.DegreeCelsius),
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
                        MoonPhase = new Ratio(0.47d * 100d, RatioUnit.Percent),
                        Temperature = new DailyTemperatureForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                            Min = new Temperature(6.32d, TemperatureUnit.DegreeCelsius),
                            Max = new Temperature(15.85d, TemperatureUnit.DegreeCelsius),
                            Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                            Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        Pressure = new Pressure(1025, PressureUnit.Hectopascal),
                        Humidity = new RelativeHumidity(63, RelativeHumidityUnit.Percent),
                        DewPoint = new Temperature(7.7d, TemperatureUnit.DegreeCelsius),
                        WindSpeed = new Speed(3.85d, SpeedUnit.MeterPerSecond),
                        WindDirection = new Angle(42, AngleUnit.Degree),
                        WindGust = new Speed(7.82d, SpeedUnit.MeterPerSecond),
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
                        Clouds = new Ratio(68d, RatioUnit.Percent),
                        Pop = new Ratio(1d / 3d, RatioUnit.Percent),
                        Rain = new Length(11.923423d, LengthUnit.Millimeter),
                        UVIndex = 2.51d,
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-18T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-18T06:33:38.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-18T18:34:57.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-18T18:52:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-18T07:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = new Ratio(0.5d * 100d, RatioUnit.Percent),
                        Temperature = new DailyTemperatureForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                            Min = new Temperature(6.32d, TemperatureUnit.DegreeCelsius),
                            Max = new Temperature(15.85d, TemperatureUnit.DegreeCelsius),
                            Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                            Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        Pressure = new Pressure(1034, PressureUnit.Hectopascal),
                        Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                        DewPoint = new Temperature(3.45d, TemperatureUnit.DegreeCelsius),
                        WindSpeed = new Speed(5.02d, SpeedUnit.MeterPerSecond),
                        WindDirection = new Angle(60, AngleUnit.Degree),
                        WindGust = new Speed(9.32d, SpeedUnit.MeterPerSecond),
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
                        Clouds = new Ratio(100d, RatioUnit.Percent),
                        Pop = new Ratio(0.08d, RatioUnit.Percent),
                        UVIndex = 2.37d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-19T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-19T06:31:37.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-19T18:36:21.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-19T20:08:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-19T07:19:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = new Ratio(0.54d * 100d, RatioUnit.Percent),
                        Temperature = new DailyTemperatureForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                            Min = new Temperature(6.32d, TemperatureUnit.DegreeCelsius),
                            Max = new Temperature(15.85d, TemperatureUnit.DegreeCelsius),
                            Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                            Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        Pressure = new Pressure(1029, PressureUnit.Hectopascal),
                        Humidity = new RelativeHumidity(59, RelativeHumidityUnit.Percent),
                        DewPoint = new Temperature(1.55d, TemperatureUnit.DegreeCelsius),
                        WindSpeed = new Speed(3.58d, SpeedUnit.MeterPerSecond),
                        WindDirection = new Angle(61, AngleUnit.Degree),
                        WindGust = new Speed(7.48d, SpeedUnit.MeterPerSecond),
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
                        Clouds = new Ratio(32d, RatioUnit.Percent),
                        Pop = new Ratio(0.32d, RatioUnit.Percent),
                        UVIndex = 2.99d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-20T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-20T06:29:36.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-20T18:37:44.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-20T21:26:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-20T07:38:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = new Ratio(0.58d * 100d, RatioUnit.Percent),
                        Temperature = new DailyTemperatureForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(6.32d, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(15.85d, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        Pressure = new Pressure(1026, PressureUnit.Hectopascal),
                        Humidity = new RelativeHumidity(34, RelativeHumidityUnit.Percent),
                        DewPoint = new Temperature(-2.23d, TemperatureUnit.DegreeCelsius),
                        WindSpeed = new Speed(2.38d, SpeedUnit.MeterPerSecond),
                        WindDirection = new Angle(155, AngleUnit.Degree),
                        WindGust = new Speed(3.04d, SpeedUnit.MeterPerSecond),
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
                        Clouds = new Ratio(69d, RatioUnit.Percent),
                        Pop = new Ratio(0.18d, RatioUnit.Percent),
                        UVIndex = 2.24d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-21T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-21T06:27:35.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-21T18:39:08.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-21T22:45:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-21T07:59:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = new Ratio(0.61d * 100d, RatioUnit.Percent),
                        Temperature = new DailyTemperatureForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                            Min = new Temperature(6.32d, TemperatureUnit.DegreeCelsius),
                            Max = new Temperature(15.85d, TemperatureUnit.DegreeCelsius),
                            Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                            Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                            Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                            Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                            Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        Pressure = new Pressure(1030, PressureUnit.Hectopascal),
                        Humidity = new RelativeHumidity(35, RelativeHumidityUnit.Percent),
                        DewPoint = new Temperature(-1.93d, TemperatureUnit.DegreeCelsius),
                        WindSpeed = new Speed(2.11d, SpeedUnit.MeterPerSecond),
                        WindDirection = new Angle(158, AngleUnit.Degree),
                        WindGust = new Speed(2.51d, SpeedUnit.MeterPerSecond),
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
                        Clouds = new Ratio(1d, RatioUnit.Percent),
                        Pop = new Ratio(0d, RatioUnit.Percent),
                        UVIndex = 3.58d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-22T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-22T06:25:34.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-22T18:40:32.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("1970-01-01T01:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-22T08:24:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = new Ratio(0.65d * 100d, RatioUnit.Percent),
                        Temperature = new DailyTemperatureForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(6.32d, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(15.85d, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        Pressure = new Pressure(1027, PressureUnit.Hectopascal),
                        Humidity = new RelativeHumidity(41, RelativeHumidityUnit.Percent),
                        DewPoint = new Temperature(-0.3d, TemperatureUnit.DegreeCelsius),
                        WindSpeed = new Speed(2d, SpeedUnit.MeterPerSecond),
                        WindDirection = new Angle(163, AngleUnit.Degree),
                        WindGust = new Speed(1.92d, SpeedUnit.MeterPerSecond),
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
                        Clouds = new Ratio(0d, RatioUnit.Percent),
                        Pop = new Ratio(0d, RatioUnit.Percent),
                        UVIndex = 4d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-23T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-23T06:23:33.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-23T18:41:55.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-23T00:06:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-23T08:56:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = new Ratio(0.69d * 100d, RatioUnit.Percent),
                        Temperature = new DailyTemperatureForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(6.32d, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(15.85d, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        Pressure = new Pressure(1025, PressureUnit.Hectopascal),
                        Humidity = new RelativeHumidity(30, RelativeHumidityUnit.Percent),
                        DewPoint = new Temperature(-3.29d, TemperatureUnit.DegreeCelsius),
                        WindSpeed = new Speed(1.9d, SpeedUnit.MeterPerSecond),
                        WindDirection = new Angle(163, AngleUnit.Degree),
                        WindGust = new Speed(1.85d, SpeedUnit.MeterPerSecond),
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
                        Clouds = new Ratio(0d, RatioUnit.Percent),
                        Pop = new Ratio(0d, RatioUnit.Percent),
                        UVIndex = 4d
                    },
                    new DailyWeatherForecast
                    {
                        DateTime = DateTime.ParseExact("2022-03-24T12:00:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunrise = DateTime.ParseExact("2022-03-24T06:21:32.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-03-24T18:43:19.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonrise = DateTime.ParseExact("2022-03-24T01:26:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Moonset = DateTime.ParseExact("2022-03-24T09:38:00.0000000+01:00", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        MoonPhase = new Ratio(0.72d * 100d, RatioUnit.Percent),
                        Temperature = new DailyTemperatureForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                        Min = new Temperature(6.32d, TemperatureUnit.DegreeCelsius),
                        Max = new Temperature(15.85d, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        FeelsLike = new DailyFeelsLikeForecast
                        {
                        Day = new Temperature(14.66d, TemperatureUnit.DegreeCelsius),
                        Night = new Temperature(8.95d, TemperatureUnit.DegreeCelsius),
                        Evening = new Temperature(13.64d, TemperatureUnit.DegreeCelsius),
                        Morning = new Temperature(6.39d, TemperatureUnit.DegreeCelsius),
                        },
                        Pressure = new Pressure(1025, PressureUnit.Hectopascal),
                        Humidity = new RelativeHumidity(37, RelativeHumidityUnit.Percent),
                        DewPoint = new Temperature(0.06d, TemperatureUnit.DegreeCelsius),
                        WindSpeed = new Speed(1.81d, SpeedUnit.MeterPerSecond),
                        WindDirection = new Angle(173, AngleUnit.Degree),
                        WindGust = new Speed(1.62d, SpeedUnit.MeterPerSecond),
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
                        Clouds = new Ratio(0d, RatioUnit.Percent),
                        Pop = new Ratio(0d, RatioUnit.Percent),
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

using System;
using System.Collections.Generic;
using System.Globalization;
using OpenWeatherMap.Models;
using UnitsNet;
using UnitsNet.Units;

namespace WeatherDisplay.Tests.Testdata
{
    internal static class WeatherForecasts
    {
        internal static class Default
        {
            internal static WeatherForecast GetTestWeatherForecast()
            {
                return new WeatherForecast
                {
                    Code = "200",
                    Count = 40,
                    Items = new List<WeatherForecastItem>
                {
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-14T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(59d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(16.92, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(16.47, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(16.92, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(14.56, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 803,
                            Type = WeatherConditionType.Clouds,
                            Description = "Überwiegend bewölkt",
                            IconId = "04n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(160, AngleUnit.Degree),
                        Speed = new Speed(1.29d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(49d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(15.89, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(15.44, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(73, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(15.89, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(13.82, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 802,
                            Type = WeatherConditionType.Clouds,
                            Description = "Mäßig bewölkt",
                            IconId = "03n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(198, AngleUnit.Degree),
                        Speed = new Speed(1.52d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(25d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(14.82, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(14.32, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(75, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(14.82, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(13.77, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1019, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 802,
                            Type = WeatherConditionType.Clouds,
                            Description = "Mäßig bewölkt",
                            IconId = "03n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(179, AngleUnit.Degree),
                        Speed = new Speed(1.72d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(6d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(19.04, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(18.75, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(67, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(19.04, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(19.04, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(216, AngleUnit.Degree),
                        Speed = new Speed(0.73d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(29d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = new PrecipitationVolume
                        {
                        Last3h = new Length(0.12, LengthUnit.Millimeter),
                        Last1h = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(25.57, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(25.36, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(45, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(25.57, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(25.57, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 500,
                            Type = WeatherConditionType.Rain,
                            Description = "Leichter Regen",
                            IconId = "10d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(299, AngleUnit.Degree),
                        Speed = new Speed(1.07d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(25d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(29, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(28.27, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(36, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(29, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(29, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 802,
                            Type = WeatherConditionType.Clouds,
                            Description = "Mäßig bewölkt",
                            IconId = "03d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(280, AngleUnit.Degree),
                        Speed = new Speed(3.2d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(37d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(29.38, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(28.64, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(36, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(29.38, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(29.38, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 802,
                            Type = WeatherConditionType.Clouds,
                            Description = "Mäßig bewölkt",
                            IconId = "03d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(285, AngleUnit.Degree),
                        Speed = new Speed(3.24d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(28d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = new PrecipitationVolume
                        {
                        Last3h = new Length(0.16, LengthUnit.Millimeter),
                        Last1h = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(23.48, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(23.82, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(74, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(23.48, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(23.48, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 500,
                            Type = WeatherConditionType.Rain,
                            Description = "Leichter Regen",
                            IconId = "10d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(17, AngleUnit.Degree),
                        Speed = new Speed(1.24d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(39d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = new PrecipitationVolume
                        {
                        Last3h = new Length(2.51, LengthUnit.Millimeter),
                        Last1h = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(16.64, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(16.76, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(92, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(16.64, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(16.64, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1019, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 500,
                            Type = WeatherConditionType.Rain,
                            Description = "Leichter Regen",
                            IconId = "10n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(201, AngleUnit.Degree),
                        Speed = new Speed(2.94d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(23d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = new PrecipitationVolume
                        {
                        Last3h = new Length(2.31, LengthUnit.Millimeter),
                        Last1h = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(13.87, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(13.69, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(91, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(13.87, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(13.87, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 500,
                            Type = WeatherConditionType.Rain,
                            Description = "Leichter Regen",
                            IconId = "10n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(197, AngleUnit.Degree),
                        Speed = new Speed(2.63d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(14d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(14.29, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(14.15, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(91, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(14.29, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(14.29, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 801,
                            Type = WeatherConditionType.Clouds,
                            Description = "Ein paar Wolken",
                            IconId = "02n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(203, AngleUnit.Degree),
                        Speed = new Speed(1.99d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(13d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = new PrecipitationVolume
                        {
                        Last3h = new Length(0.13, LengthUnit.Millimeter),
                        Last1h = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(18.79, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(18.89, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(83, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(18.79, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(18.79, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(950, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 500,
                            Type = WeatherConditionType.Rain,
                            Description = "Leichter Regen",
                            IconId = "10d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(218, AngleUnit.Degree),
                        Speed = new Speed(1.2d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(7d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = new PrecipitationVolume
                        {
                        Last3h = new Length(0.93, LengthUnit.Millimeter),
                        Last1h = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(23.13, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(23.3, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(23.13, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(23.13, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 500,
                            Type = WeatherConditionType.Rain,
                            Description = "Leichter Regen",
                            IconId = "10d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(286, AngleUnit.Degree),
                        Speed = new Speed(1.76d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(37d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = new PrecipitationVolume
                        {
                        Last3h = new Length(0.2, LengthUnit.Millimeter),
                        Last1h = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(26.88, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(26.92, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(43, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(26.88, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(26.88, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 500,
                            Type = WeatherConditionType.Rain,
                            Description = "Leichter Regen",
                            IconId = "10d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(302, AngleUnit.Degree),
                        Speed = new Speed(2.81d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(87d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(24.58, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(24.53, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(55, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(24.58, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(24.58, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 804,
                            Type = WeatherConditionType.Clouds,
                            Description = "Bedeckt",
                            IconId = "04d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(338, AngleUnit.Degree),
                        Speed = new Speed(1.98d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(62d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(22.4, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(22.5, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(22.4, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(22.4, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 803,
                            Type = WeatherConditionType.Clouds,
                            Description = "Überwiegend bewölkt",
                            IconId = "04d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(335, AngleUnit.Degree),
                        Speed = new Speed(1.29d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(96d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(17.66, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(17.49, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(17.66, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(17.66, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 804,
                            Type = WeatherConditionType.Clouds,
                            Description = "Bedeckt",
                            IconId = "04n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(179, AngleUnit.Degree),
                        Speed = new Speed(1.25d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(79d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(15.49, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(15.31, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(85, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(15.49, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(15.49, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 803,
                            Type = WeatherConditionType.Clouds,
                            Description = "Überwiegend bewölkt",
                            IconId = "04n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(168, AngleUnit.Degree),
                        Speed = new Speed(1.22d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(37d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(14.38, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(87, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 802,
                            Type = WeatherConditionType.Clouds,
                            Description = "Mäßig bewölkt",
                            IconId = "03n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(173, AngleUnit.Degree),
                        Speed = new Speed(1.53d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(64d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(19.06, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(18.9, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(72, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(19.06, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(19.06, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1024, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1024, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 803,
                            Type = WeatherConditionType.Clouds,
                            Description = "Überwiegend bewölkt",
                            IconId = "04d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(91, AngleUnit.Degree),
                        Speed = new Speed(0.41d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(68d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(24.75, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(24.64, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(52, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(24.75, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(24.75, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 803,
                            Type = WeatherConditionType.Clouds,
                            Description = "Überwiegend bewölkt",
                            IconId = "04d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(41, AngleUnit.Degree),
                        Speed = new Speed(1.29d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(71d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(28.03, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(27.5, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(37, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(28.03, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(28.03, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(954, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 803,
                            Type = WeatherConditionType.Clouds,
                            Description = "Überwiegend bewölkt",
                            IconId = "04d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(8, AngleUnit.Degree),
                        Speed = new Speed(2.43d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(55d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(28.3, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(27.72, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(37, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(28.3, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(28.3, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 803,
                            Type = WeatherConditionType.Clouds,
                            Description = "Überwiegend bewölkt",
                            IconId = "04d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(28, AngleUnit.Degree),
                        Speed = new Speed(1.81d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(61d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(23.73, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(23.91, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(67, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(23.73, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(23.73, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 803,
                            Type = WeatherConditionType.Clouds,
                            Description = "Überwiegend bewölkt",
                            IconId = "04d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(96, AngleUnit.Degree),
                        Speed = new Speed(0.77d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(93d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(18.61, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(18.43, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(73, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(18.61, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(18.61, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 804,
                            Type = WeatherConditionType.Clouds,
                            Description = "Bedeckt",
                            IconId = "04n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(171, AngleUnit.Degree),
                        Speed = new Speed(1.6d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(93d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(17.25, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(17.01, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(76, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(17.25, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(17.25, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 804,
                            Type = WeatherConditionType.Clouds,
                            Description = "Bedeckt",
                            IconId = "04n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(162, AngleUnit.Degree),
                        Speed = new Speed(1.59d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(35d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(16.34, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(15.99, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(75, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(16.34, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(16.34, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 802,
                            Type = WeatherConditionType.Clouds,
                            Description = "Mäßig bewölkt",
                            IconId = "03n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(157, AngleUnit.Degree),
                        Speed = new Speed(1.74d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(23d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(20.74, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(20.57, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(65, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(20.74, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(20.74, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 801,
                            Type = WeatherConditionType.Clouds,
                            Description = "Ein paar Wolken",
                            IconId = "02d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(108, AngleUnit.Degree),
                        Speed = new Speed(0.95d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(1d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(27.2, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(27.18, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(43, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(27.2, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(27.2, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(53, AngleUnit.Degree),
                        Speed = new Speed(2.06d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(1d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(30.82, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(29.84, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(33, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(30.82, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(30.82, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1017, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(56, AngleUnit.Degree),
                        Speed = new Speed(3.13d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(0d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(31.58, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(30.69, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(33, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(31.58, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(31.58, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1015, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1015, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(51, AngleUnit.Degree),
                        Speed = new Speed(3.19d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(0d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(25.86, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(26.17, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(64, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(25.86, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(25.86, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1014, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(946, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1014, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(59, AngleUnit.Degree),
                        Speed = new Speed(0.32d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(0d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(21.31, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(21.3, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(21.31, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(21.31, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1015, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(945, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1015, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(184, AngleUnit.Degree),
                        Speed = new Speed(2.14d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(1d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(19.56, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(19.43, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(71, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(19.56, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(19.56, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1013, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(944, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1013, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(178, AngleUnit.Degree),
                        Speed = new Speed(2.32d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(0d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(18.01, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(17.67, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(18.01, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(18.01, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1013, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(943, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1013, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01n"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(187, AngleUnit.Degree),
                        Speed = new Speed(2.32d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(0d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(21.82, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(21.75, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(65, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(21.82, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(21.82, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1012, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(943, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1012, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(183, AngleUnit.Degree),
                        Speed = new Speed(1.17d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(0d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(28.8, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(28.49, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(41, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(28.8, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(28.8, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1011, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(944, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1011, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(306, AngleUnit.Degree),
                        Speed = new Speed(1.19d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(0d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(31.74, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(31.01, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(34, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(31.74, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(31.74, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1010, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(943, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1010, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 800,
                            Type = WeatherConditionType.Clear,
                            Description = "Klarer Himmel",
                            IconId = "01d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(315, AngleUnit.Degree),
                        Speed = new Speed(2.03d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(24d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = new PrecipitationVolume
                        {
                        Last3h = new Length(1.77, LengthUnit.Millimeter),
                        Last1h = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(23.93, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(24.36, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(76, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(23.93, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(23.93, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1010, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(942, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1010, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 500,
                            Type = WeatherConditionType.Rain,
                            Description = "Leichter Regen",
                            IconId = "10d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(203, AngleUnit.Degree),
                        Speed = new Speed(1.63d, SpeedUnit.MeterPerSecond)
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = new Ratio(49d, RatioUnit.Percent),
                        Today = new Ratio(0d, RatioUnit.Percent)
                        },
                        Rain = new PrecipitationVolume
                        {
                        Last3h = new Length(2.84, LengthUnit.Millimeter),
                        Last1h = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(20.36, TemperatureUnit.DegreeCelsius),
                        FeelsLike = new Temperature(20.8, TemperatureUnit.DegreeCelsius),
                        Humidity = new RelativeHumidity(90, RelativeHumidityUnit.Percent),
                        MaximumTemperature = new Temperature(20.36, TemperatureUnit.DegreeCelsius),
                        MinimumTemperature = new Temperature(20.36, TemperatureUnit.DegreeCelsius),
                        Pressure = new Pressure(1013, PressureUnit.Hectopascal),
                        GroundLevel = new Pressure(944, PressureUnit.Hectopascal),
                        SeaLevel = new Pressure(1013, PressureUnit.Hectopascal)
                        },
                        WeatherConditions = new List<WeatherCondition>
                        {
                        new WeatherCondition
                        {
                            Id = 500,
                            Type = WeatherConditionType.Rain,
                            Description = "Leichter Regen",
                            IconId = "10d"
                        }
                        },
                        Wind = new WindInfo
                        {
                        Direction = new Angle(149, AngleUnit.Degree),
                        Speed = new Speed(3.47d, SpeedUnit.MeterPerSecond)
                        }
                    }
                },
                    City = new City
                    {
                        Id = 2659685,
                        Name = "Menznau",
                        Coordinates = new Coordinates
                        {
                            Latitude = 47.0907d,
                            Longitude = 8.0559d
                        },
                        Country = new RegionInfo("CH"),
                        Population = 2628,
                        Timezone = 7200,
                        Sunrise = DateTime.ParseExact("2022-06-14T03:31:53.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-06-14T19:24:17.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
                    }
                };

            }
        }

        internal static class Hourly
        {
            internal static WeatherForecast GetTestWeatherForecast()
            {
                return new WeatherForecast
                {
                    Code = "200",
                    Count = 96,
                    Items = new List<WeatherForecastItem>
                      {
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-14T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(56d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.16, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(15.76, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(74, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(16.16, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14.22, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(167, AngleUnit.Degree),
                            Speed = new Speed(1.38d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-14T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(49d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(15.58, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(15.18, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(76, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(15.58, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(176, AngleUnit.Degree),
                            Speed = new Speed(1.45d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(41d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.95, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(14.51, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14.95, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(13.82, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(198, AngleUnit.Degree),
                            Speed = new Speed(1.52d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(25d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.3, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(13.85, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(79, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14.3, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(13.71, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1019, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(196, AngleUnit.Degree),
                            Speed = new Speed(1.75d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(11d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(13.7, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(13.19, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(79, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(13.7, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(13.7, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1019, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 801,
                              Type = WeatherConditionType.Clouds,
                              Description = "Ein paar Wolken",
                              IconId = "02n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(189, AngleUnit.Degree),
                            Speed = new Speed(1.9d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(8d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(13.77, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(13.21, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(13.77, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(13.77, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1019, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(179, AngleUnit.Degree),
                            Speed = new Speed(1.72d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(6d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(13.96, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(13.42, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(13.96, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(13.96, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(190, AngleUnit.Degree),
                            Speed = new Speed(1.51d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(6d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(15.86, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(15.51, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(15.86, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(15.86, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(199, AngleUnit.Degree),
                            Speed = new Speed(1.27d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(6d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(19.04, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(18.75, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(67, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(19.04, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(19.04, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(216, AngleUnit.Degree),
                            Speed = new Speed(0.73d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(19d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(21.42, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(21.18, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(60, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(21.42, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(21.42, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 801,
                              Type = WeatherConditionType.Clouds,
                              Description = "Ein paar Wolken",
                              IconId = "02d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(286, AngleUnit.Degree),
                            Speed = new Speed(0.88d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(26d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(0.1, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.33, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(23.13, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(54, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(23.33, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(23.33, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 500,
                              Type = WeatherConditionType.Rain,
                              Description = "Leichter Regen",
                              IconId = "10d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(289, AngleUnit.Degree),
                            Speed = new Speed(0.98d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(29d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(25.57, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(25.36, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(45, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(25.57, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(25.57, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(299, AngleUnit.Degree),
                            Speed = new Speed(1.07d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(22d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(27.13, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(26.96, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(40, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(27.13, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(27.13, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1017, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 801,
                              Type = WeatherConditionType.Clouds,
                              Description = "Ein paar Wolken",
                              IconId = "02d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(292, AngleUnit.Degree),
                            Speed = new Speed(1.6d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(20d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.26, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(27.69, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(37, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(28.26, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(28.26, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1017, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 801,
                              Type = WeatherConditionType.Clouds,
                              Description = "Ein paar Wolken",
                              IconId = "02d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(284, AngleUnit.Degree),
                            Speed = new Speed(2.36d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(25d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(29, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(28.27, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(36, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(29, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(29, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(280, AngleUnit.Degree),
                            Speed = new Speed(3.2d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(44d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(29.44, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(28.61, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(35, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(29.44, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(29.44, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(272, AngleUnit.Degree),
                            Speed = new Speed(3.76d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(51d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(29.64, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(28.72, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(34, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(29.64, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(29.64, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(268, AngleUnit.Degree),
                            Speed = new Speed(3.74d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(37d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(29.38, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(28.64, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(36, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(29.38, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(29.38, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(285, AngleUnit.Degree),
                            Speed = new Speed(3.24d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(30d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.28, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(28.23, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(44, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(28.28, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(28.28, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(315, AngleUnit.Degree),
                            Speed = new Speed(2.98d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(25d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.64, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(26.64, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(57, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(26.64, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(26.64, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(342, AngleUnit.Degree),
                            Speed = new Speed(2.3d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(28d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(0.16, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.48, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(23.82, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(74, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(23.48, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(23.48, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 500,
                              Type = WeatherConditionType.Rain,
                              Description = "Leichter Regen",
                              IconId = "10d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(17, AngleUnit.Degree),
                            Speed = new Speed(1.24d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(10d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(0.66, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(20.36, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(20.62, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(83, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(20.36, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(20.36, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1017, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 500,
                              Type = WeatherConditionType.Rain,
                              Description = "Leichter Regen",
                              IconId = "10d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(176, AngleUnit.Degree),
                            Speed = new Speed(1.42d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(18d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(0.81, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(18.12, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(18.29, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(88, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(18.12, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(18.12, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1019, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 500,
                              Type = WeatherConditionType.Rain,
                              Description = "Leichter Regen",
                              IconId = "10n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(194, AngleUnit.Degree),
                            Speed = new Speed(2.88d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(39d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(1.04, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.64, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(16.76, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(92, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(16.64, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(16.64, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1019, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 501,
                              Type = WeatherConditionType.Rain,
                              Description = "Mäßiger Regen",
                              IconId = "10n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(201, AngleUnit.Degree),
                            Speed = new Speed(2.94d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(31d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(1.17, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(95, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 501,
                              Type = WeatherConditionType.Rain,
                              Description = "Mäßiger Regen",
                              IconId = "10n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(161, AngleUnit.Degree),
                            Speed = new Speed(2.94d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(26d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(0.96, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(13.88, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(93, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 500,
                              Type = WeatherConditionType.Rain,
                              Description = "Leichter Regen",
                              IconId = "10n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(179, AngleUnit.Degree),
                            Speed = new Speed(2.7d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(23d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(0.18, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(13.87, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(13.69, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(91, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(13.87, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(13.87, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 500,
                              Type = WeatherConditionType.Rain,
                              Description = "Leichter Regen",
                              IconId = "10n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(197, AngleUnit.Degree),
                            Speed = new Speed(2.63d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(9d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(13.99, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(13.79, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(90, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(13.99, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(13.99, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(202, AngleUnit.Degree),
                            Speed = new Speed(2.52d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(18d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.15, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(14, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(91, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14.15, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14.15, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 801,
                              Type = WeatherConditionType.Clouds,
                              Description = "Ein paar Wolken",
                              IconId = "02n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(201, AngleUnit.Degree),
                            Speed = new Speed(2.44d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(14d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.29, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(14.15, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(91, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14.29, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14.29, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 801,
                              Type = WeatherConditionType.Clouds,
                              Description = "Ein paar Wolken",
                              IconId = "02n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(203, AngleUnit.Degree),
                            Speed = new Speed(1.99d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(13d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.45, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(14.3, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(90, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14.45, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14.45, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 801,
                              Type = WeatherConditionType.Clouds,
                              Description = "Ein paar Wolken",
                              IconId = "02d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(203, AngleUnit.Degree),
                            Speed = new Speed(1.8d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(14d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.1, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(16.09, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(89, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(16.1, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(16.1, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 801,
                              Type = WeatherConditionType.Clouds,
                              Description = "Ein paar Wolken",
                              IconId = "02d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(213, AngleUnit.Degree),
                            Speed = new Speed(1.74d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(13d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(0.11, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(18.79, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(18.89, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(83, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(18.79, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(18.79, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(950, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 500,
                              Type = WeatherConditionType.Rain,
                              Description = "Leichter Regen",
                              IconId = "10d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(218, AngleUnit.Degree),
                            Speed = new Speed(1.2d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(2d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(20.93, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(20.98, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(73, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(20.93, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(20.93, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(950, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(274, AngleUnit.Degree),
                            Speed = new Speed(1.26d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(7d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(0.45, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(21.6, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(21.8, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(76, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(21.6, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(21.6, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 500,
                              Type = WeatherConditionType.Rain,
                              Description = "Leichter Regen",
                              IconId = "10d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(286, AngleUnit.Degree),
                            Speed = new Speed(1.71d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(7d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(0.41, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.13, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(23.3, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(23.13, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(23.13, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 500,
                              Type = WeatherConditionType.Rain,
                              Description = "Leichter Regen",
                              IconId = "10d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(286, AngleUnit.Degree),
                            Speed = new Speed(1.76d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(8d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = new PrecipitationVolume
                          {
                            Last3h = null,
                            Last1h = new Length(0.11, LengthUnit.Millimeter)
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(25.26, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(25.2, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(52, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(25.26, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(25.26, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 500,
                              Type = WeatherConditionType.Rain,
                              Description = "Leichter Regen",
                              IconId = "10d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(286, AngleUnit.Degree),
                            Speed = new Speed(2.12d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(24d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.27, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(26.27, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(46, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(26.27, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(26.27, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 801,
                              Type = WeatherConditionType.Clouds,
                              Description = "Ein paar Wolken",
                              IconId = "02d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(294, AngleUnit.Degree),
                            Speed = new Speed(2.4d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(37d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.88, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(26.92, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(43, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(26.88, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(26.88, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(302, AngleUnit.Degree),
                            Speed = new Speed(2.81d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(100d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(27.28, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(27.07, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(40, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(27.28, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(27.28, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(313, AngleUnit.Degree),
                            Speed = new Speed(3.25d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(97d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.53, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(26.53, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(44, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(26.53, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(26.53, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(328, AngleUnit.Degree),
                            Speed = new Speed(3.58d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(87d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(24.58, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(24.53, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(55, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(24.58, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(24.58, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(338, AngleUnit.Degree),
                            Speed = new Speed(1.98d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(74d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(25.67, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(25.49, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(46, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(25.67, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(25.67, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(340, AngleUnit.Degree),
                            Speed = new Speed(2.2d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(65d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(24.81, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(24.76, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(54, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(24.81, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(24.81, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(338, AngleUnit.Degree),
                            Speed = new Speed(1.84d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(62d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(22.4, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(22.5, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(22.4, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(22.4, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(335, AngleUnit.Degree),
                            Speed = new Speed(1.29d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(97d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(20.11, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(20.06, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(72, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(20.11, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(20.11, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(327, AngleUnit.Degree),
                            Speed = new Speed(0.39d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(94d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(18.38, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(18.23, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(75, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(18.38, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(18.38, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(187, AngleUnit.Degree),
                            Speed = new Speed(0.22d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(96d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(17.66, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(17.49, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(77, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(17.66, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(17.66, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(179, AngleUnit.Degree),
                            Speed = new Speed(1.25d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(97d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(17.29, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(17.11, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(78, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(17.29, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(17.29, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1024, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1024, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(192, AngleUnit.Degree),
                            Speed = new Speed(0.67d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(90d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.19, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(16.01, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(82, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(16.19, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(16.19, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(179, AngleUnit.Degree),
                            Speed = new Speed(0.9d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(79d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(15.49, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(15.31, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(85, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(15.49, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(15.49, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(168, AngleUnit.Degree),
                            Speed = new Speed(1.22d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(9d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.93, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(14.75, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(87, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14.93, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14.93, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(171, AngleUnit.Degree),
                            Speed = new Speed(1.05d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(9d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.53, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(14.34, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(88, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14.53, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14.53, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(180, AngleUnit.Degree),
                            Speed = new Speed(1.04d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(37d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(14.38, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(87, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14.59, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(173, AngleUnit.Degree),
                            Speed = new Speed(1.53d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(53d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.85, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(14.61, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(85, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(14.85, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(14.85, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1024, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1024, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(179, AngleUnit.Degree),
                            Speed = new Speed(1.22d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(62d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.47, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(16.31, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(82, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(16.47, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(16.47, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1024, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1024, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(163, AngleUnit.Degree),
                            Speed = new Speed(0.95d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(64d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(19.06, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(18.9, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(72, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(19.06, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(19.06, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1024, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1024, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(91, AngleUnit.Degree),
                            Speed = new Speed(0.41d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(62d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(21.38, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(21.24, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(64, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(21.38, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(21.38, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(72, AngleUnit.Degree),
                            Speed = new Speed(0.79d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(53d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.26, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(23.13, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(57, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(23.26, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(23.26, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(39, AngleUnit.Degree),
                            Speed = new Speed(1.45d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(68d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(24.75, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(24.64, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(52, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(24.75, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(24.75, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(41, AngleUnit.Degree),
                            Speed = new Speed(1.29d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(72d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.5, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(26.5, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(45, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(26.5, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(26.5, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(27, AngleUnit.Degree),
                            Speed = new Speed(1.53d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(74d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(27.47, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(27.17, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(39, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(27.47, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(27.47, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(11, AngleUnit.Degree),
                            Speed = new Speed(2.24d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(71d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.03, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(27.5, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(37, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(28.03, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(28.03, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(954, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(8, AngleUnit.Degree),
                            Speed = new Speed(2.43d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(42d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.48, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(27.81, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(36, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(28.48, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(28.48, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(20, AngleUnit.Degree),
                            Speed = new Speed(1.94d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(54d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.58, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(27.9, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(36, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(28.58, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(28.58, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(24, AngleUnit.Degree),
                            Speed = new Speed(1.83d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(55d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.3, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(27.72, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(37, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(28.3, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(28.3, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(28, AngleUnit.Degree),
                            Speed = new Speed(1.81d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(66d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(27.49, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(27.36, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(42, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(27.49, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(27.49, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(29, AngleUnit.Degree),
                            Speed = new Speed(1.79d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(59d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.42, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(26.42, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(55, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(26.42, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(26.42, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(41, AngleUnit.Degree),
                            Speed = new Speed(1.42d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(61d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.73, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(23.91, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(67, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(23.73, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(23.73, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(96, AngleUnit.Degree),
                            Speed = new Speed(0.77d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(86d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(20.64, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(20.64, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(72, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(20.64, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(20.64, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(154, AngleUnit.Degree),
                            Speed = new Speed(0.93d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(89d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(19.27, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(19.13, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(72, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(19.27, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(19.27, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(164, AngleUnit.Degree),
                            Speed = new Speed(1.37d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(93d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(18.61, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(18.43, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(73, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(18.61, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(18.61, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(171, AngleUnit.Degree),
                            Speed = new Speed(1.6d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(95d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(18.04, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(17.83, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(74, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(18.04, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(18.04, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(953, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(173, AngleUnit.Degree),
                            Speed = new Speed(1.63d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(95d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(17.56, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(17.36, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(76, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(17.56, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(17.56, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(164, AngleUnit.Degree),
                            Speed = new Speed(1.62d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(93d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(17.25, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(17.01, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(76, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(17.25, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(17.25, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 804,
                              Type = WeatherConditionType.Clouds,
                              Description = "Bedeckt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(162, AngleUnit.Degree),
                            Speed = new Speed(1.59d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(61d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.84, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(16.56, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(76, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(16.84, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(16.84, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(952, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 803,
                              Type = WeatherConditionType.Clouds,
                              Description = "Überwiegend bewölkt",
                              IconId = "04n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(160, AngleUnit.Degree),
                            Speed = new Speed(1.52d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(48d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.5, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(16.19, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(76, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(16.5, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(16.5, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1023, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1023, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(161, AngleUnit.Degree),
                            Speed = new Speed(1.53d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(35d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.34, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(15.99, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(75, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(16.34, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(16.34, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(157, AngleUnit.Degree),
                            Speed = new Speed(1.74d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(31d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.07, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(15.69, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(75, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(16.07, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(16.07, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(151, AngleUnit.Degree),
                            Speed = new Speed(1.24d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(27d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(17.69, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(17.45, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(74, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(17.69, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(17.69, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1022, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1022, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 802,
                              Type = WeatherConditionType.Clouds,
                              Description = "Mäßig bewölkt",
                              IconId = "03d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(143, AngleUnit.Degree),
                            Speed = new Speed(1.61d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(23d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(20.74, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(20.57, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(65, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(20.74, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(20.74, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 801,
                              Type = WeatherConditionType.Clouds,
                              Description = "Ein paar Wolken",
                              IconId = "02d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(108, AngleUnit.Degree),
                            Speed = new Speed(0.95d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(1d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.33, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(23.15, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(55, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(23.33, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(23.33, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1021, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1021, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(74, AngleUnit.Degree),
                            Speed = new Speed(1.09d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(1d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(25.48, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(25.34, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(48, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(25.48, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(25.48, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(51, AngleUnit.Degree),
                            Speed = new Speed(1.48d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(1d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(27.2, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(27.18, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(43, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(27.2, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(27.2, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1020, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1020, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(53, AngleUnit.Degree),
                            Speed = new Speed(2.06d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(2d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.67, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(28.12, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(38, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(28.67, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(28.67, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1019, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(951, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1019, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(60, AngleUnit.Degree),
                            Speed = new Speed(2.6d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(2d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(29.91, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(29.08, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(35, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(29.91, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(29.91, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1018, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(950, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1018, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(59, AngleUnit.Degree),
                            Speed = new Speed(2.85d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(1d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(30.82, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(29.84, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(33, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(30.82, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(30.82, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1017, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1017, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(56, AngleUnit.Degree),
                            Speed = new Speed(3.13d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(0d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(31.56, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(30.55, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(32, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(31.56, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(31.56, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1016, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(949, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1016, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(54, AngleUnit.Degree),
                            Speed = new Speed(3.25d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(0d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(31.87, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(30.9, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(32, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(31.87, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(31.87, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1015, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1015, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(55, AngleUnit.Degree),
                            Speed = new Speed(3.29d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(0d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(31.58, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(30.69, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(33, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(31.58, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(31.58, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1015, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(948, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1015, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(51, AngleUnit.Degree),
                            Speed = new Speed(3.19d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(0d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(30.66, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(30.35, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(39, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(30.66, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(30.66, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1014, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1014, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(46, AngleUnit.Degree),
                            Speed = new Speed(2.44d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(0d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.71, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(29.64, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(53, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(28.71, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(28.71, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1014, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(947, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1014, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(47, AngleUnit.Degree),
                            Speed = new Speed(1.49d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(0d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(25.86, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(26.17, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(64, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(25.86, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(25.86, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1014, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(946, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1014, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(59, AngleUnit.Degree),
                            Speed = new Speed(0.32d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(0d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.01, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(23.17, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(23.01, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(23.01, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1015, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(946, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1015, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01d"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(195, AngleUnit.Degree),
                            Speed = new Speed(1.38d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(0d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(21.97, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(22.02, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(21.97, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(21.97, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1015, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(946, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1015, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(193, AngleUnit.Degree),
                            Speed = new Speed(1.9d, SpeedUnit.MeterPerSecond)
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = new Ratio(0d, RatioUnit.Percent),
                            Today = new Ratio(0d, RatioUnit.Percent)
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(21.31, TemperatureUnit.DegreeCelsius),
                            FeelsLike = new Temperature(21.3, TemperatureUnit.DegreeCelsius),
                            Humidity = new RelativeHumidity(69, RelativeHumidityUnit.Percent),
                            MaximumTemperature = new Temperature(21.31, TemperatureUnit.DegreeCelsius),
                            MinimumTemperature = new Temperature(21.31, TemperatureUnit.DegreeCelsius),
                            Pressure = new Pressure(1015, PressureUnit.Hectopascal),
                            GroundLevel = new Pressure(945, PressureUnit.Hectopascal),
                            SeaLevel = new Pressure(1015, PressureUnit.Hectopascal)
                          },
                          WeatherConditions = new List<WeatherCondition>
                          {
                            new WeatherCondition
                            {
                              Id = 800,
                              Type = WeatherConditionType.Clear,
                              Description = "Klarer Himmel",
                              IconId = "01n"
                            }
                          },
                          Wind = new WindInfo
                          {
                            Direction = new Angle(184, AngleUnit.Degree),
                            Speed = new Speed(2.14d, SpeedUnit.MeterPerSecond)
                          }
                        }
                      },
                    City = new City
                    {
                        Id = 2659685,
                        Name = "Menznau",
                        Coordinates = new Coordinates
                        {
                            Latitude = 47.0907d,
                            Longitude = 8.0559d
                        },
                        Country = new RegionInfo("ch"),
                        Population = 2628,
                        Timezone = 7200,
                        Sunrise = DateTime.ParseExact("2022-06-14T03:31:53.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Sunset = DateTime.ParseExact("2022-06-14T19:24:17.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
                    }
                };

            }
        }
    }
}

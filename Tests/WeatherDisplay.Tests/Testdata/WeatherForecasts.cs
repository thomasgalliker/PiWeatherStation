using System;
using System.Collections.Generic;
using System.Globalization;
using WeatherDisplay.Model.OpenWeatherMap;

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
                        All = 59d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(16.92, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(16.47, TemperatureUnit.Celsius),
                        Humidity = new Humidity(69),
                        MaximumTemperature = new Temperature(16.92, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(14.56, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1018),
                        GroundLevel = new Pressure(947),
                        SeaLevel = new Pressure(1018)
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
                        Wind = new WindInformation
                        {
                        Direction = 160d,
                        Speed = 1.29d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 49d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(15.89, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(15.44, TemperatureUnit.Celsius),
                        Humidity = new Humidity(73),
                        MaximumTemperature = new Temperature(15.89, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(13.82, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1018),
                        GroundLevel = new Pressure(947),
                        SeaLevel = new Pressure(1018)
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
                        Wind = new WindInformation
                        {
                        Direction = 198d,
                        Speed = 1.52d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 25d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(14.82, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(14.32, TemperatureUnit.Celsius),
                        Humidity = new Humidity(75),
                        MaximumTemperature = new Temperature(14.82, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(13.77, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1019),
                        GroundLevel = new Pressure(947),
                        SeaLevel = new Pressure(1019)
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
                        Wind = new WindInformation
                        {
                        Direction = 179d,
                        Speed = 1.72d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 6d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(19.04, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(18.75, TemperatureUnit.Celsius),
                        Humidity = new Humidity(67),
                        MaximumTemperature = new Temperature(19.04, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(19.04, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1018),
                        GroundLevel = new Pressure(948),
                        SeaLevel = new Pressure(1018)
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
                        Wind = new WindInformation
                        {
                        Direction = 216d,
                        Speed = 0.73d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 29d,
                        Today = 0d
                        },
                        Rain = new RainInformation
                        {
                        VolumeLast3Hours = 0.12d,
                        VolumeLastHour = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(25.57, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(25.36, TemperatureUnit.Celsius),
                        Humidity = new Humidity(45),
                        MaximumTemperature = new Temperature(25.57, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(25.57, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1018),
                        GroundLevel = new Pressure(949),
                        SeaLevel = new Pressure(1018)
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
                        Wind = new WindInformation
                        {
                        Direction = 299d,
                        Speed = 1.07d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 25d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(29, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(28.27, TemperatureUnit.Celsius),
                        Humidity = new Humidity(36),
                        MaximumTemperature = new Temperature(29, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(29, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1016),
                        GroundLevel = new Pressure(949),
                        SeaLevel = new Pressure(1016)
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
                        Wind = new WindInformation
                        {
                        Direction = 280d,
                        Speed = 3.2d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 37d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(29.38, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(28.64, TemperatureUnit.Celsius),
                        Humidity = new Humidity(36),
                        MaximumTemperature = new Temperature(29.38, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(29.38, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1016),
                        GroundLevel = new Pressure(948),
                        SeaLevel = new Pressure(1016)
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
                        Wind = new WindInformation
                        {
                        Direction = 285d,
                        Speed = 3.24d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 28d,
                        Today = 0d
                        },
                        Rain = new RainInformation
                        {
                        VolumeLast3Hours = 0.16d,
                        VolumeLastHour = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(23.48, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(23.82, TemperatureUnit.Celsius),
                        Humidity = new Humidity(74),
                        MaximumTemperature = new Temperature(23.48, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(23.48, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1016),
                        GroundLevel = new Pressure(947),
                        SeaLevel = new Pressure(1016)
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
                        Wind = new WindInformation
                        {
                        Direction = 17d,
                        Speed = 1.24d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-15T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 39d,
                        Today = 0d
                        },
                        Rain = new RainInformation
                        {
                        VolumeLast3Hours = 2.51d,
                        VolumeLastHour = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(16.64, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(16.76, TemperatureUnit.Celsius),
                        Humidity = new Humidity(92),
                        MaximumTemperature = new Temperature(16.64, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(16.64, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1019),
                        GroundLevel = new Pressure(948),
                        SeaLevel = new Pressure(1019)
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
                        Wind = new WindInformation
                        {
                        Direction = 201d,
                        Speed = 2.94d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 23d,
                        Today = 0d
                        },
                        Rain = new RainInformation
                        {
                        VolumeLast3Hours = 2.31d,
                        VolumeLastHour = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(13.87, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(13.69, TemperatureUnit.Celsius),
                        Humidity = new Humidity(91),
                        MaximumTemperature = new Temperature(13.87, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(13.87, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1020),
                        GroundLevel = new Pressure(949),
                        SeaLevel = new Pressure(1020)
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
                        Wind = new WindInformation
                        {
                        Direction = 197d,
                        Speed = 2.63d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 14d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(14.29, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(14.15, TemperatureUnit.Celsius),
                        Humidity = new Humidity(91),
                        MaximumTemperature = new Temperature(14.29, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(14.29, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1020),
                        GroundLevel = new Pressure(949),
                        SeaLevel = new Pressure(1020)
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
                        Wind = new WindInformation
                        {
                        Direction = 203d,
                        Speed = 1.99d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 13d,
                        Today = 0d
                        },
                        Rain = new RainInformation
                        {
                        VolumeLast3Hours = 0.13d,
                        VolumeLastHour = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(18.79, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(18.89, TemperatureUnit.Celsius),
                        Humidity = new Humidity(83),
                        MaximumTemperature = new Temperature(18.79, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(18.79, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1020),
                        GroundLevel = new Pressure(950),
                        SeaLevel = new Pressure(1020)
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
                        Wind = new WindInformation
                        {
                        Direction = 218d,
                        Speed = 1.2d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 7d,
                        Today = 0d
                        },
                        Rain = new RainInformation
                        {
                        VolumeLast3Hours = 0.93d,
                        VolumeLastHour = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(23.13, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(23.3, TemperatureUnit.Celsius),
                        Humidity = new Humidity(69),
                        MaximumTemperature = new Temperature(23.13, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(23.13, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1021),
                        GroundLevel = new Pressure(951),
                        SeaLevel = new Pressure(1021)
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
                        Wind = new WindInformation
                        {
                        Direction = 286d,
                        Speed = 1.76d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 37d,
                        Today = 0d
                        },
                        Rain = new RainInformation
                        {
                        VolumeLast3Hours = 0.2d,
                        VolumeLastHour = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(26.88, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(26.92, TemperatureUnit.Celsius),
                        Humidity = new Humidity(43),
                        MaximumTemperature = new Temperature(26.88, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(26.88, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1020),
                        GroundLevel = new Pressure(952),
                        SeaLevel = new Pressure(1020)
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
                        Wind = new WindInformation
                        {
                        Direction = 302d,
                        Speed = 2.81d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 87d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(24.58, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(24.53, TemperatureUnit.Celsius),
                        Humidity = new Humidity(55),
                        MaximumTemperature = new Temperature(24.58, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(24.58, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1021),
                        GroundLevel = new Pressure(952),
                        SeaLevel = new Pressure(1021)
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
                        Wind = new WindInformation
                        {
                        Direction = 338d,
                        Speed = 1.98d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 62d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(22.4, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(22.5, TemperatureUnit.Celsius),
                        Humidity = new Humidity(69),
                        MaximumTemperature = new Temperature(22.4, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(22.4, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1021),
                        GroundLevel = new Pressure(952),
                        SeaLevel = new Pressure(1021)
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
                        Wind = new WindInformation
                        {
                        Direction = 335d,
                        Speed = 1.29d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-16T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 96d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(17.66, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(17.49, TemperatureUnit.Celsius),
                        Humidity = new Humidity(77),
                        MaximumTemperature = new Temperature(17.66, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(17.66, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1023),
                        GroundLevel = new Pressure(953),
                        SeaLevel = new Pressure(1023)
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
                        Wind = new WindInformation
                        {
                        Direction = 179d,
                        Speed = 1.25d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 79d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(15.49, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(15.31, TemperatureUnit.Celsius),
                        Humidity = new Humidity(85),
                        MaximumTemperature = new Temperature(15.49, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(15.49, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1023),
                        GroundLevel = new Pressure(952),
                        SeaLevel = new Pressure(1023)
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
                        Wind = new WindInformation
                        {
                        Direction = 168d,
                        Speed = 1.22d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 37d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(14.59, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(14.38, TemperatureUnit.Celsius),
                        Humidity = new Humidity(87),
                        MaximumTemperature = new Temperature(14.59, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(14.59, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1023),
                        GroundLevel = new Pressure(952),
                        SeaLevel = new Pressure(1023)
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
                        Wind = new WindInformation
                        {
                        Direction = 173d,
                        Speed = 1.53d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 64d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(19.06, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(18.9, TemperatureUnit.Celsius),
                        Humidity = new Humidity(72),
                        MaximumTemperature = new Temperature(19.06, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(19.06, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1024),
                        GroundLevel = new Pressure(953),
                        SeaLevel = new Pressure(1024)
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
                        Wind = new WindInformation
                        {
                        Direction = 91d,
                        Speed = 0.41d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 68d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(24.75, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(24.64, TemperatureUnit.Celsius),
                        Humidity = new Humidity(52),
                        MaximumTemperature = new Temperature(24.75, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(24.75, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1023),
                        GroundLevel = new Pressure(953),
                        SeaLevel = new Pressure(1023)
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
                        Wind = new WindInformation
                        {
                        Direction = 41d,
                        Speed = 1.29d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 71d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(28.03, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(27.5, TemperatureUnit.Celsius),
                        Humidity = new Humidity(37),
                        MaximumTemperature = new Temperature(28.03, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(28.03, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1022),
                        GroundLevel = new Pressure(954),
                        SeaLevel = new Pressure(1022)
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
                        Wind = new WindInformation
                        {
                        Direction = 8d,
                        Speed = 2.43d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 55d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(28.3, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(27.72, TemperatureUnit.Celsius),
                        Humidity = new Humidity(37),
                        MaximumTemperature = new Temperature(28.3, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(28.3, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1021),
                        GroundLevel = new Pressure(953),
                        SeaLevel = new Pressure(1021)
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
                        Wind = new WindInformation
                        {
                        Direction = 28d,
                        Speed = 1.81d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 61d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(23.73, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(23.91, TemperatureUnit.Celsius),
                        Humidity = new Humidity(67),
                        MaximumTemperature = new Temperature(23.73, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(23.73, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1022),
                        GroundLevel = new Pressure(952),
                        SeaLevel = new Pressure(1022)
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
                        Wind = new WindInformation
                        {
                        Direction = 96d,
                        Speed = 0.77d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-17T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 93d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(18.61, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(18.43, TemperatureUnit.Celsius),
                        Humidity = new Humidity(73),
                        MaximumTemperature = new Temperature(18.61, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(18.61, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1023),
                        GroundLevel = new Pressure(953),
                        SeaLevel = new Pressure(1023)
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
                        Wind = new WindInformation
                        {
                        Direction = 171d,
                        Speed = 1.6d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 93d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(17.25, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(17.01, TemperatureUnit.Celsius),
                        Humidity = new Humidity(76),
                        MaximumTemperature = new Temperature(17.25, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(17.25, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1023),
                        GroundLevel = new Pressure(952),
                        SeaLevel = new Pressure(1023)
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
                        Wind = new WindInformation
                        {
                        Direction = 162d,
                        Speed = 1.59d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 35d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(16.34, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(15.99, TemperatureUnit.Celsius),
                        Humidity = new Humidity(75),
                        MaximumTemperature = new Temperature(16.34, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(16.34, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1022),
                        GroundLevel = new Pressure(951),
                        SeaLevel = new Pressure(1022)
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
                        Wind = new WindInformation
                        {
                        Direction = 157d,
                        Speed = 1.74d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 23d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(20.74, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(20.57, TemperatureUnit.Celsius),
                        Humidity = new Humidity(65),
                        MaximumTemperature = new Temperature(20.74, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(20.74, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1021),
                        GroundLevel = new Pressure(951),
                        SeaLevel = new Pressure(1021)
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
                        Wind = new WindInformation
                        {
                        Direction = 108d,
                        Speed = 0.95d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 1d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(27.2, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(27.18, TemperatureUnit.Celsius),
                        Humidity = new Humidity(43),
                        MaximumTemperature = new Temperature(27.2, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(27.2, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1020),
                        GroundLevel = new Pressure(951),
                        SeaLevel = new Pressure(1020)
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
                        Wind = new WindInformation
                        {
                        Direction = 53d,
                        Speed = 2.06d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 1d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(30.82, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(29.84, TemperatureUnit.Celsius),
                        Humidity = new Humidity(33),
                        MaximumTemperature = new Temperature(30.82, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(30.82, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1017),
                        GroundLevel = new Pressure(949),
                        SeaLevel = new Pressure(1017)
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
                        Wind = new WindInformation
                        {
                        Direction = 56d,
                        Speed = 3.13d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 0d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(31.58, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(30.69, TemperatureUnit.Celsius),
                        Humidity = new Humidity(33),
                        MaximumTemperature = new Temperature(31.58, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(31.58, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1015),
                        GroundLevel = new Pressure(948),
                        SeaLevel = new Pressure(1015)
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
                        Wind = new WindInformation
                        {
                        Direction = 51d,
                        Speed = 3.19d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 0d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(25.86, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(26.17, TemperatureUnit.Celsius),
                        Humidity = new Humidity(64),
                        MaximumTemperature = new Temperature(25.86, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(25.86, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1014),
                        GroundLevel = new Pressure(946),
                        SeaLevel = new Pressure(1014)
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
                        Wind = new WindInformation
                        {
                        Direction = 59d,
                        Speed = 0.32d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-18T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 0d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(21.31, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(21.3, TemperatureUnit.Celsius),
                        Humidity = new Humidity(69),
                        MaximumTemperature = new Temperature(21.31, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(21.31, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1015),
                        GroundLevel = new Pressure(945),
                        SeaLevel = new Pressure(1015)
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
                        Wind = new WindInformation
                        {
                        Direction = 184d,
                        Speed = 2.14d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 1d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(19.56, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(19.43, TemperatureUnit.Celsius),
                        Humidity = new Humidity(71),
                        MaximumTemperature = new Temperature(19.56, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(19.56, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1013),
                        GroundLevel = new Pressure(944),
                        SeaLevel = new Pressure(1013)
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
                        Wind = new WindInformation
                        {
                        Direction = 178d,
                        Speed = 2.32d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 0d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(18.01, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(17.67, TemperatureUnit.Celsius),
                        Humidity = new Humidity(69),
                        MaximumTemperature = new Temperature(18.01, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(18.01, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1013),
                        GroundLevel = new Pressure(943),
                        SeaLevel = new Pressure(1013)
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
                        Wind = new WindInformation
                        {
                        Direction = 187d,
                        Speed = 2.32d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 0d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(21.82, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(21.75, TemperatureUnit.Celsius),
                        Humidity = new Humidity(65),
                        MaximumTemperature = new Temperature(21.82, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(21.82, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1012),
                        GroundLevel = new Pressure(943),
                        SeaLevel = new Pressure(1012)
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
                        Wind = new WindInformation
                        {
                        Direction = 183d,
                        Speed = 1.17d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 0d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(28.8, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(28.49, TemperatureUnit.Celsius),
                        Humidity = new Humidity(41),
                        MaximumTemperature = new Temperature(28.8, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(28.8, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1011),
                        GroundLevel = new Pressure(944),
                        SeaLevel = new Pressure(1011)
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
                        Wind = new WindInformation
                        {
                        Direction = 306d,
                        Speed = 1.19d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 0d,
                        Today = 0d
                        },
                        Rain = null,
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(31.74, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(31.01, TemperatureUnit.Celsius),
                        Humidity = new Humidity(34),
                        MaximumTemperature = new Temperature(31.74, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(31.74, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1010),
                        GroundLevel = new Pressure(943),
                        SeaLevel = new Pressure(1010)
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
                        Wind = new WindInformation
                        {
                        Direction = 315d,
                        Speed = 2.03d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 24d,
                        Today = 0d
                        },
                        Rain = new RainInformation
                        {
                        VolumeLast3Hours = 1.77d,
                        VolumeLastHour = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(23.93, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(24.36, TemperatureUnit.Celsius),
                        Humidity = new Humidity(76),
                        MaximumTemperature = new Temperature(23.93, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(23.93, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1010),
                        GroundLevel = new Pressure(942),
                        SeaLevel = new Pressure(1010)
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
                        Wind = new WindInformation
                        {
                        Direction = 203d,
                        Speed = 1.63d
                        }
                    },
                    new WeatherForecastItem
                    {
                        DateTime = DateTime.ParseExact("2022-06-19T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                        Clouds = new CloudsInformation
                        {
                        All = 49d,
                        Today = 0d
                        },
                        Rain = new RainInformation
                        {
                        VolumeLast3Hours = 2.84d,
                        VolumeLastHour = null
                        },
                        Main = new TemperatureInfo
                        {
                        Temperature = new Temperature(20.36, TemperatureUnit.Celsius),
                        FeelsLike = new Temperature(20.8, TemperatureUnit.Celsius),
                        Humidity = new Humidity(90),
                        MaximumTemperature = new Temperature(20.36, TemperatureUnit.Celsius),
                        MinimumTemperature = new Temperature(20.36, TemperatureUnit.Celsius),
                        Pressure = new Pressure(1013),
                        GroundLevel = new Pressure(944),
                        SeaLevel = new Pressure(1013)
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
                        Wind = new WindInformation
                        {
                        Direction = 149d,
                        Speed = 3.47d
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
                            All = 56d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.16, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(15.76, TemperatureUnit.Celsius),
                            Humidity = new Humidity(74),
                            MaximumTemperature = new Temperature(16.16, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14.22, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1018),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1018)
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
                          Wind = new WindInformation
                          {
                            Direction = 167d,
                            Speed = 1.38d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-14T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 49d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(15.58, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(15.18, TemperatureUnit.Celsius),
                            Humidity = new Humidity(76),
                            MaximumTemperature = new Temperature(15.58, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1018),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1018)
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
                          Wind = new WindInformation
                          {
                            Direction = 176d,
                            Speed = 1.45d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 41d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.95, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(14.51, TemperatureUnit.Celsius),
                            Humidity = new Humidity(77),
                            MaximumTemperature = new Temperature(14.95, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(13.82, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1018),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1018)
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
                          Wind = new WindInformation
                          {
                            Direction = 198d,
                            Speed = 1.52d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 25d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.3, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(13.85, TemperatureUnit.Celsius),
                            Humidity = new Humidity(79),
                            MaximumTemperature = new Temperature(14.3, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(13.71, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1019),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1019)
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
                          Wind = new WindInformation
                          {
                            Direction = 196d,
                            Speed = 1.75d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 11d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(13.7, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(13.19, TemperatureUnit.Celsius),
                            Humidity = new Humidity(79),
                            MaximumTemperature = new Temperature(13.7, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(13.7, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1019),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1019)
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
                          Wind = new WindInformation
                          {
                            Direction = 189d,
                            Speed = 1.9d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 8d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(13.77, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(13.21, TemperatureUnit.Celsius),
                            Humidity = new Humidity(77),
                            MaximumTemperature = new Temperature(13.77, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(13.77, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1019),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1019)
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
                          Wind = new WindInformation
                          {
                            Direction = 179d,
                            Speed = 1.72d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 6d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(13.96, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(13.42, TemperatureUnit.Celsius),
                            Humidity = new Humidity(77),
                            MaximumTemperature = new Temperature(13.96, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(13.96, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1018),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1018)
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
                          Wind = new WindInformation
                          {
                            Direction = 190d,
                            Speed = 1.51d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 6d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(15.86, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(15.51, TemperatureUnit.Celsius),
                            Humidity = new Humidity(77),
                            MaximumTemperature = new Temperature(15.86, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(15.86, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1018),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1018)
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
                          Wind = new WindInformation
                          {
                            Direction = 199d,
                            Speed = 1.27d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 6d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(19.04, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(18.75, TemperatureUnit.Celsius),
                            Humidity = new Humidity(67),
                            MaximumTemperature = new Temperature(19.04, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(19.04, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1018),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1018)
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
                          Wind = new WindInformation
                          {
                            Direction = 216d,
                            Speed = 0.73d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 19d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(21.42, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(21.18, TemperatureUnit.Celsius),
                            Humidity = new Humidity(60),
                            MaximumTemperature = new Temperature(21.42, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(21.42, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1018),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1018)
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
                          Wind = new WindInformation
                          {
                            Direction = 286d,
                            Speed = 0.88d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 26d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 0.1d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.33, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(23.13, TemperatureUnit.Celsius),
                            Humidity = new Humidity(54),
                            MaximumTemperature = new Temperature(23.33, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(23.33, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1018),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1018)
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
                          Wind = new WindInformation
                          {
                            Direction = 289d,
                            Speed = 0.98d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 29d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(25.57, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(25.36, TemperatureUnit.Celsius),
                            Humidity = new Humidity(45),
                            MaximumTemperature = new Temperature(25.57, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(25.57, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1018),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1018)
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
                          Wind = new WindInformation
                          {
                            Direction = 299d,
                            Speed = 1.07d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 22d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(27.13, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(26.96, TemperatureUnit.Celsius),
                            Humidity = new Humidity(40),
                            MaximumTemperature = new Temperature(27.13, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(27.13, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1017),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1017)
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
                          Wind = new WindInformation
                          {
                            Direction = 292d,
                            Speed = 1.6d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 20d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.26, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(27.69, TemperatureUnit.Celsius),
                            Humidity = new Humidity(37),
                            MaximumTemperature = new Temperature(28.26, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(28.26, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1017),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1017)
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
                          Wind = new WindInformation
                          {
                            Direction = 284d,
                            Speed = 2.36d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 25d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(29, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(28.27, TemperatureUnit.Celsius),
                            Humidity = new Humidity(36),
                            MaximumTemperature = new Temperature(29, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(29, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1016),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1016)
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
                          Wind = new WindInformation
                          {
                            Direction = 280d,
                            Speed = 3.2d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 44d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(29.44, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(28.61, TemperatureUnit.Celsius),
                            Humidity = new Humidity(35),
                            MaximumTemperature = new Temperature(29.44, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(29.44, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1016),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1016)
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
                          Wind = new WindInformation
                          {
                            Direction = 272d,
                            Speed = 3.76d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 51d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(29.64, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(28.72, TemperatureUnit.Celsius),
                            Humidity = new Humidity(34),
                            MaximumTemperature = new Temperature(29.64, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(29.64, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1016),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1016)
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
                          Wind = new WindInformation
                          {
                            Direction = 268d,
                            Speed = 3.74d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 37d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(29.38, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(28.64, TemperatureUnit.Celsius),
                            Humidity = new Humidity(36),
                            MaximumTemperature = new Temperature(29.38, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(29.38, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1016),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1016)
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
                          Wind = new WindInformation
                          {
                            Direction = 285d,
                            Speed = 3.24d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 30d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.28, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(28.23, TemperatureUnit.Celsius),
                            Humidity = new Humidity(44),
                            MaximumTemperature = new Temperature(28.28, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(28.28, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1016),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1016)
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
                          Wind = new WindInformation
                          {
                            Direction = 315d,
                            Speed = 2.98d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 25d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.64, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(26.64, TemperatureUnit.Celsius),
                            Humidity = new Humidity(57),
                            MaximumTemperature = new Temperature(26.64, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(26.64, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1016),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1016)
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
                          Wind = new WindInformation
                          {
                            Direction = 342d,
                            Speed = 2.3d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 28d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 0.16d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.48, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(23.82, TemperatureUnit.Celsius),
                            Humidity = new Humidity(74),
                            MaximumTemperature = new Temperature(23.48, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(23.48, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1016),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1016)
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
                          Wind = new WindInformation
                          {
                            Direction = 17d,
                            Speed = 1.24d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 10d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 0.66d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(20.36, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(20.62, TemperatureUnit.Celsius),
                            Humidity = new Humidity(83),
                            MaximumTemperature = new Temperature(20.36, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(20.36, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1017),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1017)
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
                          Wind = new WindInformation
                          {
                            Direction = 176d,
                            Speed = 1.42d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 18d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 0.81d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(18.12, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(18.29, TemperatureUnit.Celsius),
                            Humidity = new Humidity(88),
                            MaximumTemperature = new Temperature(18.12, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(18.12, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1019),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1019)
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
                          Wind = new WindInformation
                          {
                            Direction = 194d,
                            Speed = 2.88d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 39d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 1.04d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.64, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(16.76, TemperatureUnit.Celsius),
                            Humidity = new Humidity(92),
                            MaximumTemperature = new Temperature(16.64, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(16.64, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1019),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1019)
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
                          Wind = new WindInformation
                          {
                            Direction = 201d,
                            Speed = 2.94d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 31d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 1.17d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.59, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(14.59, TemperatureUnit.Celsius),
                            Humidity = new Humidity(95),
                            MaximumTemperature = new Temperature(14.59, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14.59, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 161d,
                            Speed = 2.94d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-15T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 26d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 0.96d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(13.88, TemperatureUnit.Celsius),
                            Humidity = new Humidity(93),
                            MaximumTemperature = new Temperature(14, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 179d,
                            Speed = 2.7d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 23d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 0.18d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(13.87, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(13.69, TemperatureUnit.Celsius),
                            Humidity = new Humidity(91),
                            MaximumTemperature = new Temperature(13.87, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(13.87, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 197d,
                            Speed = 2.63d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 9d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(13.99, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(13.79, TemperatureUnit.Celsius),
                            Humidity = new Humidity(90),
                            MaximumTemperature = new Temperature(13.99, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(13.99, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 202d,
                            Speed = 2.52d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 18d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.15, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(14, TemperatureUnit.Celsius),
                            Humidity = new Humidity(91),
                            MaximumTemperature = new Temperature(14.15, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14.15, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 201d,
                            Speed = 2.44d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 14d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.29, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(14.15, TemperatureUnit.Celsius),
                            Humidity = new Humidity(91),
                            MaximumTemperature = new Temperature(14.29, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14.29, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 203d,
                            Speed = 1.99d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 13d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.45, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(14.3, TemperatureUnit.Celsius),
                            Humidity = new Humidity(90),
                            MaximumTemperature = new Temperature(14.45, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14.45, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 203d,
                            Speed = 1.8d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 14d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.1, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(16.09, TemperatureUnit.Celsius),
                            Humidity = new Humidity(89),
                            MaximumTemperature = new Temperature(16.1, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(16.1, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 213d,
                            Speed = 1.74d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 13d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 0.11d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(18.79, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(18.89, TemperatureUnit.Celsius),
                            Humidity = new Humidity(83),
                            MaximumTemperature = new Temperature(18.79, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(18.79, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(950),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 218d,
                            Speed = 1.2d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 2d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(20.93, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(20.98, TemperatureUnit.Celsius),
                            Humidity = new Humidity(73),
                            MaximumTemperature = new Temperature(20.93, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(20.93, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(950),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 274d,
                            Speed = 1.26d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 7d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 0.45d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(21.6, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(21.8, TemperatureUnit.Celsius),
                            Humidity = new Humidity(76),
                            MaximumTemperature = new Temperature(21.6, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(21.6, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 286d,
                            Speed = 1.71d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 7d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 0.41d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.13, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(23.3, TemperatureUnit.Celsius),
                            Humidity = new Humidity(69),
                            MaximumTemperature = new Temperature(23.13, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(23.13, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 286d,
                            Speed = 1.76d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 8d,
                            Today = 0d
                          },
                          Rain = new RainInformation
                          {
                            VolumeLast3Hours = null,
                            VolumeLastHour = 0.11d
                          },
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(25.26, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(25.2, TemperatureUnit.Celsius),
                            Humidity = new Humidity(52),
                            MaximumTemperature = new Temperature(25.26, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(25.26, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 286d,
                            Speed = 2.12d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 24d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.27, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(26.27, TemperatureUnit.Celsius),
                            Humidity = new Humidity(46),
                            MaximumTemperature = new Temperature(26.27, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(26.27, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 294d,
                            Speed = 2.4d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 37d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.88, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(26.92, TemperatureUnit.Celsius),
                            Humidity = new Humidity(43),
                            MaximumTemperature = new Temperature(26.88, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(26.88, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 302d,
                            Speed = 2.81d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 100d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(27.28, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(27.07, TemperatureUnit.Celsius),
                            Humidity = new Humidity(40),
                            MaximumTemperature = new Temperature(27.28, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(27.28, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 313d,
                            Speed = 3.25d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 97d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.53, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(26.53, TemperatureUnit.Celsius),
                            Humidity = new Humidity(44),
                            MaximumTemperature = new Temperature(26.53, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(26.53, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 328d,
                            Speed = 3.58d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 87d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(24.58, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(24.53, TemperatureUnit.Celsius),
                            Humidity = new Humidity(55),
                            MaximumTemperature = new Temperature(24.58, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(24.58, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 338d,
                            Speed = 1.98d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 74d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(25.67, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(25.49, TemperatureUnit.Celsius),
                            Humidity = new Humidity(46),
                            MaximumTemperature = new Temperature(25.67, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(25.67, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 340d,
                            Speed = 2.2d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 65d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(24.81, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(24.76, TemperatureUnit.Celsius),
                            Humidity = new Humidity(54),
                            MaximumTemperature = new Temperature(24.81, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(24.81, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 338d,
                            Speed = 1.84d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 62d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(22.4, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(22.5, TemperatureUnit.Celsius),
                            Humidity = new Humidity(69),
                            MaximumTemperature = new Temperature(22.4, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(22.4, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 335d,
                            Speed = 1.29d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 97d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(20.11, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(20.06, TemperatureUnit.Celsius),
                            Humidity = new Humidity(72),
                            MaximumTemperature = new Temperature(20.11, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(20.11, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1022),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1022)
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
                          Wind = new WindInformation
                          {
                            Direction = 327d,
                            Speed = 0.39d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 94d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(18.38, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(18.23, TemperatureUnit.Celsius),
                            Humidity = new Humidity(75),
                            MaximumTemperature = new Temperature(18.38, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(18.38, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 187d,
                            Speed = 0.22d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 96d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(17.66, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(17.49, TemperatureUnit.Celsius),
                            Humidity = new Humidity(77),
                            MaximumTemperature = new Temperature(17.66, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(17.66, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 179d,
                            Speed = 1.25d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 97d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(17.29, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(17.11, TemperatureUnit.Celsius),
                            Humidity = new Humidity(78),
                            MaximumTemperature = new Temperature(17.29, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(17.29, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1024),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1024)
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
                          Wind = new WindInformation
                          {
                            Direction = 192d,
                            Speed = 0.67d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-16T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 90d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.19, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(16.01, TemperatureUnit.Celsius),
                            Humidity = new Humidity(82),
                            MaximumTemperature = new Temperature(16.19, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(16.19, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 179d,
                            Speed = 0.9d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 79d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(15.49, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(15.31, TemperatureUnit.Celsius),
                            Humidity = new Humidity(85),
                            MaximumTemperature = new Temperature(15.49, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(15.49, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 168d,
                            Speed = 1.22d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 9d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.93, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(14.75, TemperatureUnit.Celsius),
                            Humidity = new Humidity(87),
                            MaximumTemperature = new Temperature(14.93, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14.93, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 171d,
                            Speed = 1.05d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 9d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.53, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(14.34, TemperatureUnit.Celsius),
                            Humidity = new Humidity(88),
                            MaximumTemperature = new Temperature(14.53, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14.53, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 180d,
                            Speed = 1.04d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 37d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.59, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(14.38, TemperatureUnit.Celsius),
                            Humidity = new Humidity(87),
                            MaximumTemperature = new Temperature(14.59, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14.59, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 173d,
                            Speed = 1.53d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 53d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(14.85, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(14.61, TemperatureUnit.Celsius),
                            Humidity = new Humidity(85),
                            MaximumTemperature = new Temperature(14.85, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(14.85, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1024),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1024)
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
                          Wind = new WindInformation
                          {
                            Direction = 179d,
                            Speed = 1.22d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 62d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.47, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(16.31, TemperatureUnit.Celsius),
                            Humidity = new Humidity(82),
                            MaximumTemperature = new Temperature(16.47, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(16.47, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1024),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1024)
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
                          Wind = new WindInformation
                          {
                            Direction = 163d,
                            Speed = 0.95d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 64d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(19.06, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(18.9, TemperatureUnit.Celsius),
                            Humidity = new Humidity(72),
                            MaximumTemperature = new Temperature(19.06, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(19.06, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1024),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1024)
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
                          Wind = new WindInformation
                          {
                            Direction = 91d,
                            Speed = 0.41d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 62d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(21.38, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(21.24, TemperatureUnit.Celsius),
                            Humidity = new Humidity(64),
                            MaximumTemperature = new Temperature(21.38, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(21.38, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 72d,
                            Speed = 0.79d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 53d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.26, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(23.13, TemperatureUnit.Celsius),
                            Humidity = new Humidity(57),
                            MaximumTemperature = new Temperature(23.26, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(23.26, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 39d,
                            Speed = 1.45d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 68d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(24.75, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(24.64, TemperatureUnit.Celsius),
                            Humidity = new Humidity(52),
                            MaximumTemperature = new Temperature(24.75, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(24.75, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 41d,
                            Speed = 1.29d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 72d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.5, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(26.5, TemperatureUnit.Celsius),
                            Humidity = new Humidity(45),
                            MaximumTemperature = new Temperature(26.5, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(26.5, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1022),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1022)
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
                          Wind = new WindInformation
                          {
                            Direction = 27d,
                            Speed = 1.53d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 74d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(27.47, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(27.17, TemperatureUnit.Celsius),
                            Humidity = new Humidity(39),
                            MaximumTemperature = new Temperature(27.47, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(27.47, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1022),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1022)
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
                          Wind = new WindInformation
                          {
                            Direction = 11d,
                            Speed = 2.24d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 71d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.03, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(27.5, TemperatureUnit.Celsius),
                            Humidity = new Humidity(37),
                            MaximumTemperature = new Temperature(28.03, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(28.03, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1022),
                            GroundLevel = new Pressure(954),
                            SeaLevel = new Pressure(1022)
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
                          Wind = new WindInformation
                          {
                            Direction = 8d,
                            Speed = 2.43d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 42d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.48, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(27.81, TemperatureUnit.Celsius),
                            Humidity = new Humidity(36),
                            MaximumTemperature = new Temperature(28.48, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(28.48, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1022),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1022)
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
                          Wind = new WindInformation
                          {
                            Direction = 20d,
                            Speed = 1.94d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 54d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.58, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(27.9, TemperatureUnit.Celsius),
                            Humidity = new Humidity(36),
                            MaximumTemperature = new Temperature(28.58, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(28.58, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 24d,
                            Speed = 1.83d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 55d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.3, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(27.72, TemperatureUnit.Celsius),
                            Humidity = new Humidity(37),
                            MaximumTemperature = new Temperature(28.3, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(28.3, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 28d,
                            Speed = 1.81d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 66d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(27.49, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(27.36, TemperatureUnit.Celsius),
                            Humidity = new Humidity(42),
                            MaximumTemperature = new Temperature(27.49, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(27.49, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 29d,
                            Speed = 1.79d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 59d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(26.42, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(26.42, TemperatureUnit.Celsius),
                            Humidity = new Humidity(55),
                            MaximumTemperature = new Temperature(26.42, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(26.42, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 41d,
                            Speed = 1.42d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 61d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.73, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(23.91, TemperatureUnit.Celsius),
                            Humidity = new Humidity(67),
                            MaximumTemperature = new Temperature(23.73, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(23.73, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1022),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1022)
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
                          Wind = new WindInformation
                          {
                            Direction = 96d,
                            Speed = 0.77d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 86d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(20.64, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(20.64, TemperatureUnit.Celsius),
                            Humidity = new Humidity(72),
                            MaximumTemperature = new Temperature(20.64, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(20.64, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1022),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1022)
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
                          Wind = new WindInformation
                          {
                            Direction = 154d,
                            Speed = 0.93d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 89d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(19.27, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(19.13, TemperatureUnit.Celsius),
                            Humidity = new Humidity(72),
                            MaximumTemperature = new Temperature(19.27, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(19.27, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 164d,
                            Speed = 1.37d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 93d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(18.61, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(18.43, TemperatureUnit.Celsius),
                            Humidity = new Humidity(73),
                            MaximumTemperature = new Temperature(18.61, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(18.61, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 171d,
                            Speed = 1.6d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T22:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 95d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(18.04, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(17.83, TemperatureUnit.Celsius),
                            Humidity = new Humidity(74),
                            MaximumTemperature = new Temperature(18.04, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(18.04, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(953),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 173d,
                            Speed = 1.63d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-17T23:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 95d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(17.56, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(17.36, TemperatureUnit.Celsius),
                            Humidity = new Humidity(76),
                            MaximumTemperature = new Temperature(17.56, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(17.56, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 164d,
                            Speed = 1.62d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T00:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 93d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(17.25, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(17.01, TemperatureUnit.Celsius),
                            Humidity = new Humidity(76),
                            MaximumTemperature = new Temperature(17.25, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(17.25, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 162d,
                            Speed = 1.59d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T01:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 61d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.84, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(16.56, TemperatureUnit.Celsius),
                            Humidity = new Humidity(76),
                            MaximumTemperature = new Temperature(16.84, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(16.84, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(952),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 160d,
                            Speed = 1.52d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T02:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 48d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.5, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(16.19, TemperatureUnit.Celsius),
                            Humidity = new Humidity(76),
                            MaximumTemperature = new Temperature(16.5, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(16.5, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1023),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1023)
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
                          Wind = new WindInformation
                          {
                            Direction = 161d,
                            Speed = 1.53d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T03:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 35d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.34, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(15.99, TemperatureUnit.Celsius),
                            Humidity = new Humidity(75),
                            MaximumTemperature = new Temperature(16.34, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(16.34, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1022),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1022)
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
                          Wind = new WindInformation
                          {
                            Direction = 157d,
                            Speed = 1.74d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T04:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 31d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(16.07, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(15.69, TemperatureUnit.Celsius),
                            Humidity = new Humidity(75),
                            MaximumTemperature = new Temperature(16.07, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(16.07, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1022),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1022)
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
                          Wind = new WindInformation
                          {
                            Direction = 151d,
                            Speed = 1.24d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T05:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 27d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(17.69, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(17.45, TemperatureUnit.Celsius),
                            Humidity = new Humidity(74),
                            MaximumTemperature = new Temperature(17.69, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(17.69, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1022),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1022)
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
                          Wind = new WindInformation
                          {
                            Direction = 143d,
                            Speed = 1.61d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T06:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 23d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(20.74, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(20.57, TemperatureUnit.Celsius),
                            Humidity = new Humidity(65),
                            MaximumTemperature = new Temperature(20.74, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(20.74, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 108d,
                            Speed = 0.95d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T07:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 1d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.33, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(23.15, TemperatureUnit.Celsius),
                            Humidity = new Humidity(55),
                            MaximumTemperature = new Temperature(23.33, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(23.33, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1021),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1021)
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
                          Wind = new WindInformation
                          {
                            Direction = 74d,
                            Speed = 1.09d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T08:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 1d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(25.48, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(25.34, TemperatureUnit.Celsius),
                            Humidity = new Humidity(48),
                            MaximumTemperature = new Temperature(25.48, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(25.48, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 51d,
                            Speed = 1.48d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T09:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 1d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(27.2, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(27.18, TemperatureUnit.Celsius),
                            Humidity = new Humidity(43),
                            MaximumTemperature = new Temperature(27.2, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(27.2, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1020),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1020)
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
                          Wind = new WindInformation
                          {
                            Direction = 53d,
                            Speed = 2.06d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T10:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 2d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.67, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(28.12, TemperatureUnit.Celsius),
                            Humidity = new Humidity(38),
                            MaximumTemperature = new Temperature(28.67, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(28.67, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1019),
                            GroundLevel = new Pressure(951),
                            SeaLevel = new Pressure(1019)
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
                          Wind = new WindInformation
                          {
                            Direction = 60d,
                            Speed = 2.6d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T11:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 2d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(29.91, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(29.08, TemperatureUnit.Celsius),
                            Humidity = new Humidity(35),
                            MaximumTemperature = new Temperature(29.91, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(29.91, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1018),
                            GroundLevel = new Pressure(950),
                            SeaLevel = new Pressure(1018)
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
                          Wind = new WindInformation
                          {
                            Direction = 59d,
                            Speed = 2.85d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T12:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 1d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(30.82, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(29.84, TemperatureUnit.Celsius),
                            Humidity = new Humidity(33),
                            MaximumTemperature = new Temperature(30.82, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(30.82, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1017),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1017)
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
                          Wind = new WindInformation
                          {
                            Direction = 56d,
                            Speed = 3.13d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T13:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 0d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(31.56, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(30.55, TemperatureUnit.Celsius),
                            Humidity = new Humidity(32),
                            MaximumTemperature = new Temperature(31.56, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(31.56, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1016),
                            GroundLevel = new Pressure(949),
                            SeaLevel = new Pressure(1016)
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
                          Wind = new WindInformation
                          {
                            Direction = 54d,
                            Speed = 3.25d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T14:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 0d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(31.87, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(30.9, TemperatureUnit.Celsius),
                            Humidity = new Humidity(32),
                            MaximumTemperature = new Temperature(31.87, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(31.87, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1015),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1015)
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
                          Wind = new WindInformation
                          {
                            Direction = 55d,
                            Speed = 3.29d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T15:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 0d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(31.58, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(30.69, TemperatureUnit.Celsius),
                            Humidity = new Humidity(33),
                            MaximumTemperature = new Temperature(31.58, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(31.58, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1015),
                            GroundLevel = new Pressure(948),
                            SeaLevel = new Pressure(1015)
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
                          Wind = new WindInformation
                          {
                            Direction = 51d,
                            Speed = 3.19d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T16:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 0d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(30.66, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(30.35, TemperatureUnit.Celsius),
                            Humidity = new Humidity(39),
                            MaximumTemperature = new Temperature(30.66, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(30.66, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1014),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1014)
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
                          Wind = new WindInformation
                          {
                            Direction = 46d,
                            Speed = 2.44d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T17:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 0d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(28.71, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(29.64, TemperatureUnit.Celsius),
                            Humidity = new Humidity(53),
                            MaximumTemperature = new Temperature(28.71, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(28.71, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1014),
                            GroundLevel = new Pressure(947),
                            SeaLevel = new Pressure(1014)
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
                          Wind = new WindInformation
                          {
                            Direction = 47d,
                            Speed = 1.49d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T18:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 0d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(25.86, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(26.17, TemperatureUnit.Celsius),
                            Humidity = new Humidity(64),
                            MaximumTemperature = new Temperature(25.86, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(25.86, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1014),
                            GroundLevel = new Pressure(946),
                            SeaLevel = new Pressure(1014)
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
                          Wind = new WindInformation
                          {
                            Direction = 59d,
                            Speed = 0.32d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T19:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 0d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(23.01, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(23.17, TemperatureUnit.Celsius),
                            Humidity = new Humidity(69),
                            MaximumTemperature = new Temperature(23.01, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(23.01, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1015),
                            GroundLevel = new Pressure(946),
                            SeaLevel = new Pressure(1015)
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
                          Wind = new WindInformation
                          {
                            Direction = 195d,
                            Speed = 1.38d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T20:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 0d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(21.97, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(22.02, TemperatureUnit.Celsius),
                            Humidity = new Humidity(69),
                            MaximumTemperature = new Temperature(21.97, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(21.97, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1015),
                            GroundLevel = new Pressure(946),
                            SeaLevel = new Pressure(1015)
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
                          Wind = new WindInformation
                          {
                            Direction = 193d,
                            Speed = 1.9d
                          }
                        },
                        new WeatherForecastItem
                        {
                          DateTime = DateTime.ParseExact("2022-06-18T21:00:00.0000000Z", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                          Clouds = new CloudsInformation
                          {
                            All = 0d,
                            Today = 0d
                          },
                          Rain = null,
                          Main = new TemperatureInfo
                          {
                            Temperature = new Temperature(21.31, TemperatureUnit.Celsius),
                            FeelsLike = new Temperature(21.3, TemperatureUnit.Celsius),
                            Humidity = new Humidity(69),
                            MaximumTemperature = new Temperature(21.31, TemperatureUnit.Celsius),
                            MinimumTemperature = new Temperature(21.31, TemperatureUnit.Celsius),
                            Pressure = new Pressure(1015),
                            GroundLevel = new Pressure(945),
                            SeaLevel = new Pressure(1015)
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
                          Wind = new WindInformation
                          {
                            Direction = 184d,
                            Speed = 2.14d
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

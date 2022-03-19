﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class DailyWeatherForecast
    {
        public DailyWeatherForecast()
        {
            this.Weather = new List<WeatherCondition>();
        }

        [JsonProperty("dt")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime DateTime { get; set; }

        [JsonProperty("sunrise")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime Sunrise { get; set; }

        [JsonProperty("sunset")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime Sunset { get; set; }

        [JsonProperty("moonrise")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime Moonrise { get; set; }

        [JsonProperty("moonset")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime Moonset { get; set; }

        [JsonProperty("moon_phase")]
        public double MoonPhase { get; set; }

        [JsonProperty("temp")]
        public DailyTemperatureForecast Temperature { get; set; }

        [JsonProperty("feels_like")]
        public FeelsLikeForecast FeelsLike { get; set; }

        /// <summary>
        ///  Atmospheric pressure on the sea level, hPa.
        /// </summary>
        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("dew_point")]
        public double DewPoint { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")]
        public int WindDeg { get; set; }

        [JsonProperty("wind_gust")]
        public double WindGust { get; set; }

        [JsonProperty("weather")]
        public List<WeatherCondition> Weather { get; set; }

        [JsonProperty("clouds")]
        public int Clouds { get; set; }

        /// <summary>
        /// Probability of precipitation.
        /// The values of the parameter vary between 0 and 1, where 0 is equal to 0%, 1 is equal to 100%.
        [JsonProperty("pop")]
        public double Pop { get; set; }

        /// <summary>
        /// Daily amount of rain, precipitation volume, mm.
        /// </summary>
        [JsonProperty("rain")]
        public double Rain { get; set; }
        
        /// <summary>
        /// Daily amount of snow, precipitation volume, mm.
        /// </summary>
        [JsonProperty("snow")]
        public double Snow { get; set; }
        
        [JsonProperty("uvi")]
        public double Uvi { get; set; }

        public override string ToString()
        {
            return $"DateTime: {this.DateTime}, Temperature: {this.Temperature.Min}/{this.Temperature.Max}";
        }
    }
}
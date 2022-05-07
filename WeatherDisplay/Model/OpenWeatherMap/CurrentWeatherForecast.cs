using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class CurrentWeatherForecast
    {
        public CurrentWeatherForecast()
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

        [JsonProperty("temp")]
        public Temperature Temperature { get; set; }

        [JsonProperty("feels_like")]
        public Temperature FeelsLike { get; set; }

        /// <summary>
        ///  Atmospheric pressure on the sea level, hPa.
        /// </summary>
        [JsonProperty("pressure")]
        [JsonConverter(typeof(PressureJsonConverter))]
        public Pressure Pressure { get; set; }

        [JsonProperty("humidity")]
        [JsonConverter(typeof(HumidityJsonConverter))]
        public Humidity Humidity { get; set; }

        [JsonProperty("dew_point")]
        public Temperature DewPoint { get; set; }

        [JsonProperty("uvi")]
        [JsonConverter(typeof(UVIndexJsonConverter))]
        public UVIndex UVIndex { get; set; }

        /// <summary>
        ///  Cloudiness, %.
        /// </summary>
        [JsonProperty("clouds")]
        public int Clouds { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        /// <summary>
        ///  Wind direction, degrees (meteorological).
        /// </summary>
        [JsonProperty("wind_deg")]
        public int WindDirectionDegrees { get; set; }

        /// <summary>
        ///  Cardinal wind direction.
        /// </summary>
        [JsonIgnore]
        public CardinalWindDirection WindDirection => WindHelper.GetCardinalWindDirection(this.WindDirectionDegrees);

        /// <summary>
        /// Wind gust is a brief increase in the speed of the wind, usually less than 20 seconds. (German: Windböe).
        /// </summary>
        [JsonProperty("wind_gust")]
        public double WindGust { get; set; }

        [JsonProperty("weather")]
        public List<WeatherCondition> Weather { get; set; }

        public override string ToString()
        {
            return $"DateTime: {this.DateTime}, Temperature: {this.Temperature}";
        }
    }
}
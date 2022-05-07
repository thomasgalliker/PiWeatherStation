using System;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class MinutelyWeatherForecast
    {
        /// <summary>
        /// Time of the forecasted data.
        /// </summary>
        [JsonProperty("dt")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Precipitation volume, mm.
        /// </summary>
        [JsonProperty("precipitation ")]
        public double Precipitation  { get; set; }

        public override string ToString()
        {
            return $"DateTime: {this.DateTime}, Precipitation: {this.Precipitation}";
        }
    }
}
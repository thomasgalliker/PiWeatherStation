using System;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class AirPollutionInfoItem
    {
        [JsonProperty("dt")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime DateTime { get; set; }

        [JsonProperty("main")]
        public AirQualitySummary Main { get; set; }

        [JsonProperty("components")]
        public AirConcentrationComponents Components { get; set; }
    }
}
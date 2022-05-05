using System;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class PollutionInfoItem
    {
        [JsonProperty("dt")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime DateTime { get; set; }

        [JsonProperty("main")]
        public PollutionInfoSummary Main { get; set; }

        [JsonProperty("components")]
        public ConcentrationComponents ConcentrationComponents { get; set; }
    }
}
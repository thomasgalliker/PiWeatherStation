using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class AlertInfo
    {
        public AlertInfo()
        {
            this.Tags = new List<string>();
        }

        [JsonProperty("sender_name")]
        public string SenderName { get; set; }

        [JsonProperty("event")]
        public string EventName { get; set; }

        [JsonProperty("start")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime StartTime { get; set; }

        [JsonProperty("end")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime EndTime { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tags")]
        public IReadOnlyCollection<string> Tags { get; set; }
    }
}

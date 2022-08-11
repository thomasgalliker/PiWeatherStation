using System;
using Newtonsoft.Json;
using WeatherDisplay.Model.Wiewarm.Converters;

namespace WeatherDisplay.Model.Wiewarm
{
    public class Weather
    {
        [JsonProperty("wetter_symbol")]
        public int Symbol { get; set; }

        [JsonProperty("wetter_temp")]
        public string Temp { get; set; }

        [JsonProperty("wetter_date")]
        [JsonConverter(typeof(WiewarmDateTimeJsonConverter))]
        public DateTime Date { get; set; }
    }
}
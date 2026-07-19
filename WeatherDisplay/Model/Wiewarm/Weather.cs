using System.Text.Json.Serialization;
using UnitsNet;
using WeatherDisplay.Model.Wiewarm.Converters;

namespace WeatherDisplay.Model.Wiewarm
{
    public class Weather
    {
        [JsonPropertyName("wetter_symbol")]
        public int Symbol { get; set; }

        [JsonPropertyName("wetter_temp")]
        [JsonConverter(typeof(TemperatureJsonConverter))]
        public Temperature Temperature { get; set; }

        [JsonPropertyName("wetter_date")]
        [JsonConverter(typeof(WiewarmDateTimeJsonConverter))]
        public DateTime Date { get; set; }
    }
}

using System;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class AdditionalWeatherInfo
    {
        [JsonProperty("country")]
        public string CountryCode { get; set; }

        [JsonProperty("sunrise")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime Sunrise { get; set; }

        [JsonProperty("sunset")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime Sunset { get; set; }

        public override string ToString()
        {
            return $"Sunrise: {this.Sunrise}, Sunset: {this.Sunset}";
        }
    }
}
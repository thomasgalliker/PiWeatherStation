using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class AirQualitySummary
    {
        /// <summary>
        /// Air quality index.
        /// </summary>
        [JsonProperty("aqi")]
        [JsonConverter(typeof(AirQualityJsonConverter))]
        public AirQuality AirQuality { get; set; }
    }
}
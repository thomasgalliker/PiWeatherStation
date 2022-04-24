using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class WeatherInfo
    {
        public WeatherInfo()
        {
            this.Weather = new List<WeatherCondition>();
        }

        [JsonProperty("dt")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonProperty("weather")]
        public List<WeatherCondition> Weather { get; set; }

        [JsonProperty("main")]
        public TemperatureInfo Main { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("clouds")]
        public CloudsInformation Clouds { get; set; }

        [JsonProperty("sys")]
        public AdditionalWeatherInfo AdditionalInformation { get; set; }

        /// <summary>
        /// Shift in seconds from UTC.
        /// </summary>
        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        /// <summary>
        /// City ID.
        /// </summary>
        [JsonProperty("id")]
        public string CityId { get; set; }

        /// <summary>
        /// City name.
        /// </summary>
        [JsonProperty("name")]
        public string CityName { get; set; }

        /// <summary>
        /// City geo location.
        /// </summary>
        [JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }
    }
}
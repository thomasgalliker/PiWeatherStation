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
            this.WeatherConditions = new List<WeatherCondition>();
        }

        [JsonProperty("weather")]
        public List<WeatherCondition> WeatherConditions { get; set; }

        [JsonProperty("main")]
        public TemperatureInfo Main { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("clouds")]
        public CloudsInformation Clouds { get; internal set; }

        [JsonProperty("dt")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Dt { get; set; }

        [JsonProperty("sys")]
        public Sys Sys { get; set; }

        [JsonProperty("dt_txt")]
        public string Time { get; set; }
    }
}
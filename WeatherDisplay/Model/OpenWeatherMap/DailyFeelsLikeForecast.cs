using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class DailyFeelsLikeForecast
    {
        [JsonProperty("day")]
        public Temperature Day { get; set; }

        [JsonProperty("night")]
        public Temperature Night { get; set; }

        [JsonProperty("eve")]
        public Temperature Evening { get; set; }

        [JsonProperty("morn")]
        public Temperature Morning { get; set; }

        public override string ToString()
        {
            return $"Morning: {this.Morning}, Day: {this.Day}, Evening: {this.Evening}, Night: {this.Night}";
        }
    }
}
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
        public Temperature Eve { get; set; }

        [JsonProperty("morn")]
        public Temperature Morn { get; set; }
    }


}
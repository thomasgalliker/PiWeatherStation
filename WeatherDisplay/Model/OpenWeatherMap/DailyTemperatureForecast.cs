using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class DailyTemperatureForecast
    {
        [JsonProperty("day")]
        public Temperature Day { get; set; }

        [JsonProperty("min")]
        public Temperature Min { get; set; }

        [JsonProperty("max")]
        public Temperature Max { get; set; }

        [JsonProperty("night")]
        public Temperature Night { get; set; }

        [JsonProperty("eve")]
        public Temperature Eve { get; set; }

        [JsonProperty("morn")]
        public Temperature Morn { get; set; }
    }


}
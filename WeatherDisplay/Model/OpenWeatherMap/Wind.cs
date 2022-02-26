using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class Wind
    {
        [JsonProperty("speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("gust")]
        public double Gust { get; set; }
    }
}
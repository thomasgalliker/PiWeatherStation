using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class Coordinates
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class Coordinate
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}
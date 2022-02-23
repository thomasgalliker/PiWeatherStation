using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap;

namespace WeatherDisplay.Services
{
    public class OpenWeatherMapResponse
    {
        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        public Weather[] weather { get; set; }

        public string _base { get; set; }

        public Main main { get; set; }

        public Wind wind { get; set; }

        public Rain rain { get; set; }

        public Clouds clouds { get; set; }

        public int dt { get; set; }

        public Sys sys { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        [JsonProperty("cod")]
        public int StatusCode { get; set; }
    }


    public class Main
    {
        public float temp { get; set; }

        public float pressure { get; set; }

        public float humidity { get; set; }

        public float temp_min { get; set; }

        public float temp_max { get; set; }
    }
}
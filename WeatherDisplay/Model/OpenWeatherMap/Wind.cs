using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class Wind
    {
        [JsonProperty("speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("deg")]
        public int WindDirectionDegrees { get; set; }

        [JsonIgnore]
        public CardinalWindDirection WindDirection => WindHelper.GetCardinalWindDirection(this.WindDirectionDegrees);

        [JsonProperty("gust")]
        public double Gust { get; set; }
    }
}
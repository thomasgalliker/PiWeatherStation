using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public int DirectionDegrees { get; set; }

        [JsonIgnore]
        public CardinalWindDirection Direction => WindHelper.GetCardinalWindDirection(this.DirectionDegrees);

        [JsonProperty("gust")]
        public double Gust { get; set; }
    }
}
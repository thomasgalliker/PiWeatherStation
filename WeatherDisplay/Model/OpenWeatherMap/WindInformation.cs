using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class WindInformation
    {
        /// <summary>
        ///     Gets the angle of the wind direction.
        /// </summary>
        /// <value>the angle of the wind direction.</value>
        [JsonProperty("deg")]
        public double? Direction { get; set; }

        /// <summary>
        ///     Gets the wind speed.
        /// </summary>
        /// <value>the wind speed.</value>
        [JsonProperty("speed")]
        public double? Speed { get; set; }

        public override string ToString()
        {
            return $"Speed: {this.Speed}, Direction: {this.Direction}";
        }
    }
}

using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class RainInformation
    {
        /// <summary>
        ///     Gets the rain volume the last 3 hours in mm.
        /// </summary>
        /// <value>the rain volume the last 3 hours in mm.</value>
        [JsonProperty("3h")]
        public double? VolumeLast3Hours { get; set; }

        /// <summary>
        ///     Gets the rain volume the last hour in mm.
        /// </summary>
        /// <value>the rain volume the last hour in mm.</value>
        [JsonProperty("1h")]
        public double? VolumeLastHour { get; set; }

        public override string ToString()
        {
            return $"1h: {this.VolumeLastHour}, 3h: {this.VolumeLast3Hours}";
        }
    }
}

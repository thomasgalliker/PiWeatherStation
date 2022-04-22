using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class RainInformation
    {
        /// <summary>
        /// Rain volume for last three hour, mm.
        /// </summary>
        [JsonProperty("3h")]
        public double? VolumeLast3Hours { get; set; }

        /// <summary>
        /// Rain volume for last hour, mm.
        /// </summary>
        [JsonProperty("1h")]
        public double? VolumeLastHour { get; set; }

        public override string ToString()
        {
            return $"1h: {this.VolumeLastHour}, 3h: {this.VolumeLast3Hours}";
        }
    }
}

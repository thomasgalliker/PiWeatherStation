using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class TemperatureInfo
    {
        /// <summary>
        ///     Gets the human perception of weather.
        /// </summary>
        /// <value>the human perception of weather.</value>
        [JsonRequired, JsonProperty("feels_like")]
        public Temperature FeelsLike { get; set; }

        /// <summary>
        ///     Gets the atmospheric pressure on the ground level (in hPa).
        /// </summary>
        /// <value>the atmospheric pressure on the ground level (in hPa).</value>
        [JsonProperty("grnd_level")]
        public double? GroundLevel { get; set; }

        /// <summary>
        ///     Gets the air humidity.
        /// </summary>
        /// <value>the air humidity.</value>
        [JsonRequired, JsonProperty("humidity")]
        [JsonConverter(typeof(HumidityJsonConverter))]
        public Humidity Humidity { get; set; }

        /// <summary>
        ///     Gets the maximum temperature.
        /// </summary>
        /// <value>the maximum temperature.</value>
        [JsonProperty("temp_max")]
        public Temperature MaximumTemperature { get; set; }

        /// <summary>
        ///     Gets the minimum temperature.
        /// </summary>
        /// <value>the minimum temperature.</value>
        [JsonProperty("temp_min")]
        public Temperature MinimumTemperature { get; set; }

        /// <summary>
        ///     Gets the air pressure.
        /// </summary>
        /// <value>the air pressure.</value>
        [JsonRequired, JsonProperty("pressure")]
        public double Pressure { get; set; }

        /// <summary>
        ///     Gets the atmospheric pressure on the sea level (in hPa).
        /// </summary>
        /// <value>the atmospheric pressure on the sea level (in hPa).</value>
        [JsonProperty("sea_level")]
        public double? SeaLevel { get; set; }

        [JsonRequired, JsonProperty("temp")]
        public Temperature Temperature { get; set; }

        /// <summary>
        ///     Builds a <see cref="string"/> representation of the object.
        /// </summary>
        /// <returns>the <see cref="string"/> representation</returns>
        public override string ToString() => $"temp: {this.Temperature}, realFeel: {this.FeelsLike}, min: {this.MinimumTemperature}, max: {this.MaximumTemperature}," +
            $" hum: {this.Humidity}, press: {this.Pressure}, hPsea: {this.SeaLevel}, hPgrnd: {this.GroundLevel}";
    }
}
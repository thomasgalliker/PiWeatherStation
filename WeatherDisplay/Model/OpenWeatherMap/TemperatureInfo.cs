using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class TemperatureInfo
    {
        [JsonRequired, JsonProperty("temp")]
        public Temperature Temperature { get; set; }

        /// <summary>
        ///     Gets the human perception of weather.
        /// </summary>
        /// <value>the human perception of weather.</value>
        [JsonRequired, JsonProperty("feels_like")]
        public Temperature FeelsLike { get; set; }

        /// <summary>
        ///     Gets the air humidity.
        /// </summary>
        [JsonRequired, JsonProperty("humidity")]
        [JsonConverter(typeof(HumidityJsonConverter))]
        public Humidity Humidity { get; set; }

        /// <summary>
        ///     Gets the maximum temperature.
        /// </summary>
        [JsonProperty("temp_max")]
        public Temperature MaximumTemperature { get; set; }

        /// <summary>
        ///     Gets the minimum temperature.
        /// </summary>
        [JsonProperty("temp_min")]
        public Temperature MinimumTemperature { get; set; }

        /// <summary>
        ///     Gets the atmospheric pressure.
        /// </summary>
        [JsonRequired, JsonProperty("pressure")]
        [JsonConverter(typeof(PressureJsonConverter))]
        public Pressure Pressure { get; set; }

        /// <summary>
        ///     Gets the atmospheric pressure on the ground level (in hPa).
        /// </summary>
        [JsonProperty("grnd_level")]
        public Pressure? GroundLevel { get; set; }

        /// <summary>
        ///     Gets the atmospheric pressure on the sea level (in hPa).
        /// </summary>
        [JsonProperty("sea_level")]
        [JsonConverter(typeof(PressureJsonConverter))]
        public Pressure? SeaLevel { get; set; }

        public override string ToString()
        {
            return $"Temperature: {this.Temperature}, Humidity: {this.Humidity}";
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class WeatherCondition
    {
        /// <summary>
        ///     Gets the identifier of the weather condition.
        /// </summary>
        [JsonRequired, JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        ///     Gets the scalar type of the weather condition.
        /// </summary>
        [JsonRequired, JsonProperty("main")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeatherConditionType Type { get; set; }
        /// <summary>
        ///     Gets the description of the weather (localized).
        /// </summary>
        [JsonRequired, JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Gets the openweathermap icon identifier (e.g. 09d).
        ///     See also: https://openweathermap.org/weather-conditions#How-to-get-icon-URL
        /// </summary>
        [JsonRequired, JsonProperty("icon")]
        public string IconId { get; set; }
    }
}
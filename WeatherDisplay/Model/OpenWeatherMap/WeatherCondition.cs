using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public sealed class WeatherCondition
    {
        /// <summary>
        ///     Gets the description of the weather (localized).
        /// </summary>
        /// <value>the description of the weather (localized).</value>
        [JsonRequired, JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        ///     Gets the weather icon id (e.g. 09d).
        /// </summary>
        /// <value>the weather icon id (e.g. 09d).</value>
        [JsonRequired, JsonProperty("icon")]
        public string IconId { get; internal set; }

        /// <summary>
        ///     Gets the identifier of the weather condition.
        /// </summary>
        /// <value>the identifier of the weather condition.</value>
        [JsonRequired, JsonProperty("id")]
        public int Id { get; internal set; }

        /// <summary>
        ///     Gets the type of the weather condition.
        /// </summary>
        /// <value>the type of the weather condition.</value>
        [JsonRequired, JsonProperty("main")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeatherConditionType Type { get; internal set; }
    }
}
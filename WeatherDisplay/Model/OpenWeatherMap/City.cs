using System;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [DebuggerDisplay("{Name}")]
    public class City
    {
        /// <summary>
        /// City ID.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// City name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// City geo location.
        /// </summary>
        [JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }

        /// <summary>
        /// Country code.
        /// </summary>
        [JsonProperty("country")]
        [JsonConverter(typeof(RegionInfoJsonConverter))]
        public RegionInfo Country { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }

        /// <summary>
        /// Shift in seconds from UTC.
        /// </summary>
        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        [JsonProperty("sunrise")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime Sunrise { get; set; }

        [JsonProperty("sunset")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime Sunset { get; set; }
    }
}

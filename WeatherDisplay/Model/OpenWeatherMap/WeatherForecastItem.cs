using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    [DebuggerDisplay("WeatherForecastItem: {CalculationTime}, {Temperature}")]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class WeatherForecastItem
    {
        public WeatherForecastItem()
        {
            this.WeatherConditions = new List<WeatherCondition>();
        }

        [JsonProperty("dt")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime DateTime { get; set; }

        /// <summary>
        ///     Gets additional clouds information.
        /// </summary>
        /// <value>additional clouds information.</value>
        [JsonProperty("clouds")]
        public CloudsInformation Clouds { get; set; }

        /// <summary>
        ///     Gets additional rain information.
        /// </summary>
        /// <value>additional rain information.</value>
        [JsonProperty("rain")]
        public RainInformation Rain { get; set; }

        /// <summary>
        ///     Gets the temperature information.
        /// </summary>
        /// <value>the temperature information.</value>
        [JsonRequired, JsonProperty("main")]
        public TemperatureInfo Temperature { get; set; }

        /// <summary>
        ///     Gets a read-only list containing information about the weather conditions.
        /// </summary>
        /// <value>a read-only list containing information about the weather conditions.</value>
        [JsonRequired, JsonProperty("weather")]
        public IReadOnlyList<WeatherCondition> WeatherConditions { get; set; }

        /// <summary>
        ///     Gets additional wind information.
        /// </summary>
        /// <value>additional wind information.</value>
        [JsonProperty("wind")]
        public WindInformation Wind { get; set; }
    }
}

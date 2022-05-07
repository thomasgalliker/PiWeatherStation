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

        [JsonProperty("clouds")]
        public CloudsInformation Clouds { get; set; }

        [JsonProperty("rain")]
        public RainInformation Rain { get; set; }

        [JsonRequired, JsonProperty("main")]
        public TemperatureInfo Temperature { get; set; }

        /// <summary>
        ///     Gets a read-only list containing information about the weather conditions.
        /// </summary>
        /// <value>a read-only list containing information about the weather conditions.</value>
        [JsonRequired, JsonProperty("weather")]
        public IReadOnlyList<WeatherCondition> WeatherConditions { get; set; }

        [JsonProperty("wind")]
        public WindInformation Wind { get; set; }
    }
}

using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class ConcentrationComponents
    {
        /// <summary>
        /// Concentration of Carbon Monoxide in the air, in micrograms per cubic meter.
        /// </summary>
        [JsonProperty("co")]
        public double CarbonMonoxide { get; set; }

        /// <summary>
        /// Concentration of Nitrogen Monoxide in the air, in micrograms per cubic meter.
        /// </summary>
        [JsonProperty("no")]
        public double NitrogenMonoxide { get; set; }

        /// <summary>
        /// Concentration of Nitrogen Dioxide in the air, in micrograms per cubic meter.
        /// </summary>
        [JsonProperty("no2")]
        public double NitrogenDioxide { get; set; }

        /// <summary>
        /// Concentration of Ozone in the air, in micrograms per cubic meter.
        /// </summary>
        [JsonProperty("o3")]
        public double Ozone { get; set; }

        /// <summary>
        /// Concentration of Sulphur Dioxide in the air, in micrograms per cubic meter.
        /// </summary>
        [JsonProperty("so2")]
        public double SulphurDioxide { get; set; }

        /// <summary>
        /// Concentration of fine particles matter in the air, in micrograms per cubic meter.
        /// </summary>
        [JsonProperty("pm2_5")]
        public double FineParticulateMatter { get; set; }

        /// <summary>
        /// Concentration of coarse particulate matter in the air, in micrograms per cubic meter.
        /// </summary>
        [JsonProperty("pm10")]
        public double CoarseParticulateMatter { get; set; }

        /// <summary>
        /// Concentration of ammonia in the air, in micrograms per cubic meter.
        /// </summary>
        [JsonProperty("nh3")]
        public double Ammonia { get; set; }
    }
}
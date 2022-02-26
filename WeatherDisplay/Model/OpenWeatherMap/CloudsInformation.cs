using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class CloudsInformation
    {
        /// <summary>
        ///     Gets the cloudiness in percent (%).
        /// </summary>
        /// <value>the cloudiness in percent (%).</value>
        [JsonProperty("all")]
        public double Cloudiness { get; internal set; }

        /// <summary>
        ///     Gets the cloudiness today in percent (%).
        /// </summary>
        /// <value>the cloudiness today in percent (%).</value>
        [JsonProperty("today")]
        public double TodayCloudiness { get; internal set; }
    }

}
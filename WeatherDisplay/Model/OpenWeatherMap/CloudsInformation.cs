using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class CloudsInformation
    {
        /// <summary>
        ///     Gets the cloudiness in percent (%).
        /// </summary>
        [JsonProperty("all")]
        public double Cloudiness { get; set; }

        /// <summary>
        ///     Gets the cloudiness today in percent (%).
        /// </summary>
        [JsonProperty("today")]
        public double TodayCloudiness { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WeatherDisplay.Services;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class OneCallWeatherInfo
    {
        public OneCallWeatherInfo()
        {
            this.HourlyForecasts = new List<HourlyWeatherForecast>();
            this.DailyForecasts = new List<DailyWeatherForecast>();
            this.Alerts = new List<AlertInfo>();
        }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("timezone_offset")]
        public int TimezoneOffset { get; set; }

        //[JsonProperty("current")]
        //public CurrentWeatherInfo CurrentWeather { get; set; }

        /// <summary>
        /// 60-minutes weather forecast.
        /// </summary>
        /// <remarks>
        /// Is only included if <seealso cref="OneCallOptions.IncludeMinutelyForecasts"/> is true.
        [JsonProperty("minutely")]
        public IReadOnlyCollection<MinutelyWeatherForecast> MinutelyForecasts { get; set; }

        /// <summary>
        /// 48-hours weather forecast.
        /// </summary>
        /// <remarks>
        /// Is only included if <seealso cref="OneCallOptions.IncludeHourlyForecasts"/> is true.
        /// </remarks>
        [JsonProperty("hourly")]
        public IReadOnlyCollection<HourlyWeatherForecast> HourlyForecasts { get; set; }

        /// <summary>
        /// 8-days weather forecast.
        /// </summary>
        /// <remarks>
        /// Is only included if <seealso cref="OneCallOptions.IncludeDailyForecasts"/> is true.
        /// </remarks>
        [JsonProperty("daily")]
        public IReadOnlyCollection<DailyWeatherForecast> DailyForecasts { get; set; }

        [JsonProperty("alerts")]
        public IReadOnlyCollection<AlertInfo> Alerts { get; set; }

        public override string ToString()
        {
            var data = new [] 
            {
                (Name: "Minutely", Count: this.MinutelyForecasts.Count),
                (Name: "Hourly", Count: this.HourlyForecasts.Count),
                (Name: "Daily", Count: this.DailyForecasts.Count),
                (Name: "Alerts", Count: this.Alerts.Count) }
            .Where(x => x.Count > 0)
            .Select(x => $"{x.Name}: {x.Count}");

            var toStringValue = string.Join(", ", data);
            return toStringValue;
        }
    }
}
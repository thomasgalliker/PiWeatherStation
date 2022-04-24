using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class OneCallWeatherInfo
    {
        public OneCallWeatherInfo()
        {
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

        //[JsonProperty("minutely")]
        //public MinutelyWeatherInfo MinutelyForecasts { get; set; }

        //[JsonProperty("hourly")]
        //public CurrentWeatherInfo HourlyForecasts { get; set; }

        [JsonProperty("daily")]
        public IReadOnlyCollection<DailyWeatherForecast> DailyForecasts { get; set; }

        [JsonProperty("alerts")]
        public IReadOnlyCollection<AlertInfo> Alerts { get; set; }

        public override string ToString()
        {
            return $"Daily: {this.DailyForecasts.Count}, Alerts: {this.Alerts.Count}";
        }
    }
}
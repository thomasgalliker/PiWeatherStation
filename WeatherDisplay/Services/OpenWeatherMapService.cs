using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Model.OpenWeatherMap.Converters;

namespace WeatherDisplay.Services
{
    /// <summary>
    ///     OpenWeatherMap API Documentation:
    ///     https://openweathermap.org/current
    ///     https://openweathermap.org/weather-conditions
    /// </summary>
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private const string ApiEndpoint = "https://api.openweathermap.org";
        private const string ImageApiEndpoint = "https://openweathermap.org/img/wn";

        private readonly HttpClient httpClient;
        private readonly JsonSerializerSettings serializerSettings;
        private readonly string openWeatherMapApiKey;
        private readonly string unitSystem;

        public OpenWeatherMapService(IOpenWeatherMapConfiguration openWeatherMapConfiguration)
        {
            this.openWeatherMapApiKey = openWeatherMapConfiguration.ApiKey;
            this.unitSystem = openWeatherMapConfiguration.UnitSystem;

            this.httpClient = new HttpClient();
            this.serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            switch (openWeatherMapConfiguration.UnitSystem)
            {
                case "metric":
                    this.serializerSettings.Converters.Add(new CelsiusTemperatureJsonConverter());
                    break;
                case "imperial":
                    this.serializerSettings.Converters.Add(new FahrenheitTemperatureJsonConverter());
                    break;
                default:
                    break;
            }
        }

        public async Task<WeatherInfo> GetCurrentWeatherAsync(double latitude, double longitude)
        {
            var lat = latitude.ToString("0.0000", CultureInfo.InvariantCulture);
            var lon = longitude.ToString("0.0000", CultureInfo.InvariantCulture);

            var builder = new UriBuilder(ApiEndpoint)
            {
                Path = "data/2.5/weather",
                Query = $"lat={lat}&lon={lon}&units={this.unitSystem}&appid={this.openWeatherMapApiKey}"
            };
            var uri = builder.ToString();

            var response = await this.httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(responseJson, this.serializerSettings);

            return weatherInfo;
        }

        public async Task<Stream> GetWeatherIconAsync(WeatherCondition weatherCondition)
        {
            var iconUrl = $"{ImageApiEndpoint}{weatherCondition.IconId}@2x.png";

            var response = await this.httpClient.GetAsync(iconUrl);
            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();
            return responseStream;
        }
    }
}
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

        private readonly HttpClient httpClient;
        private readonly IWeatherIconMapping defaultWeatherIconMapping;
        private readonly JsonSerializerSettings serializerSettings;
        private readonly string openWeatherMapApiKey;
        private readonly string unitSystem;
        private readonly string language;

        public OpenWeatherMapService(IOpenWeatherMapConfiguration openWeatherMapConfiguration)
        {
            this.openWeatherMapApiKey = openWeatherMapConfiguration.ApiKey;
            this.unitSystem = openWeatherMapConfiguration.UnitSystem;
            this.language = openWeatherMapConfiguration.Language;

            this.httpClient = new HttpClient();
            this.defaultWeatherIconMapping = new DefaultWeatherIconMapping(this.httpClient);
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
                Query = $"lat={lat}&lon={lon}&units={this.unitSystem}&lang={this.language}&appid={this.openWeatherMapApiKey}"
            };
            var uri = builder.ToString();

            var response = await this.httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(responseJson, this.serializerSettings);

            return weatherInfo;
        }
        
        public async Task<WeatherForecast> GetWeatherForecast(double latitude, double longitude)
        {
            var lat = latitude.ToString("0.0000", CultureInfo.InvariantCulture);
            var lon = longitude.ToString("0.0000", CultureInfo.InvariantCulture);

            var builder = new UriBuilder(ApiEndpoint)
            {
                Path = "data/2.5/forecast",
                Query = $"lat={lat}&lon={lon}&units={this.unitSystem}&lang={this.language}&appid={this.openWeatherMapApiKey}"
            };
            var uri = builder.ToString();

            var response = await this.httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<WeatherForecast>(responseJson, this.serializerSettings);

            return weatherForecast;
        }


        public async Task<OneCallWeatherInfo> GetWeatherOneCallAsync(double latitude, double longitude)
        {
            var lat = latitude.ToString("0.0000", CultureInfo.InvariantCulture);
            var lon = longitude.ToString("0.0000", CultureInfo.InvariantCulture);

            var builder = new UriBuilder(ApiEndpoint)
            {
                Path = "data/2.5/onecall",
                Query = $"lat={lat}&lon={lon}&exclude=current,minutely,hourly&units={this.unitSystem}&lang={this.language}&appid={this.openWeatherMapApiKey}"
            };
            var uri = builder.ToString();

            var response = await this.httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<OneCallWeatherInfo>(responseJson, this.serializerSettings);

            return weatherForecast;
        }

        public async Task<Stream> GetWeatherIconAsync(WeatherCondition weatherCondition, IWeatherIconMapping weatherIconMapping = null)
        {
            if (weatherIconMapping == null)
            {
                weatherIconMapping = this.defaultWeatherIconMapping;
            }

            var imageStream = await weatherIconMapping.GetIconAsync(weatherCondition);
            return imageStream;
        }
    }
}
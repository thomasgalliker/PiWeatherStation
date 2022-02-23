using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap;

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
        private readonly string openWeatherMapApiKey;
        private readonly string unitSystem;

        public OpenWeatherMapService(IOpenWeatherMapConfiguration openWeatherMapConfiguration)
        {
            this.openWeatherMapApiKey = openWeatherMapConfiguration.ApiKey;
            this.unitSystem = openWeatherMapConfiguration.UnitSystem;

            this.httpClient = new HttpClient();
        }

        public async Task<WeatherResponse> GetWeatherInfoAsync(double latitude, double longitude)
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
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var openWeatherMapResponse = JsonConvert.DeserializeObject<OpenWeatherMapResponse>(responseJson);
                int? conditionId = null;
                string iconUrl = null;
                var primaryWeather = openWeatherMapResponse.weather.FirstOrDefault();
                if (primaryWeather != null)
                {
                    conditionId = primaryWeather.id;
                    iconUrl = $"{ImageApiEndpoint}/{primaryWeather.icon}@2x.png";
                }

                var weatherInfo = new WeatherResponse
                {
                    Temperature = openWeatherMapResponse.main.temp,
                    UnitSystem = this.unitSystem,
                    LocationName = openWeatherMapResponse.name,
                    ConditionId = conditionId,
                    IconUrl = iconUrl
                };

                return weatherInfo;
            }

            throw new Exception("OpenWeatherMap API answered with: " + (response != null ? $"StatusCode={response.StatusCode}." : "Invalid response."));
        }
    }
}
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DisplayService.ConsoleApp.Service
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

        public async Task<WeatherResponse> GetWeatherInfoAsync(double longitude, double latitude)
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

    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit,
    }

    public class WeatherResponse
    {
        public float Temperature { get; set; }

        public string UnitSystem { get; set; }

        public string LocationName { get; set; }

        public int? ConditionId { get; set; }

        public string IconUrl { get; set; }

    }

    public class OpenWeatherMapResponse
    {
        [JsonProperty("coord")]
        public Coord coord { get; set; }

        public Weather[] weather { get; set; }

        public string _base { get; set; }

        public Main main { get; set; }

        public Wind wind { get; set; }

        public Rain rain { get; set; }

        public Clouds clouds { get; set; }

        public int dt { get; set; }

        public Sys sys { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        [JsonProperty("cod")]
        public int StatusCode { get; set; }
    }

    public class Coord
    {
        public float lon { get; set; }

        public float lat { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }

        public float pressure { get; set; }

        public float humidity { get; set; }

        public float temp_min { get; set; }

        public float temp_max { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }

        public float deg { get; set; }
    }

    public class Rain
    {
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }

        public int id { get; set; }

        public float message { get; set; }

        public string country { get; set; }

        public int sunrise { get; set; }

        public int sunset { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }

        public string main { get; set; }

        public string description { get; set; }

        public string icon { get; set; }
    }
}
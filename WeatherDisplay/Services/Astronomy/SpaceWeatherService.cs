using System.Globalization;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenWeatherMap;

namespace WeatherDisplay.Services.Astronomy
{
    public class SpaceWeatherService : ISpaceWeatherService
    {
        private readonly ILogger logger;
        private readonly HttpClient httpClient;
        private readonly JsonSerializerSettings serializerSettings;
        private const string apiEndpoint = "https://services.swpc.noaa.gov";

        public SpaceWeatherService(ILogger<SpaceWeatherService> logger)
            : this(logger, new HttpClient())
        {
        }

        public SpaceWeatherService(ILogger<SpaceWeatherService> logger, HttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            };
            this.serializerSettings.Converters.Add(new PlanetaryKIndexForecastJsonConverter());
        }

        public async Task<PlanetaryKIndexForecast[]> GetPlanetaryKIndexForecastAsync()
        {
            this.logger.LogDebug($"GetPlanetaryKIndexForecastAsync");

            var builder = new UriBuilder(apiEndpoint)
            {
                Path = "products/noaa-planetary-k-index-forecast.json",
            };

            var uri = builder.ToString();
            this.logger.LogDebug($"GetPlanetaryKIndexForecastAsync: GET {uri}");

            var response = await this.httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var planetaryKIndexForecasts = JsonConvert.DeserializeObject<PlanetaryKIndexForecast[]>(responseJson, this.serializerSettings);
            return planetaryKIndexForecasts;
        }
    }
}

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WeatherDisplay.Services.Astronomy
{
    public class SpaceWeatherService : ISpaceWeatherService
    {
        private readonly ILogger logger;
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions serializerOptions;
        private const string ApiEndpoint = "https://services.swpc.noaa.gov";

        public SpaceWeatherService(ILogger<SpaceWeatherService> logger)
            : this(logger, new HttpClient())
        {
        }

        public SpaceWeatherService(ILogger<SpaceWeatherService> logger, HttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.serializerOptions = new JsonSerializerOptions();
            this.serializerOptions.Converters.Add(new PlanetaryKIndexForecastJsonConverter());
        }

        public async Task<PlanetaryKIndexForecast[]> GetPlanetaryKIndexForecastAsync()
        {
            this.logger.LogDebug("GetPlanetaryKIndexForecastAsync");

            var builder = new UriBuilder(ApiEndpoint)
            {
                Path = "products/noaa-planetary-k-index-forecast.json",
            };

            var uri = builder.ToString();
            this.logger.LogDebug($"GetPlanetaryKIndexForecastAsync: GET {uri}");

            var response = await this.httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var planetaryKIndexForecasts = JsonSerializer.Deserialize<PlanetaryKIndexForecast[]>(responseJson, this.serializerOptions);
            return planetaryKIndexForecasts;
        }
    }
}

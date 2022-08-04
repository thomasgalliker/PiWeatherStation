using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherDisplay.Model.OpenWeatherMap.Converters;
using WeatherDisplay.Model.Wiewarm;

namespace WeatherDisplay.Services.Wiewarm
{
    public class WiewarmService : IWiewarmService
    {
        private const string Endpoint = "https://www.wiewarm.ch:443/api/v1";

        private readonly ILogger<WiewarmService> logger;
        private readonly HttpClient httpClient;
        private readonly JsonSerializerSettings serializerSettings;

        public WiewarmService(ILogger<WiewarmService> logger)
         : this(logger, new HttpClient())
        {
        }

        public WiewarmService(ILogger<WiewarmService> logger, HttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            this.serializerSettings.Converters.Add(new CelsiusTemperatureJsonConverter());
        }

        public async Task<Bad> GetBadByIdAsync(int badId)
        {
            var uri = $"https://www.wiewarm.ch:443/api/v1/bad.json/{badId}";
            this.logger.LogDebug($"GetBadByIdAsync: GET {uri}");

            var response = await this.httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var wiewarmBadResponse = JsonConvert.DeserializeObject<Bad>(responseJson, this.serializerSettings);
            return wiewarmBadResponse;
        }

        public async Task<IEnumerable<Bad>> SearchBadAsync(string search)
        {
            var uri = $"{Endpoint}/bad.json?search={search}";
            this.logger.LogDebug($"GetBadByIdAsync: GET {uri}");

            var response = await this.httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var wiewarmBadResponse = JsonConvert.DeserializeObject<IEnumerable<Bad>>(responseJson, this.serializerSettings);
            return wiewarmBadResponse;
        }
    }


}
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeatherDisplay.Model.Wiewarm;

namespace WeatherDisplay.Services.Wiewarm
{
    public class WiewarmService : IWiewarmService
    {
        private const string Endpoint = "https://www.wiewarm.ch:443/api/v1";

        private readonly ILogger<WiewarmService> logger;
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions serializerOptions;

        public WiewarmService(ILogger<WiewarmService> logger)
         : this(logger, new HttpClient())
        {
        }

        public WiewarmService(ILogger<WiewarmService> logger, HttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.serializerOptions = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<Bath> GetBathByIdAsync(int badId)
        {
            var uri = $"{Endpoint}/bad.json/{badId}";
            this.logger.LogDebug($"GetBathByIdAsync: GET {uri}");

            var response = await this.httpClient.GetAsync(uri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var wiewarmBadResponse = JsonSerializer.Deserialize<Bath>(responseJson, this.serializerOptions);
            return wiewarmBadResponse;
        }

        public async Task<IEnumerable<Bath>> SearchBathsAsync(string search)
        {
            var uri = $"{Endpoint}/bad.json?search={search}";
            this.logger.LogDebug($"SearchBathsAsync: GET {uri}");

            var response = await this.httpClient.GetAsync(uri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                this.logger.LogDebug($"SearchBathsAsync: failed for uri {uri}");
                response.EnsureSuccessStatusCode();
            }

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var wiewarmBadResponse = JsonSerializer.Deserialize<IEnumerable<Bath>>(responseJson, this.serializerOptions);
            return wiewarmBadResponse;
        }
    }
}

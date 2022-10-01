using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherDisplay.Model.Wiewarm;
using WeatherDisplay.Model.Wiewarm.Converters;

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

            this.serializerSettings.Converters.Add(new TemperatureJsonConverter());
        }

        public async Task<Bath> GetBathByIdAsync(int badId)
        {
            var uri = $"https://www.wiewarm.ch:443/api/v1/bad.json/{badId}";
            this.logger.LogDebug($"GetBadByIdAsync: GET {uri}");

            var response = await this.httpClient.GetAsync(uri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var wiewarmBadResponse = JsonConvert.DeserializeObject<Bath>(responseJson, this.serializerSettings);
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

            var wiewarmBadResponse = JsonConvert.DeserializeObject<IEnumerable<Bath>>(responseJson, this.serializerSettings);
            return wiewarmBadResponse;
        }
    }
}
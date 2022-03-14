using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherDisplay.Model.OpenWeatherMap;

namespace WeatherDisplay.Services
{
    public class DefaultWeatherIconMapping : IWeatherIconMapping
    {
        private const string ImageApiEndpoint = "https://openweathermap.org/img/wn";

        private readonly HttpClient httpClient;

        public DefaultWeatherIconMapping(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Stream> GetIconAsync(WeatherCondition weatherCondition)
        {
            var iconUrl = $"{ImageApiEndpoint}/{weatherCondition.IconId}@2x.png";

            var response = await this.httpClient.GetAsync(iconUrl);
            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();
            return responseStream;
        }
    }
}
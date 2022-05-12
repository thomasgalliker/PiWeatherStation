using System.Text.Json.Serialization;

namespace WeatherDisplay.Api.Models
{
    public class GithubAssetDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("browser_download_url")]
        public string DownloadUrl { get; set; }
    }
}
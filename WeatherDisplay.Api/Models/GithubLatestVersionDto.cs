using System.Text.Json.Serialization;

namespace WeatherDisplay.Api.Models
{
    public class GithubLatestVersionDto
    {
        public GithubLatestVersionDto()
        {
            this.Assets = Array.Empty<GithubAssetDto>();
        }

        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }

        [JsonPropertyName("assets")]
        public IReadOnlyCollection<GithubAssetDto> Assets { get; set; }
    }
}
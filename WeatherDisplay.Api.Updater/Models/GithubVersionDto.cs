using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherDisplay.Api.Updater.Models
{
    public class GithubVersionDto
    {
        public GithubVersionDto()
        {
            this.Assets = Array.Empty<GithubAssetDto>();
        }

        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }

        [JsonPropertyName("assets")]
        public IReadOnlyCollection<GithubAssetDto> Assets { get; set; }
    }
}
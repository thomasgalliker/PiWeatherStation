using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class GithubVersionChecker : IRemoteVersionChecker
    {
        private readonly ILogger<GithubVersionChecker> logger;
        private readonly GithubVersionCheckerOptions options;
        private readonly HttpClient httpClient;

        public GithubVersionChecker(ILogger<GithubVersionChecker> logger, GithubVersionCheckerOptions options)
            : this(options, new HttpClient())
        {
            this.logger = logger;
        }

        public GithubVersionChecker(GithubVersionCheckerOptions options, HttpClient httpClient)
        {
            this.options = options;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
        }

        public async Task<IUpdateVersionSource> GetLatestVersionAsync()
        {
            var url = this.options.GithubRepositoryUrl;
            var preRelease = this.options.PreRelease;
            if (!preRelease)
            {
                url = $"{url}/latest";
            }

            GithubVersionDto latestVersionDto;

            try
            {
                if (preRelease)
                {
                    var githubVersionDtos = await this.httpClient.GetFromJsonAsync<IEnumerable<GithubVersionDto>>(url);
                    latestVersionDto = githubVersionDtos.FirstOrDefault();
                }
                else
                {
                    latestVersionDto = await this.httpClient.GetFromJsonAsync<GithubVersionDto>($"{url}/latest");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetLatestVersionAsync failed with exception");
                throw new Exception($"Failed to load latest version from \"{url}\":: {ex.Message}", ex);
            }

            if (latestVersionDto == null)
            {
                throw new Exception($"Failed to load latest version from \"{url}\": Make sure a matching version exists.");
            }

            GithubAssetDto asset;

            if (this.options.AssetSelector != null)
            {
                asset = this.options.AssetSelector(latestVersionDto.Assets);
            }
            else
            {
                asset = latestVersionDto.Assets.FirstOrDefault();
            }

            if (asset == null)
            {
                throw new Exception($"Failed to load latest version from \"{url}\": No assets found.");
            }

            return new HttpFileDownloadVersionSource
            {
                Url = asset.DownloadUrl,
                Version = SemanticVersion.Parse(latestVersionDto.TagName),
            };
        }
    }
}

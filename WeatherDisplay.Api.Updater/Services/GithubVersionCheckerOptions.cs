using System;
using System.Collections.Generic;
using System.Linq;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class GithubVersionCheckerOptions
    {
        public bool PreRelease { get; set; }

        public string GithubRepositoryUrl { get; set; }

        public Func<IEnumerable<GithubAssetDto>, GithubAssetDto> AssetSelector { get; set; }
    }
}
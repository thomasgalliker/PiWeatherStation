using System;
using System.Collections.Generic;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class GithubVersionCheckerOptions
    {
        public virtual bool PreRelease { get; set; }

        public virtual string GithubRepositoryUrl { get; set; }

        public virtual Func<IEnumerable<GithubAssetDto>, GithubAssetDto> AssetSelector { get; set; }
    }
}
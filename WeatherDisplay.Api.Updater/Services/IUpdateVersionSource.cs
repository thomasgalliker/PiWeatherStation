using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface IUpdateVersionSource
    {
        public SemanticVersion Version { get; set; }

        DownloadFileStep GetDownloadStep();
    }
}
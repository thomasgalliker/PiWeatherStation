using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface IVersionSource
    {
        public SemanticVersion Version { get; set; }

        DownloadFileStep GetDownloadStep();
    }
}
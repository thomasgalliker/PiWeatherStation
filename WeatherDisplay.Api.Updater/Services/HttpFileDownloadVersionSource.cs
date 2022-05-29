using System.IO;
using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class HttpFileDownloadVersionSource : IVersionSource
    {
        public SemanticVersion Version { get; set; }

        public string Url { get; set; }

        public DownloadFileStep GetDownloadStep()
        {
            return new DownloadHttpFileStep
            {
                Url = this.Url,
                DestinationFileName = Path.GetFileName(this.Url),
            };
        }
    }
}
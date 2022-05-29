using System.IO;
using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class FtpFileDownloadVersionSource : IUpdateVersionSource
    {
        public SemanticVersion Version { get; set; }

        public string Url { get; set; }

        public DownloadFileStep GetDownloadStep()
        {
            return new DownloadFtpFileStep
            {
                Url = this.Url,
                DestinationFileName = Path.GetFileName(this.Url),
            };
        }
    }
}
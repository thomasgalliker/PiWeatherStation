namespace WeatherDisplay.Api.Updater.Models
{
    public class ExtractZipStep : IExecutorStep
    {
        public string SourceArchiveFileName { get; set; }

        public string DestinationDirectoryName { get; set; }

        public bool OverwriteFiles { get; set; }

        public bool DeleteSourceArchive { get; set; }
    }
}

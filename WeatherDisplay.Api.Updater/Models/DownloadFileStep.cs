namespace WeatherDisplay.Api.Updater.Models
{
    public class DownloadFileStep : IExecutorStep
    {
        public string Url { get; set; }

        public string DestinationFileName { get; set; }
    }
}

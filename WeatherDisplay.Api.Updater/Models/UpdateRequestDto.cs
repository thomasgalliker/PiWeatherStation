namespace WeatherDisplay.Api.Updater.Models
{
    internal class UpdateRequestDto
    {
        public string DownloadUrl { get; set; }

        public string WorkingDirectory { get; set; }
    }
}

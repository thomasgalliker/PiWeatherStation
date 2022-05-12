using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class UpdateCheckResult
    {
        public static readonly UpdateCheckResult NoUpdateAvailable = new UpdateCheckResult();

        private UpdateCheckResult()
        {
        }

        public UpdateCheckResult(GithubVersionDto updateVersion)
        {
            this.UpdateVersion = updateVersion;
        }

        public bool HasUpdate => this.UpdateVersion != null;

        public GithubVersionDto UpdateVersion { get; }
    }
}
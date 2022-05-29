using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class UpdateRequest
    {
        public string CurrentDirectory { get; set; }

        public SemanticVersion UpdateVersion { get; set; }

        public IExecutorStep[] ExecutorSteps { get; set; }
    }

    public class UpdateCheckResult
    {
        public static readonly UpdateCheckResult NoUpdateAvailable = new UpdateCheckResult();

        private UpdateCheckResult()
        {
        }

        public UpdateCheckResult(SemanticVersion localVersion)
        {
            this.LocalVersion = localVersion;
        }
        
        public UpdateCheckResult(SemanticVersion localVersion, SemanticVersion updateVersion)
        {
            this.LocalVersion = localVersion;
            this.UpdateVersion = updateVersion;
        }
        
        public UpdateCheckResult(SemanticVersion localVersion, IVersionSource versionSource)
        {
            this.LocalVersion = localVersion;
            this.UpdateVersion = versionSource.Version;
            this.VersionSource = versionSource;
        }

        public bool HasUpdate => this.UpdateVersionSource != null;

        public SemanticVersion LocalVersion { get; }

        public SemanticVersion UpdateVersion { get; }
        public IVersionSource VersionSource { get; }
        public IVersionSource UpdateVersionSource { get; }
    }
}
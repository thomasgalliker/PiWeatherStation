using NuGet.Versioning;

namespace WeatherDisplay.Api.Updater.Services
{
    public class UpdateCheckResult
    {
        public UpdateCheckResult(SemanticVersion localVersion)
        {
            this.LocalVersion = localVersion;
        }
        
        public UpdateCheckResult(SemanticVersion localVersion, SemanticVersion updateVersion)
        {
            this.LocalVersion = localVersion;
            this.UpdateVersion = updateVersion;
        }
        
        public UpdateCheckResult(SemanticVersion localVersion, IUpdateVersionSource updateVersionSource)
        {
            this.LocalVersion = localVersion;
            this.UpdateVersion = updateVersionSource.Version;
            this.UpdateVersionSource = updateVersionSource;
        }

        public bool HasUpdate => this.UpdateVersionSource != null;

        public SemanticVersion LocalVersion { get; }

        public SemanticVersion UpdateVersion { get; }

        public IUpdateVersionSource UpdateVersionSource { get; }
    }
}
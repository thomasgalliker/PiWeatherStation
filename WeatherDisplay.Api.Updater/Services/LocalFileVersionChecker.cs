using System.IO;
using System.Threading.Tasks;
using NuGet.Versioning;

namespace WeatherDisplay.Api.Updater.Services
{
    public class LocalFileVersionChecker : ILocalVersionChecker
    {
        private readonly string installedVersionFile;

        public LocalFileVersionChecker(LocalFileVersionCheckerOptions installFileVersionCheckerOptions)
        {
            this.installedVersionFile = installFileVersionCheckerOptions.InstalledVersionFile;
        }

        public async Task<SemanticVersion> GetLocalVersionAsync()
        {
            var productVersion = "0.0.0";

            if (!File.Exists(this.installedVersionFile))
            {
                // ???
            }
            else
            {
                productVersion = await File.ReadAllTextAsync(this.installedVersionFile);
            }

            return SemanticVersion.Parse(productVersion);
        }
    }
}

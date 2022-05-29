using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using NuGet.Versioning;

namespace WeatherDisplay.Api.Updater.Services
{
    public class ProductVersionChecker : ILocalVersionChecker
    {
        public ProductVersionChecker()
        {
        }

        private static string GetProductVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }

        public Task<SemanticVersion> GetLocalVersionAsync()
        {
            var productVersion = GetProductVersion();
            var localVersion = SemanticVersion.Parse(productVersion);
            return Task.FromResult(localVersion);
        }
    }
}

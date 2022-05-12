using System.Threading.Tasks;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface IAutoUpdateService
    {
        Task<UpdateCheckResult> CheckForUpdateAsync();

        Task<GithubVersionDto> GetLatestVersionAsync(bool prerelease);

        Task InstallUpdateAsync(GithubVersionDto updateVersion);
    }
}

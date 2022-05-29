using System.Threading.Tasks;
using NuGet.Versioning;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface IRemoteVersionChecker
    {
        Task<IVersionSource> GetLatestVersionAsync();
    }
}

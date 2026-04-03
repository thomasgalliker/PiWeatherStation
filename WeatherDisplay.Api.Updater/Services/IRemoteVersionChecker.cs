using System.Threading.Tasks;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface IRemoteVersionChecker
    {
        Task<IUpdateVersionSource> GetLatestVersionAsync();
    }
}

using System.Threading.Tasks;
using NuGet.Versioning;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface ILocalVersionChecker
    {
        Task<SemanticVersion> GetLocalVersionAsync();
    }
}

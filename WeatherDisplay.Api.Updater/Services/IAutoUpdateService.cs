using System.Threading.Tasks;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface IAutoUpdateService
    {
        Task<UpdateCheckResult> CheckForUpdateAsync(bool force = false);

        void StartUpdate(UpdateRequest updateRequest);
    }
}

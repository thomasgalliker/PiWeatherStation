using System.Threading.Tasks;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface IAutoUpdateService
    {
        Task<UpdateCheckResult> CheckForUpdateAsync();

        void StartUpdate(UpdateRequest updateRequest);
    }
}

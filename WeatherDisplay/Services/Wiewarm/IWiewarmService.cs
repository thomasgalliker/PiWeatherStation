using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherDisplay.Model.Wiewarm;

namespace WeatherDisplay.Services.Wiewarm
{
    public interface IWiewarmService
    {
        Task<Bad> GetBadByIdAsync(int badId);

        Task<IEnumerable<Bad>> SearchBadAsync(string search);
    }
}
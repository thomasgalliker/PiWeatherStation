using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherDisplay.Model.Wiewarm;

namespace WeatherDisplay.Services.Wiewarm
{
    public interface IWiewarmService
    {
        Task<Bath> GetBathByIdAsync(int badId);

        Task<IEnumerable<Bath>> SearchBathsAsync(string search);
    }
}
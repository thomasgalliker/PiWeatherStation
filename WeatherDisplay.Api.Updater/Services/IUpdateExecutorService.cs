using System.Threading.Tasks;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    internal interface IUpdateExecutorService
    {
        Task RunAsync(UpdateRequestDto updateRequestDto);
    }
}
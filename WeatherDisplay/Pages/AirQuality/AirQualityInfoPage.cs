using System.Threading.Tasks;
using WeatherDisplay.Services.Navigation;

namespace WeatherDisplay.Pages.AirQuality
{
    public class AirQualityInfoPage : INavigatedTo
    {
        public Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }
    }
}

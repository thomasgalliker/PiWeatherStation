using System.Threading.Tasks;
using WeatherDisplay.Services.Navigation;

namespace WeatherDisplay.Pages.Astronomy
{
    public class SpaceWeatherConditionsPage : IPage, INavigatedTo
    {
        public Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }
    }
}

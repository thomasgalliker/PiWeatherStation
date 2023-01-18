using System.Threading.Tasks;
using WeatherDisplay.Services.Navigation;

namespace WeatherDisplay.Pages.Astronomy
{
    public class SpaceWeatherConditionsPage : INavigatedTo
    {
        public Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }
    }
}

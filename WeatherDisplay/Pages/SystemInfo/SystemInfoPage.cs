using System.Threading.Tasks;
using WeatherDisplay.Services.Navigation;

namespace WeatherDisplay.Pages.SystemInfo
{
    public class SystemInfoPage : INavigatedTo
    {
        public Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }
    }
}

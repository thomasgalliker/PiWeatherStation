using System.Threading.Tasks;

namespace WeatherDisplay.Pages.SystemInfo
{
    public class SystemInfoPage : INavigatedAware
    {
        public Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }
    }
}

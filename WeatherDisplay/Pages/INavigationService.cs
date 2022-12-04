using System.Threading.Tasks;

namespace WeatherDisplay.Pages
{
    public interface INavigationService
    {
        Task NavigateAsync(string name);

        Task NavigateAsync(string name, INavigationParameters navigationParameters);
    }
}
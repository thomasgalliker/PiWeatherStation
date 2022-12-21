using System.Threading.Tasks;

namespace WeatherDisplay.Pages
{
    public interface INavigationService
    {
        string GetCurrentPage();

        Task NavigateAsync(string name);

        Task NavigateAsync(string name, INavigationParameters navigationParameters);
    }
}
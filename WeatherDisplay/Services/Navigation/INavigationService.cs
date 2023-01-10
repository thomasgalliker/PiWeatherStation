using System.Threading.Tasks;

namespace WeatherDisplay.Services.Navigation
{
    public interface INavigationService
    {
        string GetCurrentPage();

        Task NavigateAsync(string name);

        Task NavigateAsync(string name, INavigationParameters navigationParameters);
    }
}
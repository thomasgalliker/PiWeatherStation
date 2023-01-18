using System.Threading.Tasks;

namespace WeatherDisplay.Services.Navigation
{
    public interface INavigatedFrom
    {
        /// <summary>
        /// Called when the implementer has been navigated from.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        Task OnNavigatedFromAsync(INavigationParameters parameters);
    }
}
using System.Threading.Tasks;

namespace WeatherDisplay.Services.Navigation
{
    public interface INavigatedTo
    {
        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        Task OnNavigatedToAsync(INavigationParameters parameters);
    }
}
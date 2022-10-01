using WeatherDisplay.Api.Models;

namespace WeatherDisplay.Api.Services
{
    public interface IUserService
    {
        User GetUser(string username, string password);
    }
}

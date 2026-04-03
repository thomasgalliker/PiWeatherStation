using Microsoft.Extensions.Options;
using WeatherDisplay.Api.Models;
using WeatherDisplay.Model.Settings;

namespace WeatherDisplay.Api.Services
{
    public class UserService : IUserService
    {
        private const string DefaultUserName = "pi";
        private readonly AccessPointSettings accessPointSettings;

        public UserService(IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings)
        {
            this.accessPointSettings = appSettings.Value.AccessPoint;
        }

        public User GetUser(string username, string password)
        {
            if (this.accessPointSettings.PSK != null &&
                string.Equals(username, DefaultUserName, StringComparison.Ordinal) &&
                string.Equals(password, this.accessPointSettings.PSK, StringComparison.Ordinal))
            {
                return new User
                {
                    Id = $"{Guid.NewGuid():B}",
                    Username = username,
                };
            }

            return null;
        }
    }
}

using Microsoft.Extensions.Options;
using WeatherDisplay.Api.Models;

namespace WeatherDisplay.Api.Services
{
    public class UserService : IUserService
    {
        private readonly UserServiceOptions userServiceOptions;

        public UserService(IOptions<UserServiceOptions> userServiceOptions)
        {
            this.userServiceOptions = userServiceOptions.Value;
        }

        public User GetUser(string username, string password)
        {
            if (string.Equals(username, this.userServiceOptions.Username, StringComparison.Ordinal) &&
                string.Equals(password, this.userServiceOptions.Password, StringComparison.Ordinal))
            {
                return new User
                {
                    Id = $"{Guid.NewGuid():B}",
                    Username = this.userServiceOptions.Username,
                };
            }

            return null;
        }
    }
}

using WeatherDisplay.Api.Models;

namespace WeatherDisplay.Api.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> users = new List<User>
        {
            // Hard-coded list of users. 
            // Don't ever use this code in production!
            // Don't ever store passwords - neither in clear text nor encrypted!
            new User { Id = Guid.NewGuid().ToString(), Username = "pi", Password = "raspberry" },
        };

        public User GetUser(string username, string password)
        {
            var user = this.users.SingleOrDefault(x => x.Username == username && x.Password == password);
            return user;
        }
    }
}
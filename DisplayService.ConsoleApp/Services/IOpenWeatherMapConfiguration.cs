
namespace DisplayService.ConsoleApp.Services
{
    public interface IOpenWeatherMapConfiguration
    {
        string ApiKey { get; }

        string UnitSystem { get; }
    }
}
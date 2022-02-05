namespace DisplayService.ConsoleApp.Service
{
    public interface IOpenWeatherMapConfiguration
    {
        string ApiKey { get; }

        string UnitSystem { get; }
    }
}
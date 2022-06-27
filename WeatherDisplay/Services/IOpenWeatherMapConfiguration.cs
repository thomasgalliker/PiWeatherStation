namespace WeatherDisplay.Services
{
    public interface IOpenWeatherMapConfiguration
    {
        string ApiEndpoint { get; }

        string ApiKey { get; }

        string UnitSystem { get; }
        
        string Language { get; }
       
        bool VerboseLogging{ get; }
    }
}
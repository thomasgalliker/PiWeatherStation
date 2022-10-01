namespace WeatherDisplay.Api.Services
{
    public interface IWeatherDisplayServiceConfigurator
    {
        void RestartService();

        void StartService();

        void StopService();

        void InstallService();

        void ReinstallService();

        void UninstallService();
    }
}
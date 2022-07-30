namespace WeatherDisplay.Api.Services
{
    public interface IWeatherDisplayHardwareCoordinator
    {
        Task HandleButtonPress(int buttonId);
    }
}
namespace WeatherDisplay.Services
{
    public interface IShutdownService
    {
        void Shutdown();

        void Reboot();
    }
}

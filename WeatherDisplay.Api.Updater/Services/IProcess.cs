namespace WeatherDisplay.Api.Updater.Services
{
    public interface IProcess
    {
        bool Start();

        void WaitForExit();

        int ExitCode { get; }
    }
}
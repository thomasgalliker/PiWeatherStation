using System.Diagnostics;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface IProcessFactory
    {
        IProcess CreateProcess(ProcessStartInfo processStartInfo);

        IProcess GetCurrentProcess();
    }
}
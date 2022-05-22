using System.Diagnostics;

namespace WeatherDisplay.Api.Updater.Services
{
    public class SystemProcessFactory : IProcessFactory
    {
        public IProcess CreateProcess(ProcessStartInfo processStartInfo)
        {
            return new SystemProcess(processStartInfo);
        }

        public IProcess GetCurrentProcess()
        {
            var process = Process.GetCurrentProcess();
            return new SystemProcess(process);
        }
    }
}
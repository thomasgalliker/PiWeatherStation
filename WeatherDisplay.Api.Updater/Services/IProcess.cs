using System.Diagnostics;
using System.IO;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface IProcess
    {
        bool Start();

        void WaitForExit();

        int ExitCode { get; }

        StreamReader StandardOutput { get; }
        
        StreamReader StandardError { get; }
        
        int Id { get; }

        event DataReceivedEventHandler OutputDataReceived;

        event DataReceivedEventHandler ErrorDataReceived;

        void BeginOutputReadLine();

        void BeginErrorReadLine();
    }
}
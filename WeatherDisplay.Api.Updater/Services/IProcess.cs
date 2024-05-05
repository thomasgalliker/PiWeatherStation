using System.Diagnostics;
using System.IO;

namespace WeatherDisplay.Api.Updater.Services
{
    public interface IProcess
    {
        /// <inheritdoc cref="Process.Start()"/>
        bool Start();

        /// <inheritdoc cref="Process.WaitForExit()"/>
        void WaitForExit();

        /// <inheritdoc cref="Process.ExitCode"/>
        int ExitCode { get; }

        StreamReader StandardOutput { get; }
        
        StreamReader StandardError { get; }

        /// <inheritdoc cref="Process.Id"/>
        int Id { get; }

        event DataReceivedEventHandler OutputDataReceived;

        event DataReceivedEventHandler ErrorDataReceived;

        void BeginOutputReadLine();

        void BeginErrorReadLine();
    }
}
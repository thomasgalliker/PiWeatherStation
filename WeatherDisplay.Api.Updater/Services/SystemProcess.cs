using System.Diagnostics;

namespace WeatherDisplay.Api.Updater.Services
{
    public class SystemProcess : IProcess
    {
        private readonly Process process;

        public SystemProcess(ProcessStartInfo processStartInfo)
        {
            this.process = new Process()
            {
                StartInfo = processStartInfo,
            };
        }

        public bool Start()
        {
            return this.process.Start();
        }

        public void WaitForExit()
        {
            this.process.WaitForExit();
        }

        public int ExitCode => this.process.ExitCode;
    }
}
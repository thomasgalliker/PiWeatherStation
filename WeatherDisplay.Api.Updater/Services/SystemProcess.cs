using System.Diagnostics;
using System.IO;

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
        
        public SystemProcess(Process process)
        {
            this.process = process;
        }

        public bool Start()
        {
            return this.process.Start();
        }

        public void WaitForExit()
        {
            this.process.WaitForExit();
        }

        public int Id => this.process.Id;

        public int ExitCode => this.process.ExitCode;

        public StreamReader StandardOutput => this.process.StandardOutput;

        public event DataReceivedEventHandler OutputDataReceived
        {
            add
            {
                this.process.OutputDataReceived += value;
            }
            remove
            {
                this.process.OutputDataReceived -= value;
            }
        }

        public event DataReceivedEventHandler ErrorDataReceived
        {
            add
            {
                this.process.ErrorDataReceived += value;
            }
            remove
            {
                this.process.ErrorDataReceived -= value;
            }
        }

        public void BeginOutputReadLine()
        {
            this.process.BeginOutputReadLine();
        }
        
        public void BeginErrorReadLine()
        {
            this.process.BeginErrorReadLine();
        }

        public StreamReader StandardError => this.process.StandardError;
    }
}
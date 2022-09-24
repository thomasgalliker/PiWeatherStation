using System.Reflection;

namespace RaspberryPi.Process
{
    public class CommandLineResult
    {
        public CommandLineResult(int exitCode, string outputData, string errorData)
        {
            this.ExitCode = exitCode;
            this.Success = exitCode == 0;
            this.OutputData = outputData;
            this.ErrorData = errorData;
        }

        public int ExitCode { get; }

        public bool Success { get; }

        public string OutputData { get; }

        public string ErrorData { get; }

        public override string ToString()
        {
            return $"ExitCode: {this.ExitCode}";
        }
    }
}
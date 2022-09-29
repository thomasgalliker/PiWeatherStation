using System.Reflection;

namespace RaspberryPi.Process
{
    public class CommandLineResult
    {
        private const int SuccessExitCode = 0;

        public CommandLineResult(int exitCode) : this(exitCode, null, null)
        {
        }
        
        public CommandLineResult(string outputData) : this(SuccessExitCode, outputData, null)
        {
        }

        public CommandLineResult(int exitCode, string outputData, string errorData)
        {
            this.ExitCode = exitCode;
            this.Success = exitCode == SuccessExitCode;
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
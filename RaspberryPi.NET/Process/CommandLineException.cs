using System;

namespace RaspberryPi.Process
{
    public class CommandLineException : Exception
    {
        private const int DefaultErrorExitCode = -1;

        internal CommandLineException(Exception innerException)
            : base($"Command line execution failed. See the inner exception for more details.", innerException)
        {
        }

        internal CommandLineException(CommandLineResult result)
            : this(result.ExitCode)
        {
            this.ErrorData = result.ErrorData;
        }

        internal CommandLineException(int exitCode)
            : base($"Command line execution failed with exit code {exitCode}")
        {
            this.ExitCode = exitCode;
        }

        public int ExitCode { get; } = DefaultErrorExitCode;

        public string ErrorData { get; }

        public override string ToString()
        {
            return $"ExitCode: {this.ExitCode}";
        }
    }
}
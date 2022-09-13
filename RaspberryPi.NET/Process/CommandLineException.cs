using System;

namespace RaspberryPi.Process
{
    public class CommandLineException : Exception
    {
        internal CommandLineException(CommandLineResult result)
            : base($"Command line execution failed with exit code {result.ExitCode}")
        {
            this.ExitCode = result.ExitCode;
            this.ErrorData = result.ErrorData;
        }

        public int ExitCode { get; }

        public string ErrorData { get; }

        public override string ToString()
        {
            return $"ExitCode: {this.ExitCode}";
        }
    }
}
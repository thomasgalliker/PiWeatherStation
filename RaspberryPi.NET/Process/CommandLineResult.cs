namespace RaspberryPi.Process
{
    public class CommandLineResult
    {
        public CommandLineResult(int exitCode, string outputData, string errorData)
        {
            this.ExitCode = exitCode;
            this.OutputData = outputData;
            this.ErrorData = errorData;
        }

        public int ExitCode { get; }

        public string OutputData { get; set; }

        public string ErrorData { get; set; }

        public override string ToString()
        {
            return $"ExitCode: {this.ExitCode}";
        }
    }
}
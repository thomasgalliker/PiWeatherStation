namespace RaspberryPi
{
    public class CommandLineInvocation
    {
        public CommandLineInvocation(string executable, string arguments, string systemArguments = null)
        {
            this.Executable = executable;
            this.Arguments = arguments;
            this.SystemArguments = systemArguments;
        }

        public string Executable { get; }

        public string Arguments { get; }

        // Arguments only used when we are invoking this directly from within the tools - not used when 
        // exporting the script for use later.
        public string SystemArguments { get; }

        public bool IgnoreFailedExitCode { get; set; }

        public override string ToString()
            => "\"" + this.Executable + "\" " + this.Arguments;
    }
}
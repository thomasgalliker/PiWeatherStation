using System;

namespace RaspberryPi.Process
{
    public class CommandLineInvocation
    {
        public CommandLineInvocation(string commandLine, string workingDirectory = null)
            : this(ParseCommandLine(commandLine), workingDirectory)
        {
        }

        private CommandLineInvocation((string executable, string arguments) commandLine, string workingDirectory = null)
            : this(commandLine.executable, commandLine.arguments, workingDirectory)
        {
        }


        public CommandLineInvocation(string executable, string arguments, string workingDirectory = null)
        {
            if (string.IsNullOrEmpty(executable))
            {
                throw new ArgumentException($"Parameter '{nameof(executable)}' must not be null or empty", nameof(executable));
            }

            if (executable.Length <= 2 && executable.Contains(@""""))
            {
                throw new ArgumentException($"Parameter '{nameof(executable)}' is not valid", nameof(executable));
            }

            this.Executable = executable;
            this.Arguments = arguments;
            this.WorkingDirectory = string.IsNullOrWhiteSpace(workingDirectory) ? Environment.CurrentDirectory : workingDirectory;
        }

        private static (string Executable, string Arguments) ParseCommandLine(string commandLine)
        {
            if (string.IsNullOrEmpty(commandLine))
            {
                throw new ArgumentException($"Parameter '{nameof(commandLine)}' must not be null or empty", nameof(commandLine));
            }

            var executable = commandLine;

            if (!string.IsNullOrEmpty(executable))
            {
                executable = executable.Trim();
            }

            var arguments = string.Empty;

            if (!string.IsNullOrEmpty(executable) && executable.Length > 2)
            {
                if (executable.StartsWith(@""""))
                {
                    var secondQuotePosition = executable.IndexOf(@"""", 1);
                    var secondQuotePositionNext = secondQuotePosition + 1;
                    if (secondQuotePosition > 0 && executable.Length > secondQuotePositionNext)
                    {
                        arguments = executable.Substring(secondQuotePositionNext).Trim();
                        executable = executable.Substring(0, secondQuotePositionNext).Trim();
                    }
                }
                else
                {
                    var emptySpacePosition = executable.IndexOf(" ");
                    var emptySpacePositionNext = emptySpacePosition + 1;
                    if (emptySpacePosition > 0 && executable.Length > emptySpacePositionNext)
                    {
                        arguments = executable.Substring(emptySpacePositionNext).Trim();
                        executable = executable.Substring(0, emptySpacePositionNext).Trim();
                    }
                }
            }

            return (executable, arguments);
        }

        public string Executable { get; }

        public string Arguments { get; }

        public string WorkingDirectory { get; }

        public override string ToString()
        {
            return $"{this.Executable} {this.Arguments}";
        }
    }
}
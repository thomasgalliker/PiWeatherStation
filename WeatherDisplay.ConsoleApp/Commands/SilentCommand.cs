using System;
using System.CommandLine;

namespace WeatherDisplay.ConsoleApp.Commands
{
    public class SilentCommand : Command
    {
        public SilentCommand() : base(name: "silent", "Disables console logging")
        {
            var backupOut = Console.Out;
            this.Add(ProgramOptions.SilentOption);
            this.SetAction(parseResult =>
            {
                // TODO: Disable console logging (ILogger)
                var silent = parseResult.GetValue(ProgramOptions.SilentOption);
                if (silent)
                {
                    Console.SetOut(TextWriter.Null);
                }
                else
                {
                    Console.SetOut(backupOut);
                }

                return Task.FromResult(0);
            });
        }
    }
}

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;

namespace DisplayService.ConsoleApp.Commands
{
    public class SilentCommand : Command
    {
        public SilentCommand() : base(name: "silent", "Disables console logging")
        {
            this.Handler = new SilentCommandHandler();
        }

        private class SilentCommandHandler : ICommandHandler
        {
            private readonly TextWriter backupOut;

            public SilentCommandHandler()
            {
                this.backupOut = Console.Out;
            }

            public Task<int> InvokeAsync(InvocationContext context)
            {
                // TODO: Disable console logging (ILogger)
                var silent = context.ParseResult.GetValueForOption(ProgramOptions.SilentOption);
                if (silent)
                {
                    Console.SetOut(TextWriter.Null);
                }
                else
                {

                    Console.SetOut(this.backupOut);
                }

                return Task.FromResult(0);
            }
        }
    }
}
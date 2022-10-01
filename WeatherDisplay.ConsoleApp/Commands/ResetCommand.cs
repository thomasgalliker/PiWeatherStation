using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using DisplayService.Services;

namespace DisplayService.ConsoleApp.Commands
{
    public class ResetCommand : Command
    {
        public ResetCommand(IDisplayManager displayService) : base(name: "reset", "Resets the display")
        {
            this.Handler = new ClearCommandHandler(displayService);
        }

        private class ClearCommandHandler : ICommandHandler
        {
            private readonly IDisplayManager displayService;

            public ClearCommandHandler(IDisplayManager displayService)
            {
                this.displayService = displayService;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                Console.Clear();
                await this.displayService.ResetAsync();
                return 0;
            }
        }
    }
}
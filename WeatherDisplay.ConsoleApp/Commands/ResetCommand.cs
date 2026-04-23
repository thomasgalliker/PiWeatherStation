using System;
using System.CommandLine;
using System.Threading.Tasks;
using DisplayService.Services;

namespace WeatherDisplay.ConsoleApp.Commands
{
    public class ResetCommand : Command
    {
        public ResetCommand(IDisplayManager displayService) : base(name: "reset", "Resets the display")
        {
            this.SetAction(_ =>
            {
                Console.Clear();
                displayService.Reset();
                return Task.FromResult(0);
            });
        }
    }
}

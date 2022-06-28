using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using DisplayService.Services;
using WeatherDisplay;
using WeatherDisplay.Model;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.OpenWeatherMap;

namespace DisplayService.ConsoleApp.Commands
{
    public class StartCommand : Command
    {
        public const string CommandName = "start";

        public StartCommand(
            IDisplayManager displayManager,
            IOpenWeatherMapService openWeatherMapService,
            ITranslationService translationService,
            IDateTime dateTime,
            IAppSettings appSettings) : base(CommandName, "Starts the scheduled rendering process")
        {
            this.Handler = new StartCommandHandler(displayManager);

            displayManager.AddWeatherRenderActions(openWeatherMapService, translationService, dateTime, appSettings);
        }

        private class StartCommandHandler : ICommandHandler
        {
            private readonly IDisplayManager displayManager;

            public StartCommandHandler(IDisplayManager displayManager)
            {
                this.displayManager = displayManager;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                await this.displayManager.StartAsync();

                return 0;
            }
        }
    }
}
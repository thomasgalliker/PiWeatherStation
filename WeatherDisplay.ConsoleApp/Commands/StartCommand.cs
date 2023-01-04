using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using DisplayService.Services;
using OpenWeatherMap;
using WeatherDisplay.Model;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.Navigation;

namespace DisplayService.ConsoleApp.Commands
{
    public class StartCommand : Command
    {
        public const string CommandName = "start";

        public StartCommand(
            INavigationService navigationService,
            IOpenWeatherMapService openWeatherMapService,
            ITranslationService translationService,
            IDateTime dateTime,
            IAppSettings appSettings) : base(CommandName, "Starts the scheduled rendering process")
        {
            this.Handler = new StartCommandHandler(navigationService);
        }

        private class StartCommandHandler : ICommandHandler
        {
            private readonly INavigationService displayManager;

            public StartCommandHandler(INavigationService displayManager)
            {
                this.displayManager = displayManager;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                await this.displayManager.NavigateAsync("OpenWeatherMapPage");

                return 0;
            }
        }
    }
}
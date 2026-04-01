using System.CommandLine;
using DisplayService.Services;
using OpenWeatherMap;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.Navigation;

namespace WeatherDisplay.ConsoleApp.Commands
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
            this.SetAction(async _ =>
            {
                // await navigationService.NavigateAsync("OpenWeatherMapPage");
                return 0;
            });
        }
    }
}

using System.CommandLine;

namespace WeatherDisplay.ConsoleApp
{
    public static class ProgramOptions
    {
        public static readonly Option<bool> ClearOption = new Option<bool>(
            name: "--clear",
            aliases: new[] { "clear" })
        {
            Description = "Clears the display",
            DefaultValueFactory = _ => false,
            Required = false,
            Arity = ArgumentArity.ZeroOrOne,
        };

        public static readonly Option<bool> SilentOption = new Option<bool>(
            name: "--silent",
            aliases: new[] { "silent" })
        {
            Description = "Silences command output on standard out.",
            DefaultValueFactory = _ => true,
            Required = false,
            Arity = ArgumentArity.ZeroOrOne,
        };
    }
}

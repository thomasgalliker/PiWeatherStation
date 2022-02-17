using System.CommandLine;

namespace DisplayService.ConsoleApp
{
    public static class ProgramOptions
    {
        public static readonly Option<bool> ClearOption = new Option<bool>(
            aliases: new[] { "clear", "--clear" },
            getDefaultValue: () => false,
            description: "Clears the display")
        {
            IsRequired = false,
            Arity = ArgumentArity.ZeroOrOne,
        };

        public static readonly Option<bool> SilentOption = new Option<bool>(
            aliases: new[] { "silent", "--silent" },
            getDefaultValue: () => true,
            description: "Silences command output on standard out.")
        {
            IsRequired = false,
            Arity = ArgumentArity.ZeroOrOne,
        };
    }
}
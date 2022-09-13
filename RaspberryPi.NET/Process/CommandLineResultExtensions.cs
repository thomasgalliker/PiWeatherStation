namespace RaspberryPi.Process
{
    public static class CommandLineResultExtensions
    {
        /// <summary>
        /// Throws <seealso cref="CommandLineException"/> if exit code is not equal to 0.
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="CommandLineException"></exception>
        public static void EnsureSuccessExitCode(this CommandLineResult result)
        {
            if (result.ExitCode != 0)
            {
                throw new CommandLineException(result);
            }
        }
    }
}
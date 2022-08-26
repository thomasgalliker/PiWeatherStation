using System.Collections.Generic;

namespace RaspberryPi
{
    public class CmdResult
    {
        public CmdResult(int exitCode, IEnumerable<string> infos, IEnumerable<string> errors)
        {
            this.ExitCode = exitCode;
            this.Infos = infos;
            this.Errors = errors;
        }

        public int ExitCode { get; }

        public IEnumerable<string> Infos { get; }

        public IEnumerable<string> Errors { get; }
    }
}
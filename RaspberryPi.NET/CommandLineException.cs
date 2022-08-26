using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaspberryPi
{
    public class CommandLineException : Exception
    {
        private readonly int exitCode;

        public CommandLineException(int exitCode, IEnumerable<string> errors)
        {
            this.exitCode = exitCode;
            this.Errors = errors;
        }

        public IEnumerable<string> Errors { get; }

        public override string Message
        {
            get
            {
                var stringBuilder = new StringBuilder(base.Message);
                stringBuilder.AppendFormat(" Exit code: {0}", this.exitCode);
                if (this.Errors.Any())
                {
                    stringBuilder.AppendLine(string.Join(Environment.NewLine, this.Errors));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
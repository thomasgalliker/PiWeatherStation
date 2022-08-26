using System;
using System.Threading;
using RaspberryPi.Internals;

namespace RaspberryPi
{
    public interface IProcessRunner
    {
        CmdResult ExecuteCommand(CommandLineInvocation invocation, CancellationToken cancellationToken = default);

        CmdResult ExecuteCommand(CommandLineInvocation invocation, string workingDirectory, CancellationToken cancellationToken = default);

        int ExecuteCommand(string executable, string arguments, string workingDirectory, Action<string> debug, Action<string> info, Action<string> error, CancellationToken cancellationToken = default);
    }
}
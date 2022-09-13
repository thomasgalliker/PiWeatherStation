using System.Threading;

namespace RaspberryPi.Process
{
    public interface IProcessRunner
    {
        CommandLineResult TryExecuteCommand(string invocation, CancellationToken cancellationToken = default);

        CommandLineResult TryExecuteCommand(CommandLineInvocation invocation, CancellationToken cancellationToken = default);

        CommandLineResult ExecuteCommand(string invocation, CancellationToken cancellationToken = default);

        CommandLineResult ExecuteCommand(CommandLineInvocation invocation, CancellationToken cancellationToken = default);
    }
}
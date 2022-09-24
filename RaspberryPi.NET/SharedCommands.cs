using System.Threading;
using RaspberryPi.Process;

namespace RaspberryPi
{
    public static class SharedCommands
    {
        public static readonly CommandLineInvocation IsSystemdInstalledCommand = new CommandLineInvocation(
            executable: "/bin/bash", 
            arguments: "-c \"command -v systemctl >/dev/null\"");

        public static bool IsSystemdInstalled(this IProcessRunner processRunner, CancellationToken cancellationToken = default)
        {
            var result = processRunner.TryExecuteCommand(IsSystemdInstalledCommand, cancellationToken);
            return result.Success;
        }

        public static readonly CommandLineInvocation HaveSudoPrivilegesCommand = new CommandLineInvocation(
            executable: "/bin/bash", 
            arguments: "-c \"sudo -vn 2> /dev/null\"");

        public static bool HaveSudoPrivileges(this IProcessRunner processRunner, CancellationToken cancellationToken = default)
        {
            var result = processRunner.TryExecuteCommand(HaveSudoPrivilegesCommand, cancellationToken);
            return result.Success;
        }
    }
}

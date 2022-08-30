namespace RaspberryPi.Services
{
    public enum ServiceType
    {
        /// <summary>
        /// The main process of the service is specified in the start line.
        /// This is the default if the Type= and Busname= directives are not set, but the ExecStart= is set.
        /// Any communication should be handled outside of the unit through a second unit of the appropriate type (like through a .socket unit if this unit must communicate using sockets).
        /// </summary>
        Simple = 0,

        /// <summary>
        /// This service type is used when the service forks a child process, exiting the parent process almost immediately.
        /// This tells systemd that the process is still running even though the parent exited.
        /// </summary>
        Forking,

        /// <summary>
        /// This type indicates that the process will be short-lived and that systemd should wait for the process to exit before continuing on with other units.
        /// This is the default Type= and ExecStart= are not set. It is used for one-off tasks.
        /// </summary>
        Oneshot,

        /// <summary>
        /// This indicates that unit will take a name on the D-Bus bus. When this happens, systemd will continue to process the next unit.
        /// </summary>
        Dbus,

        /// <summary>
        /// This indicates that the service will issue a notification when it has finished starting up.
        /// The systemd process will wait for this to happen before proceeding to other units.
        /// </summary>
        Notify,

        /// <summary>
        /// This indicates that the service will not be run until all jobs are dispatched.
        /// </summary>
        Idle
    }
}
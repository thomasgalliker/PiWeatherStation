namespace RaspberryPi.Services
{
    public enum KillMode
    {
        No = 0,

        /// <summary>
        /// Only kills the main identified process. Others are left untouched.
        /// </summary>
        Process,

        ControlGroup,

        Mixed
    }
}
namespace RaspberryPi.Services
{
    /// <summary>
    /// This indicates the circumstances under which systemd will attempt to automatically restart the service.
    /// This can be set to values like "always", "on-success", "on-failure", "on-abnormal", "on-abort", or "on-watchdog". 
    /// These will trigger a restart according to the way that the service was stopped.
    /// </summary>
    public enum ServiceRestart
    {
        No = 0,
        Always,
        OnSuccess,
        OnFailure,
        OnAbnormal,
        OnAbort,
        OnWatchdog,
    }
}
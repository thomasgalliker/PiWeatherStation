namespace WeatherDisplay.Api.Updater.Services
{
    public class AutoUpdateOptions
    {
        public virtual bool CheckForUpdateAtStartup { get; set; }

        public virtual string DotnetExecutable { get; set; }

        public virtual string UpdaterDirectoryName { get; set; }

        public virtual string UpdaterExecutable { get; set; }
    }
}

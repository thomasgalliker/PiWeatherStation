namespace WeatherDisplay.Api.Updater.Services
{
    public class AutoUpdateOptions
    {
        public virtual bool CheckForUpdateAtStartup { get; set; }

        public virtual string DotnetExecutable { get; set; }

        public virtual string UpdaterDirectoryName { get; set; }

        public virtual string UpdaterExecutable { get; set; }

        public virtual string GithubRepositoryUrl { get; set; }

        public virtual bool PreRelease { get; set; }

        public virtual string InstalledVersionFile { get; set; }
    }
}

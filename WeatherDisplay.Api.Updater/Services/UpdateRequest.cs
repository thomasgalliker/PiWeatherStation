using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class UpdateRequest
    {
        public string CurrentDirectory { get; set; }

        public SemanticVersion UpdateVersion { get; set; }

        public IExecutorStep[] ExecutorSteps { get; set; }
    }
}
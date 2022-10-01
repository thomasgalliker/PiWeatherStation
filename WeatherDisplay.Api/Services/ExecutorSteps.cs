using System.Reflection;
using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;
using WeatherDisplay.Api.Updater.Services;

namespace WeatherDisplay.Api.Services
{
    public static class UpdateRequestFactory
    {
        public static UpdateRequest Create(SemanticVersion updateVersion, IUpdateVersionSource updateVersionSource)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var updateRequest = new UpdateRequest
            {
                CurrentDirectory = currentDirectory,
                UpdateVersion = updateVersion,
                ExecutorSteps = GetExecutorSteps(currentDirectory, updateVersionSource),
            };
            return updateRequest;
        }

        private static IExecutorStep[] GetExecutorSteps(string currentDirectory, IUpdateVersionSource updateVersionSource)
        {
            var downloadHttpFileStep = updateVersionSource.GetDownloadStep();

            return new IExecutorStep[]
            {
               downloadHttpFileStep,
                new ProcessStartExecutorStep
                {
                    FileName = "sudo",
                    Arguments = "systemctl stop weatherdisplay.api.service",
                    CreateNoWindow = true,
                },
                new ExtractZipStep
                {
                    SourceArchiveFileName = downloadHttpFileStep.DestinationFileName,
                    DestinationDirectoryName = currentDirectory,
                    OverwriteFiles = true,
                    DeleteSourceArchive = true,
                },
                new ProcessStartExecutorStep
                {
                    FileName = "sudo",
                    Arguments = "systemctl daemon-reload",
                    CreateNoWindow = true,
                },
                new ProcessStartExecutorStep
                {
                    FileName = "sudo",
                    Arguments = "systemctl start weatherdisplay.api.service",
                    CreateNoWindow = true,
                },
                //new ProcessStartExecutorStep
                //{
                //    FileName = "sudo",
                //    Arguments = "reboot",
                //    CreateNoWindow = true,
                //}
            };
        }
    }
}

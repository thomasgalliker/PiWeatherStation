using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace WeatherDisplay.Api.Updater.Models
{
    public class UpdateRequestDto
    {
        public UpdateRequestDto()
        {
            this.ExecutorSteps = new List<IExecutorStep>();
        }


        public int CallingProcessId { get; set; }

        public string DownloadUrl { get; set; }

        public string WorkingDirectory { get; set; }

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Auto)]
        public ICollection<IExecutorStep> ExecutorSteps { get; set; }
    }

    public class DownloadFileStep : IExecutorStep
    {
        public string Url { get; set; }

        public string DestinationFileName { get; set; }
    }

    public class ExtractZipStep : IExecutorStep
    {
        public string SourceArchiveFileName { get; set; }

        public string DestinationDirectoryName { get; set; }

        public bool OverwriteFiles { get; set; }

        public bool DeleteSourceArchive { get; set; }
    }

    public class DeleteFileStep : IExecutorStep
    {
        public string Path { get; set; }
    }

    public class ProcessStartExecutorStep : IExecutorStep
    {
        public ProcessStartExecutorStep()
        {
        }

        public ProcessStartExecutorStep(string fileName, string arguments)
        {
            this.FileName = fileName;
            this.Arguments = arguments;
        }

        public string FileName { get; set; }

        public string Arguments { get; set; }

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool RedirectStandardOutput { get; set; }

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool UseShellExecute { get; set; }

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool CreateNoWindow { get; set; }
    }

    public interface IExecutorStep
    {
    }
}

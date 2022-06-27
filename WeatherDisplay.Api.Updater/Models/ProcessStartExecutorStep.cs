using System.ComponentModel;
using Newtonsoft.Json;

namespace WeatherDisplay.Api.Updater.Models
{
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
}

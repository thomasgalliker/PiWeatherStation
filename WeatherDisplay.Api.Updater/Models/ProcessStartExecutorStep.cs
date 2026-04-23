using System.Text.Json.Serialization;

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

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool RedirectStandardOutput { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool UseShellExecute { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool CreateNoWindow { get; set; }
    }
}

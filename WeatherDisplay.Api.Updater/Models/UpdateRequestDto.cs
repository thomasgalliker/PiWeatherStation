using System.Collections.Generic;

namespace WeatherDisplay.Api.Updater.Models
{
    public class UpdateRequestDto
    {
        public UpdateRequestDto()
        {
            this.ExecutorSteps = new List<IExecutorStep>();
        }

        public int CallingProcessId { get; set; }

        public string WorkingDirectory { get; set; }

        public ICollection<IExecutorStep> ExecutorSteps { get; set; }
    }
}

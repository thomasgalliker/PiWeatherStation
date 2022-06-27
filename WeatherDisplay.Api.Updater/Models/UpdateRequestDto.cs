using System.Collections.Generic;
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

        public string WorkingDirectory { get; set; }

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Auto)]
        public ICollection<IExecutorStep> ExecutorSteps { get; set; }
    }
}

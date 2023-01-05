using System.Collections.Generic;
using Newtonsoft.Json;

namespace Iot.Device.Model
{
    public class SensorDevice
    {
        public SensorDevice([JsonProperty("id")] string sensorId)
        {
            this.DeviceId = sensorId;
            this.Data = new List<SensorData>();
        }

        [JsonProperty("id")]
        public string DeviceId { get; }

        [JsonProperty("data")]
        public IReadOnlyCollection<SensorData> Data { get; set; }
    }
}

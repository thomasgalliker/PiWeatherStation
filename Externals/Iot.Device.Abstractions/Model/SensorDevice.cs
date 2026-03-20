using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Iot.Device.Model
{
    public class SensorDevice
    {
        [JsonConstructor]
        public SensorDevice(string sensorId)
        {
            this.DeviceId = sensorId;
            this.Data = new List<SensorData>();
        }

        [JsonPropertyName("id")]
        public string DeviceId { get; }

        [JsonPropertyName("data")]
        public IReadOnlyCollection<SensorData> Data { get; set; }
    }
}

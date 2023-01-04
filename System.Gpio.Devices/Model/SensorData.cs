using System.Collections.Generic;
using Newtonsoft.Json;
using UnitsNet;

namespace System.Gpio.Devices.Model
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

    public class SensorData
    {
        public SensorData([JsonProperty("id")] string sensorId)
        {
            this.SensorId = sensorId;
            this.SensorValues = new List<IQuantity>();
        }

        [JsonProperty("id")]
        public string SensorId { get; }

        [JsonProperty("dt")]
        public DateTime Date { get; set; }

        [JsonProperty("values")]
        public IReadOnlyCollection<IQuantity> SensorValues { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnitsNet;

namespace Iot.Device.Model
{
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

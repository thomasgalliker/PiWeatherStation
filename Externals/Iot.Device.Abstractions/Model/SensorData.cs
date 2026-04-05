using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnitsNet;

namespace Iot.Device.Model
{
    public class SensorData
    {
        [JsonConstructor]
        public SensorData(string sensorId)
        {
            this.SensorId = sensorId;
            this.SensorValues = new List<IQuantity>();
        }

        [JsonPropertyName("id")]
        public string SensorId { get; }

        [JsonPropertyName("dt")]
        public DateTime Date { get; set; }

        [JsonPropertyName("values")]
        public IReadOnlyCollection<IQuantity> SensorValues { get; set; }
    }
}

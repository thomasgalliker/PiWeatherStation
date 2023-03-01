using System;
using System.Linq;
using Iot.Device.Bmxx80.ReadResult;
using Iot.Device.Model;
using Iot.Device.Scd4x;
using UnitsNet;
using UnitsNet.Units;

namespace Iot.Device.Extensions
{
    public static class SensorDataExtensions
    {
        public static SensorData ToSensorData(this Bme680ReadResult readResult)
        {
            return new SensorData("bme680")
            {
                Date = DateTime.Now,
                SensorValues = new IQuantity[]
                {
                    readResult.Temperature,
                    readResult.Humidity,
                    readResult.Pressure?.ToUnit(PressureUnit.Hectopascal),
                    readResult.GasResistance?.ToUnit(ElectricResistanceUnit.Kiloohm),
                }
                .Where(v => v != null).ToArray()
            };
        }

        public static SensorData ToSensorData(this Scd41ReadResult readResult)
        {
            return new SensorData("scd41")
            {
                Date = DateTime.Now,
                SensorValues = new IQuantity[]
                {
                    readResult.Temperature,
                    readResult.RelativeHumidity,
                    readResult.Co2.ToUnit(VolumeConcentrationUnit.PartPerMillion),
                }
                .Where(v => v != null).ToArray()
            };
        }
    }
}

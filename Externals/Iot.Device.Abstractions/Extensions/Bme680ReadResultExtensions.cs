using System;
using System.Collections.Generic;
using System.Linq;
using Iot.Device.Bmxx80.ReadResult;
using Iot.Device.Model;
using UnitsNet;

namespace Iot.Device.Extensions
{
    public static class Bme680ReadResultExtensions
    {
        public static SensorData ToSensorData(this Bme680ReadResult readResult)
        {
            return new SensorData("bme680")
            {
                Date = DateTime.Now,
                SensorValues = readResult.ToSensorValues().ToList(),
            };
        }

        public static IEnumerable<IQuantity> ToSensorValues(this Bme680ReadResult readResult)
        {
            if (readResult.Temperature is Temperature temperature)
            {
                yield return temperature;
            }

            if (readResult.Humidity is RelativeHumidity relativeHumidity)
            {
                yield return relativeHumidity;
            }

            if (readResult.Pressure is Pressure pressure)
            {
                yield return pressure.ToUnit(UnitsNet.Units.PressureUnit.Hectopascal);
            }

            if (readResult.GasResistance is ElectricResistance gasResistance)
            {
                yield return gasResistance.ToUnit(UnitsNet.Units.ElectricResistanceUnit.Kiloohm);
            }
        }
    }
}

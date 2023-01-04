using System.Device.I2c;
using System.Gpio.Devices.BMxx80;
using UnitsNet;

namespace System.Device.Devices
{
    public class Bme680Wrapper : Bme680, IBme680
    {
        public Bme680Wrapper(I2cDevice i2cDevice) : base(i2cDevice)
        {
        }

        public Bme680Wrapper(I2cDevice i2cDevice, Temperature ambientTemperatureDefault) : base(i2cDevice, ambientTemperatureDefault)
        {
        }
    }
}

using System.Device.I2c;
using UnitsNet;

namespace Iot.Device.Bmxx80
{
    /// <inheritdoc/>
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

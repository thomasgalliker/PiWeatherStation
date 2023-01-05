using System.Device.I2c;
using UnitsNet;

namespace Iot.Device.Bmxx80
{
    public interface IBme680Factory
    {
        IBme680 Create(I2cConnectionSettings i2cSettings);

        IBme680 Create(I2cConnectionSettings i2cSettings, Temperature ambientTemperatureDefault);
    }
}

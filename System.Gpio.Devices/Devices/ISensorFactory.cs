using System.Device.I2c;
using UnitsNet;

namespace System.Device.Devices
{
    public interface ISensorFactory
    {
        IBme680 CreateBme680(I2cConnectionSettings i2cSettings);

        IBme680 CreateBme680(I2cConnectionSettings i2cSettings, Temperature ambientTemperatureDefault);
    }
}

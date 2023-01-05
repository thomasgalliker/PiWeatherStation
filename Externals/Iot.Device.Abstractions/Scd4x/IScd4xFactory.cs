using System.Device.I2c;

namespace Iot.Device.Scd4x
{
    public interface IScd4xFactory
    {
        IScd4x Create(I2cConnectionSettings i2cSettings);
    }
}

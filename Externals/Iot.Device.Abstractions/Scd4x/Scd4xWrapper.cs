using System.Device.I2c;
using UnitsNet;

namespace Iot.Device.Scd4x
{
    /// <inheritdoc/>
    public class Scd4xWrapper : IScd4x
    {
        private readonly Scd4x scd4x;

        public Scd4xWrapper(I2cDevice i2cDevice)
        {
            this.scd4x = new Scd4x(i2cDevice);
        }

        public Temperature Temperature => this.scd4x.Temperature;

        public RelativeHumidity RelativeHumidity => this.scd4x.RelativeHumidity;

        public VolumeConcentration Co2 => this.scd4x.Co2;

        public void Reset()
        {
            this.scd4x.Reset();
        }

        public void Dispose()
        {
            this.scd4x.Dispose();
        }
    }
}
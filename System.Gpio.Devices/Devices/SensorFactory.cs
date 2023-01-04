using System.Device.I2c;
using Microsoft.Extensions.Logging;
using UnitsNet;

namespace System.Device.Devices
{
    public class SensorFactory : ISensorFactory
    {
        private readonly ILogger logger;

        public SensorFactory(ILogger<SensorFactory> logger)
        {
            this.logger = logger;
        }

        public IBme680 CreateBme680(I2cConnectionSettings i2cSettings)
        {
            this.logger.LogDebug("CreateBme680");
            var i2cDevice = I2cDevice.Create(i2cSettings);
            return new Bme680Wrapper(i2cDevice);
        }

        public IBme680 CreateBme680(I2cConnectionSettings i2cSettings, Temperature ambientTemperatureDefault)
        {
            this.logger.LogDebug("CreateBme680");
            var i2cDevice = I2cDevice.Create(i2cSettings);
            return new Bme680Wrapper(i2cDevice, ambientTemperatureDefault);
        }
    }
}

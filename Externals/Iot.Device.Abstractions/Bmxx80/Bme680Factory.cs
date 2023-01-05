using System.Device.I2c;
using Microsoft.Extensions.Logging;
using UnitsNet;

namespace Iot.Device.Bmxx80
{
    public class Bme680Factory : IBme680Factory
    {
        private readonly ILogger logger;

        public Bme680Factory(ILogger<Bme680Factory> logger)
        {
            this.logger = logger;
        }

        public IBme680 Create(I2cConnectionSettings i2cSettings)
        {
            this.logger.LogDebug("CreateBme680");
            var i2cDevice = I2cDevice.Create(i2cSettings);
            return new Bme680Wrapper(i2cDevice);
        }

        public IBme680 Create(I2cConnectionSettings i2cSettings, Temperature ambientTemperatureDefault)
        {
            this.logger.LogDebug("CreateBme680");
            var i2cDevice = I2cDevice.Create(i2cSettings);
            return new Bme680Wrapper(i2cDevice, ambientTemperatureDefault);
        }
    }
}

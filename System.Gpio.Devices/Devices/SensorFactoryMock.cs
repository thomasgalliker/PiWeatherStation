using System.Device.I2c;
using Microsoft.Extensions.Logging;
using UnitsNet;

namespace System.Device.Devices
{
    public class SensorFactoryMock : ISensorFactory
    {
        private readonly ILogger logger;

        public SensorFactoryMock(ILogger<SensorFactoryMock> logger)
        {
            this.logger = logger;
        }

        public IBme680 CreateBme680(I2cConnectionSettings i2cSettings)
        {
            this.logger.LogDebug("CreateBme680");
            return new Bme680Mock(i2cSettings);
        }

        public IBme680 CreateBme680(I2cConnectionSettings i2cSettings, Temperature ambientTemperatureDefault)
        {
            this.logger.LogDebug("CreateBme680");
            return new Bme680Mock(i2cSettings, ambientTemperatureDefault);
        }
    }
}

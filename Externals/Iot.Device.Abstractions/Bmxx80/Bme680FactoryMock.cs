using System.Device.I2c;
using Microsoft.Extensions.Logging;
using UnitsNet;

namespace Iot.Device.Bmxx80
{
    public class Bme680FactoryMock : IBme680Factory
    {
        private readonly ILogger logger;

        public Bme680FactoryMock(ILogger<Bme680FactoryMock> logger)
        {
            this.logger = logger;
        }

        public IBme680 Create(I2cConnectionSettings i2cSettings)
        {
            this.logger.LogDebug("CreateBme680");
            return new Bme680Mock(i2cSettings);
        }

        public IBme680 Create(I2cConnectionSettings i2cSettings, Temperature ambientTemperatureDefault)
        {
            this.logger.LogDebug("CreateBme680");
            return new Bme680Mock(i2cSettings, ambientTemperatureDefault);
        }
    }
}

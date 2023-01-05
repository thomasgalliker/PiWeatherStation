using System.Device.I2c;
using Microsoft.Extensions.Logging;
using UnitsNet;

namespace Iot.Device.Scd4x
{
    public class Scd41FactoryMock : IScd4xFactory
    {
        private readonly ILogger logger;

        public Scd41FactoryMock(ILogger<Scd41FactoryMock> logger)
        {
            this.logger = logger;
        }

        public IScd4x Create(I2cConnectionSettings i2cSettings)
        {
            this.logger.LogDebug("Create");
            return new Scd41Mock();
        }
    }
}

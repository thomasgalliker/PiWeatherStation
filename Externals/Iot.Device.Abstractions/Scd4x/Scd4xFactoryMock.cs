using System.Device.I2c;
using Microsoft.Extensions.Logging;

namespace Iot.Device.Scd4x
{
    public class Scd4xFactoryMock : IScd4xFactory
    {
        private readonly ILogger logger;

        public Scd4xFactoryMock(ILogger<Scd4xFactoryMock> logger)
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

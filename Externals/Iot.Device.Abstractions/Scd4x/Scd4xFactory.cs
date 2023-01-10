using System.Device.I2c;
using Microsoft.Extensions.Logging;

namespace Iot.Device.Scd4x
{
    public class Scd4xFactory : IScd4xFactory
    {
        private readonly ILogger logger;

        public Scd4xFactory(ILogger<Scd4xFactory> logger)
        {
            this.logger = logger;
        }

        public IScd4x Create(I2cConnectionSettings i2cSettings)
        {
            this.logger.LogDebug("Create");
            var i2cDevice = I2cDevice.Create(i2cSettings);
            return new Scd4xWrapper(i2cDevice);
        }
    }
}

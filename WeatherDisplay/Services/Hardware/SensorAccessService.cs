using System;
using System.Device.Devices;
using System.Device.I2c;
using System.Gpio.Devices.BMxx80;
using Microsoft.Extensions.Logging;
using UnitsNet;

namespace WeatherDisplay.Services.Hardware
{
    public class SensorAccessService : ISensorAccessService, IDisposable
    {
        private const int I2cBusId = 1;

        private readonly ILogger logger;
        private readonly ISensorFactory sensorFactory;

        private bool disposed;
        private bool initialized;

        public SensorAccessService(
            ILogger<SensorAccessService> logger,
            ISensorFactory sensorFactory)
        {

            this.logger = logger;
            this.sensorFactory = sensorFactory;
        }

        public void Initialize()
        {
            if (this.initialized)
            {
                this.logger.LogDebug($"Initialize: Already initialized");
                return;
            }

            this.logger.LogDebug($"Initialize");

            try
            {
                var i2cSettings = new I2cConnectionSettings(I2cBusId, System.Gpio.Devices.BMxx80.Bme680.SecondaryI2cAddress);
                var bme680 = this.sensorFactory.CreateBme680(i2cSettings, Temperature.FromDegreesCelsius(20.0));
                bme680.Reset();
                this.Bme680 = bme680;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Failed to initialize Bme680");
            }

            this.initialized = true;
        }

        public IBme680 Bme680 { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Bme680?.Dispose();
                    this.Bme680 = null;

                    this.initialized = false;
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

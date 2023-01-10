using System;
using System.Device.I2c;
using Iot.Device.Bmxx80;
using Iot.Device.Scd4x;
using Microsoft.Extensions.Logging;
using UnitsNet;

namespace WeatherDisplay.Services.Hardware
{
    public class SensorAccessService : ISensorAccessService, IDisposable
    {
        private const int I2cBusId = 1;

        private readonly ILogger logger;
        private readonly IBme680Factory bme680Factory;
        private readonly IScd4xFactory scd4xFactory;
        private bool disposed;
        private bool initialized;

        public SensorAccessService(
            ILogger<SensorAccessService> logger,
            IBme680Factory bme680Factory,
            IScd4xFactory scd4xFactory)
        {

            this.logger = logger;
            this.bme680Factory = bme680Factory;
            this.scd4xFactory = scd4xFactory;
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
                var i2cSettings = new I2cConnectionSettings(I2cBusId, Iot.Device.Bmxx80.Bme680.SecondaryI2cAddress);
                var bme680 = this.bme680Factory.Create(i2cSettings, Temperature.FromDegreesCelsius(20.0));
                bme680.Reset();
                this.Bme680 = bme680;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Failed to initialize Bme680");
            }
            
            try
            {
                var i2cSettings = new I2cConnectionSettings(I2cBusId, Iot.Device.Scd4x.Scd4x.DefaultI2cAddress);
                var scd41 = this.scd4xFactory.Create(i2cSettings);
                scd41.Reset();
                this.Scd41 = scd41;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Failed to initialize Scd41");
            }

            this.initialized = true;
        }

        public IBme680 Bme680 { get; private set; }
        
        public IScd4x Scd41 { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Bme680?.Dispose();
                    this.Bme680 = null;
                    
                    this.Scd41?.Dispose();
                    this.Scd41 = null;

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

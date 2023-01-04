// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Device.I2c;
using System.Gpio.Devices.BMxx80.PowerMode;
using System.Gpio.Devices.BMxx80.ReadResult;
using System.Threading;
using System.Threading.Tasks;

namespace System.Gpio.Devices.BMxx80
{
    /// <summary>
    /// Represents a BME280 temperature and barometric pressure sensor.
    /// </summary>
    public class Bmp280 : Bmx280Base
    {
        /// <summary>
        /// The expected chip ID of the BMP280.
        /// </summary>
        private const byte DeviceId = 0x58;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bmp280"/> class.
        /// </summary>
        /// <param name="i2cDevice">The <see cref="I2cDevice"/> to create with.</param>
        public Bmp280(I2cDevice i2cDevice)
            : base(DeviceId, i2cDevice)
        {
            this.communicationProtocol = CommunicationProtocol.I2c;
        }

        /// <summary>
        /// Performs a synchronous reading.
        /// </summary>
        /// <returns><see cref="Bmp280ReadResult"/></returns>
        public Bmp280ReadResult Read()
        {
            if (this.ReadPowerMode() != Bmx280PowerMode.Normal)
            {
                this.SetPowerMode(Bmx280PowerMode.Forced);
                Thread.Sleep(this.GetMeasurementDuration());
            }

            var tempSuccess = this.TryReadTemperatureCore(out var temperature);
            var pressSuccess = this.TryReadPressureCore(out var pressure, skipTempFineRead: true);

            return new Bmp280ReadResult(tempSuccess ? temperature : null, pressSuccess ? pressure : null);
        }

        /// <summary>
        /// Performs an asynchronous reading.
        /// </summary>
        /// <returns><see cref="Bmp280ReadResult"/></returns>
        public async Task<Bmp280ReadResult> ReadAsync()
        {
            if (this.ReadPowerMode() != Bmx280PowerMode.Normal)
            {
                this.SetPowerMode(Bmx280PowerMode.Forced);
                await Task.Delay(this.GetMeasurementDuration());
            }

            var tempSuccess = this.TryReadTemperatureCore(out var temperature);
            var pressSuccess = this.TryReadPressureCore(out var pressure, skipTempFineRead: true);

            return new Bmp280ReadResult(tempSuccess ? temperature : null, pressSuccess ? pressure : null);
        }
    }
}

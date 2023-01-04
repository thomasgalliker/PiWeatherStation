// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Device.I2c;
using System.Gpio.Devices.BMxx80.CalibrationData;
using System.Gpio.Devices.BMxx80.PowerMode;
using System.Gpio.Devices.BMxx80.ReadResult;
using System.Gpio.Devices.BMxx80.Register;
using System.Threading;
using System.Threading.Tasks;
using UnitsNet;

namespace System.Gpio.Devices.BMxx80
{
    /// <summary>
    /// Represents a BME280 temperature, barometric pressure and humidity sensor.
    /// </summary>
    public class Bme280 : Bmx280Base
    {
        /// <summary>
        /// The expected chip ID of the BME280.
        /// </summary>
        private const byte DeviceId = 0x60;

        /// <summary>
        /// Calibration data for the <see cref="Bme680"/>.
        /// </summary>
        private readonly Bme280CalibrationData bme280Calibration;

        private Sampling humiditySampling;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bme280"/> class.
        /// </summary>
        /// <param name="i2cDevice">The <see cref="I2cDevice"/> to create with.</param>
        public Bme280(I2cDevice i2cDevice)
            : base(DeviceId, i2cDevice)
        {
            this.bme280Calibration = (Bme280CalibrationData)this.calibrationData;
            this.communicationProtocol = CommunicationProtocol.I2c;
        }

        /// <summary>
        /// Gets or sets the humidity sampling.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="Sampling"/> is set to an undefined mode.</exception>
        public unsafe Sampling HumiditySampling
        {
            get => this.humiditySampling;
            set
            {
                if (!Enum.IsDefined(typeof(Sampling), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                var status = this.Read8BitsFromRegister((byte)Bme280Register.CTRL_HUM);
                status = (byte)(status & 0b_1111_1000);
                status = (byte)(status | (byte)value);

                Span<byte> command = stackalloc[]
                {
                    (byte)Bme280Register.CTRL_HUM,
                    status
                };
                this.i2cDevice.Write(command);

                // Changes to the above register only become effective after a write operation to "CTRL_MEAS".
                var measureState = this.Read8BitsFromRegister((byte)Bmx280Register.CTRL_MEAS);

                Span<byte> command2 = stackalloc[]
                {
                    (byte)Bmx280Register.CTRL_MEAS,
                    measureState
                };
                this.i2cDevice.Write(command2);
                this.humiditySampling = value;
            }
        }

        /// <summary>
        /// Reads the humidity. A return value indicates whether the reading succeeded.
        /// </summary>
        /// <param name="humidity">
        /// Contains the measured humidity as %rH if the <see cref="HumiditySampling"/> was not set to <see cref="Sampling.Skipped"/>.
        /// Contains an undefined value if the return value is false.
        /// </param>
        /// <returns><code>true</code> if measurement was not skipped, otherwise <code>false</code>.</returns>
        public bool TryReadHumidity(out RelativeHumidity humidity) => this.TryReadHumidityCore(out humidity);

        /// <summary>
        /// Gets the required time in ms to perform a measurement with the current sampling modes.
        /// </summary>
        /// <returns>The time it takes for the chip to read data in milliseconds rounded up.</returns>
        public override int GetMeasurementDuration()
        {
            return s_osToMeasCycles[(int)this.PressureSampling] + s_osToMeasCycles[(int)this.TemperatureSampling] + s_osToMeasCycles[(int)this.HumiditySampling];
        }

        /// <summary>
        /// Performs a synchronous reading.
        /// </summary>
        /// <returns><see cref="Bme280ReadResult"/></returns>
        public Bme280ReadResult Read()
        {
            if (this.ReadPowerMode() != Bmx280PowerMode.Normal)
            {
                this.SetPowerMode(Bmx280PowerMode.Forced);
                Thread.Sleep(this.GetMeasurementDuration());
            }

            var tempSuccess = this.TryReadTemperatureCore(out var temperature);
            var pressSuccess = this.TryReadPressureCore(out var pressure, skipTempFineRead: true);
            var humiditySuccess = this.TryReadHumidityCore(out var humidity, skipTempFineRead: true);

            return new Bme280ReadResult(tempSuccess ? temperature : null, pressSuccess ? pressure : null, humiditySuccess ? humidity : null);
        }

        /// <summary>
        /// Performs an asynchronous reading.
        /// </summary>
        /// <returns><see cref="Bme280ReadResult"/></returns>
        public async Task<Bme280ReadResult> ReadAsync()
        {
            if (this.ReadPowerMode() != Bmx280PowerMode.Normal)
            {
                this.SetPowerMode(Bmx280PowerMode.Forced);
                await Task.Delay(this.GetMeasurementDuration());
            }

            var tempSuccess = this.TryReadTemperatureCore(out var temperature);
            var pressSuccess = this.TryReadPressureCore(out var pressure, skipTempFineRead: true);
            var humiditySuccess = this.TryReadHumidityCore(out var humidity, skipTempFineRead: true);

            return new Bme280ReadResult(tempSuccess ? temperature : null, pressSuccess ? pressure : null, humiditySuccess ? humidity : null);
        }

        /// <summary>
        /// Sets the default configuration for the sensor.
        /// </summary>
        protected override void SetDefaultConfiguration()
        {
            base.SetDefaultConfiguration();
            this.HumiditySampling = Sampling.UltraLowPower;
        }

        /// <summary>
        /// Compensates the humidity.
        /// </summary>
        /// <param name="adcHumidity">The humidity value read from the device.</param>
        /// <returns>The relative humidity.</returns>
        private RelativeHumidity CompensateHumidity(int adcHumidity)
        {
            // The humidity is calculated using the compensation formula in the BME280 datasheet.
            var varH = this.TemperatureFine - 76800.0;
            varH = (adcHumidity - ((this.bme280Calibration.DigH4 * 64.0) + (this.bme280Calibration.DigH5 / 16384.0 * varH))) *
                (this.bme280Calibration.DigH2 / 65536.0 * (1.0 + (this.bme280Calibration.DigH6 / 67108864.0 * varH *
                                               (1.0 + (this.bme280Calibration.DigH3 / 67108864.0 * varH)))));
            varH *= 1.0 - (this.bme280Calibration.DigH1 * varH / 524288.0);

            if (varH > 100)
            {
                varH = 100;
            }
            else if (varH < 0)
            {
                varH = 0;
            }

            return RelativeHumidity.FromPercent(varH);
        }

        private bool TryReadHumidityCore(out RelativeHumidity humidity, bool skipTempFineRead = false)
        {
            if (this.HumiditySampling == Sampling.Skipped)
            {
                humidity = default;
                return false;
            }

            if (!skipTempFineRead)
            {
                this.TryReadTemperature(out _);
            }

            // Read the temperature first to load the t_fine value for compensation.
            var hum = this.Read16BitsFromRegister((byte)Bme280Register.HUMIDDATA, Endianness.BigEndian);

            humidity = this.CompensateHumidity(hum);
            return true;
        }
    }
}

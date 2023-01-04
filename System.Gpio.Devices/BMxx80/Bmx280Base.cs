// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

// Ported from https://github.com/adafruit/Adafruit_BMP280_Library/blob/master/Adafruit_BMP280.cpp
// Formulas and code examples can also be found in the datasheet http://www.adafruit.com/datasheets/BST-BMP280-DS001-11.pdf
using System.Device.I2c;
using System.Gpio.Devices.BMxx80.FilteringMode;
using System.Gpio.Devices.BMxx80.PowerMode;
using System.Gpio.Devices.BMxx80.Register;
using System.Gpio.Devices.Utils;
using System.IO;
using UnitsNet;

namespace System.Gpio.Devices.BMxx80
{
    /// <summary>
    /// Represents the core functionality of the Bmx280 family.
    /// </summary>
    public abstract class Bmx280Base : Bmxx80Base
    {
        /// <summary>
        /// Default I2C bus address.
        /// </summary>
        public const byte DefaultI2cAddress = 0x77;

        /// <summary>
        /// Secondary I2C bus address.
        /// </summary>
        public const byte SecondaryI2cAddress = 0x76;

        /// <summary>
        /// Converts oversampling to needed measurement cycles for that oversampling.
        /// </summary>
        protected static readonly int[] s_osToMeasCycles = { 0, 7, 9, 14, 23, 44 };

        private Bmx280FilteringMode filteringMode;
        private StandbyTime standbyTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bmx280Base"/> class.
        /// </summary>
        /// <param name="deviceId">The ID of the device.</param>
        /// <param name="i2cDevice">The <see cref="I2cDevice"/> to create with.</param>
        protected Bmx280Base(byte deviceId, I2cDevice i2cDevice)
            : base(deviceId, i2cDevice)
        {
        }

        /// <summary>
        /// Gets or sets the IIR filter mode.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="Bmx280FilteringMode"/> is set to an undefined mode.</exception>
        public Bmx280FilteringMode FilterMode
        {
            get => this.filteringMode;
            set
            {
                var current = this.Read8BitsFromRegister((byte)Bmx280Register.CONFIG);
                current = (byte)((current & 0b_1110_0011) | ((byte)value << 2));

                Span<byte> command = stackalloc[]
                {
                    (byte)Bmx280Register.CONFIG,
                    current
                };
                this.i2cDevice.Write(command);
                this.filteringMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the standby time between two consecutive measurements.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="Bmxx80.StandbyTime"/> is set to an undefined mode.</exception>
        public StandbyTime StandbyTime
        {
            get => this.standbyTime;
            set
            {
                var current = this.Read8BitsFromRegister((byte)Bmx280Register.CONFIG);
                current = (byte)((current & 0b_0001_1111) | ((byte)value << 5));

                Span<byte> command = stackalloc[]
                {
                    (byte)Bmx280Register.CONFIG,
                    current
                };
                this.i2cDevice.Write(command);
                this.standbyTime = value;
            }
        }

        /// <summary>
        /// Reads the temperature. A return value indicates whether the reading succeeded.
        /// </summary>
        /// <param name="temperature">
        /// Contains the measured temperature if the <see cref="Bmxx80Base.TemperatureSampling"/> was not set to <see cref="Sampling.Skipped"/>.
        /// Contains <see cref="double.NaN"/> otherwise.
        /// </param>
        /// <returns><code>true</code> if measurement was not skipped, otherwise <code>false</code>.</returns>
        public override bool TryReadTemperature(out Temperature temperature) => this.TryReadTemperatureCore(out temperature);

        /// <summary>
        /// Read the <see cref="Bmx280PowerMode"/> state.
        /// </summary>
        /// <returns>The current <see cref="Bmx280PowerMode"/>.</returns>
        /// <exception cref="NotImplementedException">Thrown when the power mode does not match a defined mode in <see cref="Bmx280PowerMode"/>.</exception>
        public Bmx280PowerMode ReadPowerMode()
        {
            var read = this.Read8BitsFromRegister(this.controlRegister);

            // Get only the power mode bits.
            var powerMode = (byte)(read & 0b_0000_0011);

            if (Enum.IsDefined(typeof(Bmx280PowerMode), powerMode) == false)
            {
                throw new IOException("Read unexpected power mode");
            }

            return (Bmx280PowerMode)powerMode;
        }

        /// <summary>
        /// Reads the pressure. A return value indicates whether the reading succeeded.
        /// </summary>
        /// <param name="pressure">
        /// Contains the measured pressure in Pa if the <see cref="Bmxx80Base.PressureSampling"/> was not set to <see cref="Sampling.Skipped"/>.
        /// Contains <see cref="double.NaN"/> otherwise.
        /// </param>
        /// <returns><code>true</code> if measurement was not skipped, otherwise <code>false</code>.</returns>
        public override bool TryReadPressure(out Pressure pressure) => this.TryReadPressureCore(out pressure);

        /// <summary>
        /// Calculates the altitude in meters from the specified sea-level pressure(in hPa).
        /// </summary>
        /// <param name="seaLevelPressure">Sea-level pressure</param>
        /// <param name="altitude">
        /// Contains the calculated metres above sea-level if the <see cref="Bmxx80Base.PressureSampling"/> was not set to <see cref="Sampling.Skipped"/>.
        /// Contains <see cref="double.NaN"/> otherwise.
        /// </param>
        /// <returns><code>true</code> if pressure measurement was not skipped, otherwise <code>false</code>.</returns>
        public bool TryReadAltitude(Pressure seaLevelPressure, out Length altitude)
        {
            // Read the pressure first.
            var success = this.TryReadPressureCore(out var pressure);
            if (!success)
            {
                altitude = default;
                return false;
            }

            // Then read the temperature.
            success = this.TryReadTemperatureCore(out var temperature);
            if (!success)
            {
                altitude = default;
                return false;
            }

            // Calculate and return the altitude using the hypsometric formula.
            altitude = WeatherHelper.CalculateAltitude(pressure, seaLevelPressure, temperature);
            return true;
        }

        /// <summary>
        /// Calculates the altitude in meters from the mean sea-level pressure.
        /// </summary>
        /// <param name="altitude">
        /// Contains the calculated metres above sea-level if the <see cref="Bmxx80Base.PressureSampling"/> was not set to <see cref="Sampling.Skipped"/>.
        /// Contains <see cref="double.NaN"/> otherwise.
        /// </param>
        /// <returns><code>true</code> if pressure measurement was not skipped, otherwise <code>false</code>.</returns>
        public bool TryReadAltitude(out Length altitude) => this.TryReadAltitude(WeatherHelper.MeanSeaLevel, out altitude);

        /// <summary>
        /// Get the current status of the device.
        /// </summary>
        /// <returns>The <see cref="DeviceStatus"/>.</returns>
        public DeviceStatus ReadStatus()
        {
            var status = this.Read8BitsFromRegister((byte)Bmx280Register.STATUS);

            // Bit 3.
            var measuring = ((status >> 3) & 1) == 1;

            // Bit 0.
            var imageUpdating = (status & 1) == 1;

            return new DeviceStatus
            {
                ImageUpdating = imageUpdating,
                Measuring = measuring
            };
        }

        /// <summary>
        /// Sets the power mode to the given mode
        /// </summary>
        /// <param name="powerMode">The <see cref="Bmx280PowerMode"/> to set.</param>
        public void SetPowerMode(Bmx280PowerMode powerMode)
        {
            var read = this.Read8BitsFromRegister(this.controlRegister);

            // Clear last 2 bits.
            var cleared = (byte)(read & 0b_1111_1100);

            Span<byte> command = stackalloc[]
            {
                this.controlRegister,
                (byte)(cleared | (byte)powerMode)
            };
            this.i2cDevice.Write(command);
        }

        /// <summary>
        /// Gets the required time in ms to perform a measurement with the current sampling modes.
        /// </summary>
        /// <returns>The time it takes for the chip to read data in milliseconds rounded up.</returns>
        public virtual int GetMeasurementDuration() => s_osToMeasCycles[(int)this.PressureSampling] + s_osToMeasCycles[(int)this.TemperatureSampling];

        /// <summary>
        /// Sets the default configuration for the sensor.
        /// </summary>
        protected override void SetDefaultConfiguration()
        {
            base.SetDefaultConfiguration();
            this.FilterMode = Bmx280FilteringMode.Off;
            this.StandbyTime = StandbyTime.Ms125;
        }

        /// <summary>
        /// Performs a temperature reading.
        /// </summary>
        /// <returns><see cref="Temperature"/></returns>
        protected bool TryReadTemperatureCore(out Temperature temperature)
        {
            if (this.TemperatureSampling == Sampling.Skipped)
            {
                temperature = default;
                return false;
            }

            var temp = (int)this.Read24BitsFromRegister((byte)Bmx280Register.TEMPDATA_MSB, Endianness.BigEndian);

            temperature = this.CompensateTemperature(temp >> 4);
            return true;
        }

        /// <summary>
        /// Performs a pressure reading.
        /// </summary>
        /// <returns><see cref="Pressure"/></returns>
        protected bool TryReadPressureCore(out Pressure pressure, bool skipTempFineRead = false)
        {
            if (this.PressureSampling == Sampling.Skipped)
            {
                pressure = default;
                return false;
            }

            if (!skipTempFineRead)
            {
                this.TryReadTemperatureCore(out _);
            }

            // Read pressure data.
            var press = (int)this.Read24BitsFromRegister((byte)Bmx280Register.PRESSUREDATA, Endianness.BigEndian);

            // Convert the raw value to the pressure in Pa.
            pressure = this.CompensatePressure(press >> 4);

            return true;
        }

        /// <summary>
        /// Compensates the pressure in Pa, in double format
        /// </summary>
        /// <param name="adcPressure">The pressure value read from the device.</param>
        /// <returns>Pressure as an instance of <see cref="Pressure"/>.</returns>
        private Pressure CompensatePressure(long adcPressure)
        {
            // Formula from the datasheet http://www.adafruit.com/datasheets/BST-BMP280-DS001-11.pdf
            // This uses the recommended approach with floating point math
            double var1, var2, p;
            var1 = (this.TemperatureFine / 2.0) - 64000.0;
            var2 = var1 * var1 * this.calibrationData.DigP6 / 32768.0;
            var2 += var1 * this.calibrationData.DigP5 * 2.0;
            var2 = (var2 / 4.0) + (this.calibrationData.DigP4 * 65536.0);
            var1 = ((this.calibrationData.DigP3 * var1 * var1 / 524288.0) + (this.calibrationData.DigP2 * var1)) / 524288.0;
            var1 = (1.0 + (var1 / 32768.0)) * this.calibrationData.DigP1;
            if (var1 == 0.0)
            {
                return Pressure.FromPascals(0); // Avoid exception caused by division by zero
            }

            p = 1048576.0 - adcPressure;
            p = (p - (var2 / 4096.0)) * 6250.0 / var1;
            var1 = this.calibrationData.DigP9 * p * p / 2147483648.0;
            var2 = p * this.calibrationData.DigP8 / 32768.0;
            p += (var1 + var2 + this.calibrationData.DigP7) / 16.0;

            return Pressure.FromPascals(p);
        }
    }
}

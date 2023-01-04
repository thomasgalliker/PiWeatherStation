// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Buffers.Binary;
using System.Device.I2c;
using System.Gpio.Devices.BMxx80.CalibrationData;
using System.Gpio.Devices.BMxx80.Register;
using System.IO;
using UnitsNet;

namespace System.Gpio.Devices.BMxx80
{
    /// <summary>
    /// Represents the core functionality of the Bmxx80 family.
    /// </summary>
    public abstract class Bmxx80Base : IDisposable
    {
        /// <summary>
        /// Calibration data for the sensor.
        /// </summary>
        internal Bmxx80CalibrationData calibrationData;

        /// <summary>
        /// I2C device used to communicate with the device.
        /// </summary>
        protected I2cDevice i2cDevice;

        /// <summary>
        /// Chosen communication protocol.
        /// </summary>
        protected CommunicationProtocol communicationProtocol;

        /// <summary>
        /// The control register of the sensor.
        /// </summary>
        protected byte controlRegister;

        /// <summary>
        /// Bmxx80 communication protocol.
        /// </summary>
        public enum CommunicationProtocol
        {
            /// <summary>
            /// I²C communication protocol.
            /// </summary>
            I2c
        }

        /// <summary>
        /// The variable TemperatureFine carries a fine resolution temperature value over to the
        /// pressure compensation formula and could be implemented as a global variable.
        /// </summary>
        protected double TemperatureFine { get; set; }

        /// <summary>
        /// The temperature calibration factor.
        /// </summary>
        protected virtual int TempCalibrationFactor => 1;

        private Sampling temperatureSampling;
        private Sampling pressureSampling;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bmxx80Base"/> class.
        /// </summary>
        /// <param name="deviceId">The ID of the device.</param>
        /// <param name="i2cDevice">The <see cref="Device.I2c.I2cDevice"/> to create with.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given <see cref="Device.I2c.I2cDevice"/> is null.</exception>
        /// <exception cref="IOException">Thrown when the device cannot be found on the bus.</exception>
        protected Bmxx80Base(byte deviceId, I2cDevice i2cDevice)
        {
            this.i2cDevice = i2cDevice ?? throw new ArgumentNullException(nameof(i2cDevice));
            this.i2cDevice.WriteByte((byte)Bmxx80Register.CHIPID);

            var readSignature = this.i2cDevice.ReadByte();

            if (readSignature != deviceId)
            {
                throw new IOException($"Unable to find a chip with id {deviceId}. Found one with id {readSignature}");
            }

            this.ReadCalibrationData();
            this.Reset();
#if !NET5_0_OR_GREATER
            if (this.calibrationData is null)
            {
                throw new Exception("BMxx80 device is not correctly configured.");
            }
#endif
        }

        /// <summary>
        /// Gets or sets the pressure sampling.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="Sampling"/> is set to an undefined mode.</exception>
        public Sampling PressureSampling
        {
            get => this.pressureSampling;
            set
            {
                var status = this.Read8BitsFromRegister(this.controlRegister);
                status = (byte)(status & 0b1110_0011);
                status = (byte)(status | ((byte)value << 2));

                Span<byte> command = stackalloc[]
                {
                    this.controlRegister,
                    status
                };
                this.i2cDevice.Write(command);
                this.pressureSampling = value;
            }
        }

        /// <summary>
        /// Gets or sets the temperature sampling.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="Sampling"/> is set to an undefined mode.</exception>
        public Sampling TemperatureSampling
        {
            get => this.temperatureSampling;
            set
            {
                var status = this.Read8BitsFromRegister(this.controlRegister);
                status = (byte)(status & 0b0001_1111);
                status = (byte)(status | ((byte)value << 5));

                Span<byte> command = stackalloc[]
                {
                    this.controlRegister,
                    status
                };
                this.i2cDevice.Write(command);
                this.temperatureSampling = value;
            }
        }

        /// <summary>
        /// When called, the device is reset using the complete power-on-reset procedure.
        /// The device will reset to the default configuration.
        /// </summary>
        public void Reset()
        {
            const byte resetCommand = 0xB6;
            Span<byte> command = stackalloc[]
            {
                (byte)Bmxx80Register.RESET,
                resetCommand
            };
            this.i2cDevice.Write(command);

            this.SetDefaultConfiguration();
        }

        /// <summary>
        /// Reads the temperature. A return value indicates whether the reading succeeded.
        /// </summary>
        /// <param name="temperature">
        /// Contains the measured temperature if the <see cref="TemperatureSampling"/> was not set to <see cref="Sampling.Skipped"/>.
        /// Contains <see cref="double.NaN"/> otherwise.
        /// </param>
        /// <returns><code>true</code> if measurement was not skipped, otherwise <code>false</code>.</returns>
        public abstract bool TryReadTemperature(out Temperature temperature);

        /// <summary>
        /// Reads the pressure. A return value indicates whether the reading succeeded.
        /// </summary>
        /// <param name="pressure">
        /// Contains the measured pressure if the <see cref="PressureSampling"/> was not set to <see cref="Sampling.Skipped"/>.
        /// Contains <see cref="double.NaN"/> otherwise.
        /// </param>
        /// <returns><code>true</code> if measurement was not skipped, otherwise <code>false</code>.</returns>
        public abstract bool TryReadPressure(out Pressure pressure);

        /// <summary>
        /// Compensates the temperature.
        /// </summary>
        /// <param name="adcTemperature">The temperature value read from the device.</param>
        /// <returns>The <see cref="Temperature"/>.</returns>
        protected Temperature CompensateTemperature(int adcTemperature)
        {
            // The temperature is calculated using the compensation formula in the BMP280 datasheet.
            // See: https://cdn-shop.adafruit.com/datasheets/BST-BMP280-DS001-11.pdf
            var var1 = ((adcTemperature / 16384.0) - (this.calibrationData.DigT1 / 1024.0)) * this.calibrationData.DigT2;
            var var2 = (adcTemperature / 131072.0) - (this.calibrationData.DigT1 / 8192.0);
            var2 *= var2 * this.calibrationData.DigT3 * this.TempCalibrationFactor;

            this.TemperatureFine = var1 + var2;

            var temp = (var1 + var2) / 5120.0;
            return Temperature.FromDegreesCelsius(temp);
        }

        /// <summary>
        /// Reads an 8 bit value from a register.
        /// </summary>
        /// <param name="register">Register to read from.</param>
        /// <returns>Value from register.</returns>
        protected internal byte Read8BitsFromRegister(byte register)
        {
            if (this.communicationProtocol == CommunicationProtocol.I2c)
            {
                this.i2cDevice.WriteByte(register);
                var value = this.i2cDevice.ReadByte();
                return value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Reads a 16 bit value over I2C.
        /// </summary>
        /// <param name="register">Register to read from.</param>
        /// <param name="endianness">Interpretation of the bytes (big or little endian).</param>
        /// <returns>Value from register.</returns>
        protected internal ushort Read16BitsFromRegister(byte register, Endianness endianness = Endianness.LittleEndian)
        {
            Span<byte> bytes = stackalloc byte[2];
            switch (this.communicationProtocol)
            {
                case CommunicationProtocol.I2c:
                    this.i2cDevice.WriteByte(register);
                    this.i2cDevice.Read(bytes);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return endianness switch
            {
                Endianness.LittleEndian => BinaryPrimitives.ReadUInt16LittleEndian(bytes),
                Endianness.BigEndian => BinaryPrimitives.ReadUInt16BigEndian(bytes),
                _ => throw new ArgumentOutOfRangeException(nameof(endianness), endianness, null)
            };
        }

        /// <summary>
        /// Reads a 24 bit value over I2C.
        /// </summary>
        /// <param name="register">Register to read from.</param>
        /// <param name="endianness">Interpretation of the bytes (big or little endian).</param>
        /// <returns>Value from register.</returns>
        protected internal uint Read24BitsFromRegister(byte register, Endianness endianness = Endianness.LittleEndian)
        {
            Span<byte> bytes = stackalloc byte[4];
            switch (this.communicationProtocol)
            {
                case CommunicationProtocol.I2c:
                    this.i2cDevice.WriteByte(register);
                    this.i2cDevice.Read(bytes.Slice(1));
                    break;
                default:
                    throw new NotImplementedException();
            }

            return endianness switch
            {
                Endianness.LittleEndian => BinaryPrimitives.ReadUInt32LittleEndian(bytes),
                Endianness.BigEndian => BinaryPrimitives.ReadUInt32BigEndian(bytes),
                _ => throw new ArgumentOutOfRangeException(nameof(endianness), endianness, null)
            };
        }

        /// <summary>
        /// Converts byte to <see cref="Sampling"/>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns><see cref="Sampling"/></returns>
        protected Sampling ByteToSampling(byte value)
        {
            // Values >=5 equals UltraHighResolution.
            if (value >= 5)
            {
                return Sampling.UltraHighResolution;
            }

            return (Sampling)value;
        }

        /// <summary>
        /// Sets the default configuration for the sensor.
        /// </summary>
        protected virtual void SetDefaultConfiguration()
        {
            this.PressureSampling = Sampling.UltraLowPower;
            this.TemperatureSampling = Sampling.UltraLowPower;
        }

        /// <summary>
        /// Specifies the Endianness of a device.
        /// </summary>
        protected internal enum Endianness
        {
            /// <summary>
            /// Indicates little endian.
            /// </summary>
            LittleEndian,

            /// <summary>
            /// Indicates big endian.
            /// </summary>
            BigEndian
        }

#if NET5_0_OR_GREATER
        [MemberNotNull(nameof(_calibrationData))]
#endif
        private void ReadCalibrationData()
        {
            switch (this)
            {
                case Bme280 _:
                    this.calibrationData = new Bme280CalibrationData();
                    this.controlRegister = (byte)Bmx280Register.CTRL_MEAS;
                    break;
                case Bmp280 _:
                    this.calibrationData = new Bmp280CalibrationData();
                    this.controlRegister = (byte)Bmx280Register.CTRL_MEAS;
                    break;
                case Bme680 _:
                    this.calibrationData = new Bme680CalibrationData();
                    this.controlRegister = (byte)Bme680Register.CTRL_MEAS;
                    break;
                default:
                    throw new Exception("Bmxx80 device not correctly configured. Could not find calibration data.");
            }

            this.calibrationData.ReadFromDevice(this);
        }

        /// <summary>
        /// Cleanup.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the Bmxx80 and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.i2cDevice?.Dispose();
            this.i2cDevice = null!;
        }
    }
}

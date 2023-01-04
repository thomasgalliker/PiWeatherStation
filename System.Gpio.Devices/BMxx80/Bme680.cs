// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Device.Devices;
using System.Device.I2c;
using System.Gpio.Devices.BMxx80.CalibrationData;
using System.Gpio.Devices.BMxx80.FilteringMode;
using System.Gpio.Devices.BMxx80.PowerMode;
using System.Gpio.Devices.BMxx80.ReadResult;
using System.Gpio.Devices.BMxx80.Register;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnitsNet;

namespace System.Gpio.Devices.BMxx80
{
    /// <summary>
    /// Represents a BME680 temperature, pressure, relative humidity and VOC gas sensor.
    /// </summary>
    public class Bme680 : Bmxx80Base
    {
        private static readonly Temperature DefaultAmbientTemperature = Temperature.FromDegreesCelsius(20);
        private static readonly byte[] s_osToMeasCycles = { 0, 1, 2, 4, 8, 16 };
        private static readonly byte[] s_osToSwitchCount = { 0, 1, 1, 1, 1, 1 };
        private static readonly double[] s_k1Lookup = { 0.0, 0.0, 0.0, 0.0, 0.0, -1.0, 0.0, -0.8, 0.0, 0.0, -0.2, -0.5, 0.0, -1.0, 0.0, 0.0 };
        private static readonly double[] s_k2Lookup = { 0.0, 0.0, 0.0, 0.0, 0.1, 0.7, 0.0, -0.8, -0.1, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };

        private readonly Temperature ambientTemperatureUserDefault;

        /// <summary>
        /// Default I2C bus address.
        /// </summary>
        public const byte DefaultI2cAddress = 0x76;

        /// <summary>
        /// Secondary I2C bus address.
        /// </summary>
        public const byte SecondaryI2cAddress = 0x77;

        /// <summary>
        /// The expected chip ID of the BME680.
        /// </summary>
        private const byte DeviceId = 0x61;

        /// <summary>
        /// Calibration data for the <see cref="Bme680"/>.
        /// </summary>
        private Bme680CalibrationData bme680Calibration;

        /// <inheritdoc/>
        protected override int TempCalibrationFactor => 16;

        private readonly List<Bme680HeaterProfileConfig> heaterConfigs = new List<Bme680HeaterProfileConfig>();
        private bool gasConversionIsEnabled;
        private bool heaterIsEnabled;

        private Bme680HeaterProfile heaterProfile;
        private Bme680FilteringMode filterMode;
        private Sampling humiditySampling;

        /// <summary>
        /// Initialize a new instance of the <see cref="Bme680"/> class.
        /// </summary>
        /// <param name="i2cDevice">The <see cref="I2cDevice"/> to create with.</param>
        /// <param name="ambientTemperatureDefault">Assumed ambient temperature for startup. Used for initialization of the gas measurement
        /// if the temperature cannot be read during a reset.</param>
        public Bme680(I2cDevice i2cDevice, Temperature ambientTemperatureDefault)
            : base(DeviceId, i2cDevice)
        {
            this.ambientTemperatureUserDefault = ambientTemperatureDefault;
            this.communicationProtocol = CommunicationProtocol.I2c;
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Bme680"/> class.
        /// </summary>
        /// <param name="i2cDevice">The <see cref="I2cDevice"/> to create with.</param>
        public Bme680(I2cDevice i2cDevice)
            : this(i2cDevice, DefaultAmbientTemperature)
        {
        }

        /// <summary>
        /// Gets or sets the humidity sampling.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="Sampling"/> is set to an undefined mode.</exception>
        public Sampling HumiditySampling
        {
            get => this.humiditySampling;
            set
            {
                if (!Enum.IsDefined(typeof(Sampling), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                var status = this.Read8BitsFromRegister((byte)Bme680Register.CTRL_HUM);
                status = (byte)((status & (byte)~Bme680Mask.HUMIDITY_SAMPLING) | (byte)value);

                Span<byte> command = stackalloc[]
                {
                    (byte)Bme680Register.CTRL_HUM,
                    status
                };
                this.i2cDevice.Write(command);
                this.humiditySampling = value;
            }
        }

        /// <summary>
        /// Gets or sets the heater profile to be used for measurements.
        /// Current heater profile is only set if the chosen profile is configured.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="Bme680HeaterProfile"/> is set to an undefined profile.</exception>

        public Bme680HeaterProfile HeaterProfile
        {
            get => this.heaterProfile;
            set
            {
                if (this.heaterConfigs.Exists(config => config.HeaterProfile == value))
                {
                    if (!Enum.IsDefined(typeof(Bme680HeaterProfile), value))
                    {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }

                    var heaterProfile = this.Read8BitsFromRegister((byte)Bme680Register.CTRL_GAS_1);
                    heaterProfile = (byte)((heaterProfile & (byte)~Bme680Mask.NB_CONV) | (byte)value);

                    Span<byte> command = stackalloc[]
                    {
                        (byte)Bme680Register.CTRL_GAS_1,
                        heaterProfile
                    };
                    this.i2cDevice.Write(command);
                    this.heaterProfile = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the filtering mode to be used for measurements.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="Bme680FilteringMode"/> is set to an undefined mode.</exception>

        public Bme680FilteringMode FilterMode
        {
            get => this.filterMode;
            set
            {
                if (!Enum.IsDefined(typeof(Bme680FilteringMode), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                var filter = this.Read8BitsFromRegister((byte)Bme680Register.CONFIG);
                filter = (byte)((filter & (byte)~Bme680Mask.FILTER_COEFFICIENT) | ((byte)value << 2));

                Span<byte> command = stackalloc[]
                {
                    (byte)Bme680Register.CONFIG,
                    filter
                };
                this.i2cDevice.Write(command);
                this.filterMode = value;
            }
        }

        /// <summary>
        /// Gets or sets whether the heater is enabled.
        /// </summary>

        public bool HeaterIsEnabled
        {
            get => this.heaterIsEnabled;
            set
            {
                var heaterStatus = this.Read8BitsFromRegister((byte)Bme680Register.CTRL_GAS_0);
                heaterStatus = (byte)((heaterStatus & (byte)~Bme680Mask.HEAT_OFF) | (Convert.ToByte(!value) << 3));

                Span<byte> command = stackalloc[]
                {
                    (byte)Bme680Register.CTRL_GAS_0,
                    heaterStatus
                };
                this.i2cDevice.Write(command);
                this.heaterIsEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets whether gas conversions are enabled.
        /// </summary>

        public bool GasConversionIsEnabled
        {
            get => this.gasConversionIsEnabled;
            set
            {
                var gasConversion = this.Read8BitsFromRegister((byte)Bme680Register.CTRL_GAS_1);
                gasConversion = (byte)((gasConversion & (byte)~Bme680Mask.RUN_GAS) | (Convert.ToByte(value) << 4));

                Span<byte> command = stackalloc[]
                {
                    (byte)Bme680Register.CTRL_GAS_1,
                    gasConversion
                };
                this.i2cDevice.Write(command);
                this.gasConversionIsEnabled = value;
            }
        }

        /// <summary>
        /// Reads whether new data is available.
        /// </summary>
        public bool ReadNewDataIsAvailable()
        {
            var newData = this.Read8BitsFromRegister((byte)Bme680Register.STATUS);
            newData = (byte)(newData >> 7);

            return Convert.ToBoolean(newData);
        }

        /// <summary>
        /// Reads whether a gas measurement is in process.
        /// </summary>
        public bool ReadGasMeasurementInProcess()
        {
            var gasMeasInProcess = this.Read8BitsFromRegister((byte)Bme680Register.STATUS);
            gasMeasInProcess = (byte)((gasMeasInProcess & (byte)Bme680Mask.GAS_MEASURING) >> 6);

            return Convert.ToBoolean(gasMeasInProcess);
        }

        /// <summary>
        /// Reads whether a measurement of any kind is in process.
        /// </summary>
        public bool ReadMeasurementInProcess()
        {
            var measInProcess = this.Read8BitsFromRegister((byte)Bme680Register.STATUS);
            measInProcess = (byte)((measInProcess & (byte)Bme680Mask.MEASURING) >> 5);

            return Convert.ToBoolean(measInProcess);
        }

        /// <summary>
        /// Reads whether the target heater temperature is reached.
        /// </summary>
        public bool ReadHeaterIsStable()
        {
            var heaterStable = this.Read8BitsFromRegister((byte)Bme680Register.GAS_RANGE);
            heaterStable = (byte)((heaterStable & (byte)Bme680Mask.HEAT_STAB) >> 4);

            return Convert.ToBoolean(heaterStable);
        }

        /// <summary>
        /// Sets the power mode to the given mode
        /// </summary>
        /// <param name="powerMode">The <see cref="Bme680PowerMode"/> to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the power mode does not match a defined mode in <see cref="Bme680PowerMode"/>.</exception>
        public void SetPowerMode(Bme680PowerMode powerMode)
        {
            if (!Enum.IsDefined(typeof(Bme680PowerMode), powerMode))
            {
                throw new ArgumentOutOfRangeException(nameof(powerMode));
            }

            var status = this.Read8BitsFromRegister((byte)Bme680Register.CTRL_MEAS);
            status = (byte)((status & (byte)~Bme680Mask.PWR_MODE) | (byte)powerMode);

            Span<byte> command = stackalloc[]
            {
                (byte)Bme680Register.CTRL_MEAS,
                status
            };
            this.i2cDevice.Write(command);
        }

        /// <summary>
        /// Configures a heater profile, making it ready for use.
        /// </summary>
        /// <param name="profile">The <see cref="Bme680HeaterProfile"/> to configure.</param>
        /// <param name="targetTemperature">The target temperature. Ranging from 0-400.</param>
        /// <param name="duration">The measurement durations. Ranging from 0-4032ms.</param>
        /// <param name="ambientTemperature">The ambient temperature.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the heating profile does not match a defined profile in <see cref="Bme680HeaterProfile"/>.</exception>
        public void ConfigureHeatingProfile(Bme680HeaterProfile profile, Temperature targetTemperature, Duration duration, Temperature ambientTemperature)
        {
            if (!Enum.IsDefined(typeof(Bme680HeaterProfile), profile))
            {
                throw new ArgumentOutOfRangeException(nameof(profile));
            }

            // read ambient temperature for resistance calculation
            var heaterResistance = this.CalculateHeaterResistance(targetTemperature, ambientTemperature);
            var heaterDuration = this.CalculateHeaterDuration(duration);

            Span<byte> resistanceCommand = stackalloc[]
            {
                (byte)((byte)Bme680Register.RES_HEAT_0 + profile),
                heaterResistance
            };
            Span<byte> durationCommand = stackalloc[]
            {
                (byte)((byte)Bme680Register.GAS_WAIT_0 + profile),
                heaterDuration
            };
            this.i2cDevice.Write(resistanceCommand);
            this.i2cDevice.Write(durationCommand);

            // cache heater configuration
            if (this.heaterConfigs.Exists(config => config.HeaterProfile == profile))
            {
                this.heaterConfigs.Remove(this.heaterConfigs.Single(config => config.HeaterProfile == profile));
            }

            this.heaterConfigs.Add(new Bme680HeaterProfileConfig(profile, heaterResistance, duration));
        }

        /// <summary>
        /// Read the <see cref="Bme680PowerMode"/> state.
        /// </summary>
        /// <returns>The current <see cref="Bme680PowerMode"/>.</returns>
        public Bme680PowerMode ReadPowerMode()
        {
            var status = this.Read8BitsFromRegister((byte)Bme680Register.CTRL_MEAS);

            return (Bme680PowerMode)(status & (byte)Bme680Mask.PWR_MODE);
        }

        /// <summary>
        /// Gets the required time in ms to perform a measurement. The duration of the gas
        /// measurement is not considered if <see cref="GasConversionIsEnabled"/> is set to false
        /// or the chosen <see cref="Bme680HeaterProfile"/> is not configured.
        /// The precision of this duration is within 1ms of the actual measurement time.
        /// </summary>
        /// <param name="profile">The used <see cref="Bme680HeaterProfile"/>. </param>
        /// <returns></returns>
        public Duration GetMeasurementDuration(Bme680HeaterProfile profile)
        {
            var measCycles = s_osToMeasCycles[(int)this.TemperatureSampling];
            measCycles += s_osToMeasCycles[(int)this.PressureSampling];
            measCycles += s_osToMeasCycles[(int)this.HumiditySampling];

            var switchCount = s_osToSwitchCount[(int)this.TemperatureSampling];
            switchCount += s_osToSwitchCount[(int)this.PressureSampling];
            switchCount += s_osToSwitchCount[(int)this.HumiditySampling];

            double measDuration = measCycles * 1963;
            measDuration += 477 * switchCount;      // TPH switching duration

            if (this.GasConversionIsEnabled)
            {
                measDuration += 477 * 5;            // Gas measurement duration
            }

            measDuration += 500;                    // get it to the closest whole number
            measDuration /= 1000.0;                 // convert to ms
            measDuration += 1;                      // wake up duration of 1ms

            if (this.GasConversionIsEnabled && this.heaterConfigs.Exists(config => config.HeaterProfile == profile))
            {
                measDuration += this.heaterConfigs.Single(config => config.HeaterProfile == profile).HeaterDuration.Milliseconds;
            }

            return Duration.FromMilliseconds(Math.Ceiling(measDuration));
        }

        /// <summary>
        /// Performs a synchronous reading.
        /// </summary>
        /// <returns><see cref="Bme680ReadResult"/></returns>
        public Bme680ReadResult Read()
        {
            this.SetPowerMode(Bme680PowerMode.Forced);
            Thread.Sleep((int)this.GetMeasurementDuration(this.HeaterProfile).Milliseconds);

            var tempSuccess = this.TryReadTemperatureCore(out var temperature);
            var pressSuccess = this.TryReadPressureCore(out var pressure, skipTempFineRead: true);
            var humiditySuccess = this.TryReadHumidityCore(out var humidity, skipTempFineRead: true);
            var gasSuccess = this.TryReadGasResistanceCore(out var gasResistance);

            return new Bme680ReadResult(tempSuccess ? temperature : null, pressSuccess ? pressure : null, humiditySuccess ? humidity : null, gasSuccess ? gasResistance : null);
        }

        /// <summary>
        /// Performs an asynchronous reading.
        /// </summary>
        /// <returns><see cref="Bme680ReadResult"/></returns>
        public async Task<Bme680ReadResult> ReadAsync()
        {
            this.SetPowerMode(Bme680PowerMode.Forced);
            await Task.Delay((int)this.GetMeasurementDuration(this.HeaterProfile).Milliseconds);

            var tempSuccess = this.TryReadTemperatureCore(out var temperature);
            var pressSuccess = this.TryReadPressureCore(out var pressure, skipTempFineRead: true);
            var humiditySuccess = this.TryReadHumidityCore(out var humidity, skipTempFineRead: true);
            var gasSuccess = this.TryReadGasResistanceCore(out var gasResistance);

            return new Bme680ReadResult(tempSuccess ? temperature : null, pressSuccess ? pressure : null, humiditySuccess ? humidity : null, gasSuccess ? gasResistance : null);
        }

        /// <summary>
        /// Reads the humidity. A return value indicates whether the reading succeeded.
        /// </summary>
        /// <param name="humidity">
        /// Contains the measured humidity as %rH if the <see cref="HumiditySampling"/> was not set to <see cref="Sampling.Skipped"/>.
        /// Contains <see cref="double.NaN"/> otherwise.
        /// </param>
        /// <returns><code>true</code> if measurement was not skipped, otherwise <code>false</code>.</returns>
        public bool TryReadHumidity(out RelativeHumidity humidity) => this.TryReadHumidityCore(out humidity);

        /// <summary>
        /// Reads the pressure. A return value indicates whether the reading succeeded.
        /// </summary>
        /// <param name="pressure">
        /// Contains the measured pressure if the <see cref="Bmxx80Base.PressureSampling"/> was not set to <see cref="Sampling.Skipped"/>.
        /// Contains <see cref="double.NaN"/> otherwise.
        /// </param>
        /// <returns><code>true</code> if measurement was not skipped, otherwise <code>false</code>.</returns>
        public override bool TryReadPressure(out Pressure pressure) => this.TryReadPressureCore(out pressure);

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
        /// Reads the gas resistance. A return value indicates whether the reading succeeded.
        /// </summary>
        /// <param name="gasResistance">
        /// Contains the measured gas resistance if the heater module reached the target temperature and
        /// the measurement was valid. Undefined otherwise.
        /// </param>
        /// <returns><code>true</code> if measurement was not skipped, otherwise <code>false</code>.</returns>
        public bool TryReadGasResistance(out ElectricResistance gasResistance) => this.TryReadGasResistanceCore(out gasResistance);

        /// <summary>
        /// Sets the default configuration for the sensor.
        /// </summary>
        protected override void SetDefaultConfiguration()
        {
            base.SetDefaultConfiguration();
            this.HumiditySampling = Sampling.UltraLowPower;
            this.FilterMode = Bme680FilteringMode.C0;

            this.bme680Calibration = (Bme680CalibrationData)this.calibrationData;
            if (!this.TryReadTemperature(out var temp))
            {
                temp = this.ambientTemperatureUserDefault;
            }

            this.ConfigureHeatingProfile(Bme680HeaterProfile.Profile1, Temperature.FromDegreesCelsius(320), Duration.FromMilliseconds(150), temp);
            this.HeaterProfile = Bme680HeaterProfile.Profile1;

            this.HeaterIsEnabled = true;
            this.GasConversionIsEnabled = true;
        }

        /// <summary>
        /// Compensates the humidity.
        /// </summary>
        /// <param name="adcHumidity">The humidity value read from the device.</param>
        /// <returns>The percentage relative humidity.</returns>
        private RelativeHumidity CompensateHumidity(int adcHumidity)
        {
            if (this.bme680Calibration is null)
            {
                throw new Exception($"{nameof(Bme680)} is incorrectly configured.");
            }

            // Calculate the humidity.
            var temperature = this.TemperatureFine / 5120.0;
            var var1 = adcHumidity - ((this.bme680Calibration.DigH1 * 16.0) + (this.bme680Calibration.DigH3 / 2.0 * temperature));
            var var2 = var1 * (this.bme680Calibration.DigH2 / 262144.0 * (1.0 + (this.bme680Calibration.DigH4 / 16384.0 * temperature)
                + (this.bme680Calibration.DigH5 / 1048576.0 * temperature * temperature)));
            var var3 = this.bme680Calibration.DigH6 / 16384.0;
            var var4 = this.bme680Calibration.DigH7 / 2097152.0;
            var calculatedHumidity = var2 + ((var3 + (var4 * temperature)) * var2 * var2);

            if (calculatedHumidity > 100.0)
            {
                calculatedHumidity = 100.0;
            }
            else if (calculatedHumidity < 0.0)
            {
                calculatedHumidity = 0.0;
            }

            return RelativeHumidity.FromPercent(calculatedHumidity);
        }

        /// <summary>
        /// Compensates the pressure.
        /// </summary>
        /// <param name="adcPressure">The pressure value read from the device.</param>
        /// <returns>The measured pressure.</returns>
        private Pressure CompensatePressure(long adcPressure)
        {
            if (this.bme680Calibration is null)
            {
                throw new Exception($"{nameof(Bme680)} is incorrectly configured.");
            }

            // Calculate the pressure.
            var var1 = (this.TemperatureFine / 2.0) - 64000.0;
            var var2 = var1 * var1 * (this.bme680Calibration.DigP6 / 131072.0);
            var2 += var1 * this.bme680Calibration.DigP5 * 2.0;
            var2 = (var2 / 4.0) + (this.bme680Calibration.DigP4 * 65536.0);
            var1 = ((this.bme680Calibration.DigP3 * var1 * var1 / 16384.0) + (this.bme680Calibration.DigP2 * var1)) / 524288.0;
            var1 = (1.0 + (var1 / 32768.0)) * this.bme680Calibration.DigP1;
            var calculatedPressure = 1048576.0 - adcPressure;

            // Avoid exception caused by division by zero.
            if (var1 != 0)
            {
                calculatedPressure = (calculatedPressure - (var2 / 4096.0)) * 6250.0 / var1;
                var1 = this.bme680Calibration.DigP9 * calculatedPressure * calculatedPressure / 2147483648.0;
                var2 = calculatedPressure * (this.bme680Calibration.DigP8 / 32768.0);
                var var3 = calculatedPressure / 256.0 * (calculatedPressure / 256.0) * (calculatedPressure / 256.0)
                    * (this.bme680Calibration.DigP10 / 131072.0);
                calculatedPressure += (var1 + var2 + var3 + (this.bme680Calibration.DigP7 * 128.0)) / 16.0;
            }
            else
            {
                calculatedPressure = 0;
            }

            return Pressure.FromPascals(calculatedPressure);
        }

        private bool ReadGasMeasurementIsValid()
        {
            var gasMeasValid = this.Read8BitsFromRegister((byte)Bme680Register.GAS_RANGE);
            gasMeasValid = (byte)((gasMeasValid & (byte)Bme680Mask.GAS_VALID) >> 5);

            return Convert.ToBoolean(gasMeasValid);
        }

        private ElectricResistance CalculateGasResistance(ushort adcGasRes, byte gasRange)
        {
            if (this.bme680Calibration is null)
            {
                throw new Exception($"{nameof(Bme680)} is incorrectly configured.");
            }

            var var1 = 1340.0 + (5.0 * this.bme680Calibration.RangeSwErr);
            var var2 = var1 * (1.0 + (s_k1Lookup[gasRange] / 100.0));
            var var3 = 1.0 + (s_k2Lookup[gasRange] / 100.0);
            var gasResistance = 1.0 / (var3 * 0.000000125 * (1 << gasRange) * (((adcGasRes - 512.0) / var2) + 1.0));

            return ElectricResistance.FromOhms(gasResistance);
        }

        private byte CalculateHeaterResistance(Temperature setTemp, Temperature ambientTemp)
        {
            if (this.bme680Calibration is null)
            {
                throw new Exception($"{nameof(Bme680)} is incorrectly configured.");
            }

            // limit maximum temperature to 400°C
            var temp = setTemp.DegreesCelsius;
            if (temp > 400)
            {
                temp = 400;
            }

            var var1 = (this.bme680Calibration.DigGh1 / 16.0) + 49.0;
            var var2 = (this.bme680Calibration.DigGh2 / 32768.0 * 0.0005) + 0.00235;
            var var3 = this.bme680Calibration.DigGh3 / 1024.0;
            var var4 = var1 * (1.0 + (var2 * temp));
            var var5 = var4 + (var3 * ambientTemp.DegreesCelsius);
            var heaterResistance = (byte)(3.4 * ((var5 * (4.0 / (4.0 + this.bme680Calibration.ResHeatRange)) * (1.0 / (1.0 + (this.bme680Calibration.ResHeatVal * 0.002)))) - 25));

            return heaterResistance;
        }

        // The duration is interpreted as follows:
        // Byte [7:6]: multiplication factor of 1, 4, 16 or 64
        // Byte [5:0]: 64 timer values, 1ms step size
        // Values are rounded down
        private byte CalculateHeaterDuration(Duration duration)
        {
            byte factor = 0;
            byte durationValue;

            var shortDuration = (ushort)duration.Milliseconds;
            // check if value exceeds maximum duration
            if (shortDuration > 0xFC0)
            {
                durationValue = 0xFF;
            }
            else
            {
                while (shortDuration > 0x3F)
                {
                    shortDuration = (ushort)(shortDuration >> 2);
                    factor += 1;
                }

                durationValue = (byte)(shortDuration + (factor * 64));
            }

            return durationValue;
        }

        private bool TryReadTemperatureCore(out Temperature temperature)
        {
            if (this.TemperatureSampling == Sampling.Skipped)
            {
                temperature = default;
                return false;
            }

            var temp = (int)this.Read24BitsFromRegister((byte)Bme680Register.TEMPDATA, Endianness.BigEndian);

            temperature = this.CompensateTemperature(temp >> 4);
            return true;
        }

        private bool TryReadHumidityCore(out RelativeHumidity humidity, bool skipTempFineRead = false)
        {
            if (this.HumiditySampling == Sampling.Skipped)
            {
                humidity = default;
                return false;
            }

            // Read humidity data.
            var hum = this.Read16BitsFromRegister((byte)Bme680Register.HUMIDITYDATA, Endianness.BigEndian);

            if (!skipTempFineRead)
            {
                this.TryReadTemperatureCore(out _);
            }

            humidity = this.CompensateHumidity(hum);
            return true;
        }

        private bool TryReadPressureCore(out Pressure pressure, bool skipTempFineRead = false)
        {
            if (this.PressureSampling == Sampling.Skipped)
            {
                pressure = default;
                return false;
            }

            // Read pressure data.
            var press = (int)this.Read24BitsFromRegister((byte)Bme680Register.PRESSUREDATA, Endianness.BigEndian);

            // Read the temperature first to load the t_fine value for compensation.
            if (!skipTempFineRead)
            {
                this.TryReadTemperatureCore(out _);
            }

            pressure = this.CompensatePressure(press >> 4);
            return true;
        }

        private bool TryReadGasResistanceCore(out ElectricResistance gasResistance)
        {
            if (!this.ReadGasMeasurementIsValid() || !this.ReadHeaterIsStable())
            {
                gasResistance = default;
                return false;
            }

            // Read 10 bit gas resistance value from registers
            var gasResRaw = this.Read8BitsFromRegister((byte)Bme680Register.GAS_RES);
            var gasRange = this.Read8BitsFromRegister((byte)Bme680Register.GAS_RANGE);

            var gasRes = (ushort)((ushort)(gasResRaw << 2) + (byte)(gasRange >> 6));
            gasRange &= (byte)Bme680Mask.GAS_RANGE;

            gasResistance = this.CalculateGasResistance(gasRes, gasRange);
            return true;
        }
    }
}

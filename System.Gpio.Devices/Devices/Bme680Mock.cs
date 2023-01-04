using System.Device.I2c;
using System.Gpio.Devices.BMxx80;
using System.Gpio.Devices.BMxx80.ReadResult;
using System.Threading.Tasks;
using UnitsNet;

namespace System.Device.Devices
{
    internal class Bme680Mock : IBme680
    {
        private readonly I2cConnectionSettings i2cSettings;
        private readonly Temperature ambientTemperatureDefault;

        public Bme680Mock(I2cConnectionSettings i2cSettings)
        {
            this.i2cSettings = i2cSettings;
        }

        public Bme680Mock(I2cConnectionSettings i2cSettings, Temperature ambientTemperatureDefault) : this(i2cSettings)
        {
            this.ambientTemperatureDefault = ambientTemperatureDefault;
        }

        public bool HeaterIsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Bme680HeaterProfile HeaterProfile { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Sampling HumiditySampling { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Bme680ReadResult Read()
        {
            return new Bme680ReadResult(Temperature.FromDegreesCelsius(20), Pressure.FromHectopascals(1000), RelativeHumidity.FromPercent(60), ElectricResistance.FromKiloohms(30));
        }

        public Task<Bme680ReadResult> ReadAsync()
        {
            return Task.FromResult(this.Read());
        }

        public void Reset()
        {
        }

        public void Dispose()
        {
        }
    }
}
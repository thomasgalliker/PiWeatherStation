using System;
using System.Threading.Tasks;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.ReadResult;

namespace Iot.Device.Bmxx80
{
    /// <summary>
    /// Represents the abstraction for a BME680 temperature, pressure, relative humidity and VOC gas sensor.
    /// </summary>
    public interface IBme680 : IDisposable
    {
        bool HeaterIsEnabled { get; set; }

        Bme680HeaterProfile HeaterProfile { get; set; }

        Sampling HumiditySampling { get; set; }

        Bme680ReadResult Read();

        Task<Bme680ReadResult> ReadAsync();

        void Reset();
    }
}

using System.Gpio.Devices.BMxx80;
using System.Gpio.Devices.BMxx80.ReadResult;
using System.Threading.Tasks;

namespace System.Device.Devices
{
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

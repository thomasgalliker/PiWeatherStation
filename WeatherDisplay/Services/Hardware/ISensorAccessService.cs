using Iot.Device.Bmxx80;
using Iot.Device.Scd4x;

namespace WeatherDisplay.Services.Hardware
{
    public interface ISensorAccessService
    {
        void Initialize();

        IBme680 Bme680 { get; }
        
        IScd4x Scd41 { get; }
    }
}
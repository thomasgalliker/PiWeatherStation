using Iot.Device.Bmxx80;

namespace WeatherDisplay.Services.Hardware
{
    public interface ISensorAccessService
    {
        void Initialize();

        IBme680 Bme680 { get; }
    }
}
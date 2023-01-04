using System.Device.Devices;

namespace WeatherDisplay.Services.Hardware
{
    public interface ISensorAccessService
    {
        void Initialize();

        IBme680 Bme680 { get; }
    }
}
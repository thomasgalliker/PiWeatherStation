using System.Drawing;
namespace WeatherDisplay.Services.QR
{
    public interface IQRCodeService
    {
        Bitmap GenerateQRCode(string content);
    }
}
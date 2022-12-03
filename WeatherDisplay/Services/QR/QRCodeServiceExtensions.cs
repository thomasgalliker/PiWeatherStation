using System.Drawing;
using QRCoder;

namespace WeatherDisplay.Services.QR
{
    public static class QRCodeServiceExtensions
    {
        public static Bitmap GenerateWifiQRCode(this IQRCodeService qrCodeService, string ssid, string password)
        {
            var payloadGenerator = new PayloadGenerator.WiFi(ssid, password, PayloadGenerator.WiFi.Authentication.WPA, isHiddenSSID: false);
            var qrCodeImage = qrCodeService.GenerateQRCode(payloadGenerator.ToString());
            return qrCodeImage;
        }

        public static Bitmap GenerateUrlQRCode(this IQRCodeService qrCodeService, string url)
        {
            var payloadGenerator = new PayloadGenerator.Url(url);
            //var qrCodeImage = this.GenerateQRCode("http://" + ipAddress.ToString());
            var qrCodeImage = qrCodeService.GenerateQRCode(payloadGenerator.ToString());
            return qrCodeImage;
        }
    }
}

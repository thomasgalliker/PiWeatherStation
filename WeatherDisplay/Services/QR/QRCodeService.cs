using System.Drawing;
using System.Net;
using QRCoder;

namespace WeatherDisplay.Services.QR
{
    public class QRCodeService : IQRCodeService
    {
        public QRCodeService()
        {
        }

        public Bitmap GenerateQRCode(string content)
        {
            var qrcodeGenerator = new QRCodeGenerator();
            var qrCodeData = qrcodeGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(pixelsPerModule: 10);
            return qrCodeImage;
        }
    }
}

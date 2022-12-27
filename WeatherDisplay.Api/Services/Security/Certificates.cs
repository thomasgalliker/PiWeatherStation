using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace WeatherDisplay.Api.Services.Security
{
    internal static class Certificates
    {
        internal static X509Certificate2 CreateSelfSignedCertificate(IPAddress address, string distinguishedName = "")
        {
            if (distinguishedName == "")
            {
                distinguishedName = "CN=" + address;
            }

            using (var rsa = RSA.Create(2048))
            {
                var request = new CertificateRequest(new X500DistinguishedName(distinguishedName), rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                request.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false));
                request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(new OidCollection { new Oid("1.3.6.1.5.5.7.3.1") }, false));

                var subjectAlternativeName = new SubjectAlternativeNameBuilder();
                subjectAlternativeName.AddIpAddress(address);
                request.CertificateExtensions.Add(subjectAlternativeName.Build());

                return request.CreateSelfSigned(new DateTimeOffset(DateTime.UtcNow.AddDays(-1)), new DateTimeOffset(DateTime.UtcNow.AddDays(3650)));
            }
        }
    }
}
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using RaspberryPi.NET.Tests.Logging;
using RaspberryPi.Network;
using RaspberryPi.Process;
using RaspberryPi.Services;
using RaspberryPi.Storage;
using Xunit;
using Xunit.Abstractions;

namespace RaspberryPi.NET.Tests.Network
{
    public class AccessPointTests
    {
        private readonly AutoMocker autoMocker;

        public AccessPointTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();
            this.autoMocker.Use<ILogger<AccessPoint>>(new TestOutputHelperLogger<AccessPoint>(testOutputHelper));

            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            fileSystemMock.Setup(f => f.File.Exists("/bin/bash"))
                .Returns(true);

            fileSystemMock.Setup(f => f.File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string p, string c) => testOutputHelper.WriteLine(
                    $"WriteAllText:{Environment.NewLine}" +
                    $"{p}{Environment.NewLine}" +
                    $"{c}"));

            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            processRunnerMock.Setup(p => p.ExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CommandLineResult(0, "", ""));
        }

        [Fact]
        public void ShouldStart()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtlMock = this.autoMocker.GetMock<ISystemCtl>();

            var accessPoint = this.autoMocker.CreateInstance<AccessPoint>();

            // Act
            accessPoint.Start();

            // Assert
            fileSystemMock.VerifyNoOtherCalls();
            processRunnerMock.VerifyNoOtherCalls();

            systemCtlMock.Verify(s => s.IsActive("hostapd@wlan0.service"), Times.Once);
            systemCtlMock.Verify(s => s.StartService("hostapd@wlan0.service"), Times.Once);
            systemCtlMock.Verify(s => s.EnableService("hostapd@wlan0.service"), Times.Once);

            systemCtlMock.Verify(s => s.IsActive("dnsmasq.service"), Times.Once);
            systemCtlMock.Verify(s => s.StartService("dnsmasq.service"), Times.Once);
            systemCtlMock.Verify(s => s.EnableService("dnsmasq.service"), Times.Once);

            systemCtlMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldConfigure()
        {
            // Arrange
            var fileStreamFactoryMock = this.autoMocker.GetMock<IFileStreamFactory>();

            var hostapdStreamWriterMock = new Mock<StreamWriter>("wlan0.conf");
            fileStreamFactoryMock.Setup(f => f.CreateStreamWriter("/etc/hostapd/wlan0.conf", FileMode.Create, FileAccess.Write))
                .Returns(() => hostapdStreamWriterMock.Object);

            var dnsmasqStreamWriterMock = new Mock<StreamWriter>("dnsmasq.conf");
            fileStreamFactoryMock.Setup(f => f.CreateStreamWriter("/etc/dnsmasq.conf", FileMode.Create, FileAccess.Write))
                .Returns(() => dnsmasqStreamWriterMock.Object);

            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            fileSystemMock.SetupGet(f => f.FileStreamFactory)
                .Returns(fileStreamFactoryMock.Object);

            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtlMock = this.autoMocker.GetMock<ISystemCtl>();
            var wpaMock = this.autoMocker.GetMock<IWPA>();
            wpaMock.Setup(w => w.GetCountryCode())
                .ReturnsAsync("CH");

            var accessPoint = this.autoMocker.CreateInstance<AccessPoint>();

            // Act
            await accessPoint.ConfigureAsync("testssid", "testpsdk", IPAddress.Parse("192.168.50.100"), 6);

            // Assert
            fileSystemMock.Verify(f => f.FileStreamFactory.CreateStreamWriter("/etc/hostapd/wlan0.conf", FileMode.Create, FileAccess.Write), Times.Once);
            fileSystemMock.Verify(f => f.FileStreamFactory.CreateStreamWriter("/etc/dnsmasq.conf", FileMode.Create, FileAccess.Write), Times.Once);
            fileSystemMock.VerifyNoOtherCalls();

            processRunnerMock.VerifyNoOtherCalls();

            wpaMock.Verify(w => w.GetCountryCode(), Times.Once);
            wpaMock.VerifyNoOtherCalls();

            systemCtlMock.VerifyNoOtherCalls();

            hostapdStreamWriterMock.Verify(a => a.WriteLineAsync("ssid=testssid"), Times.Once);
            hostapdStreamWriterMock.Verify(a => a.WriteLineAsync("wpa_passphrase=testpsdk"), Times.Once);
            hostapdStreamWriterMock.Verify(a => a.WriteLineAsync("channel=6"), Times.Once);

            dnsmasqStreamWriterMock.Verify(a => a.WriteLineAsync("interface=wlan0"), Times.Once);
            dnsmasqStreamWriterMock.Verify(a => a.WriteLineAsync("no-dhcp-interface=eth0"), Times.Once);
            dnsmasqStreamWriterMock.Verify(a => a.WriteLineAsync("dhcp-range=192.168.50.151,192.168.50.200,255.255.255.0,24h"), Times.Once);
            dnsmasqStreamWriterMock.Verify(a => a.WriteLineAsync("dhcp-option=option:dns-server,192.168.50.100"), Times.Once);
        }
    }
}
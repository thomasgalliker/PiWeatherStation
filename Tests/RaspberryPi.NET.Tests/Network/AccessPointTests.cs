using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using RaspberryPi.Internals;
using RaspberryPi.NET.Tests.Logging;
using RaspberryPi.Network;
using RaspberryPi.Services;
using RaspberryPi.Storage;
using Xunit;
using Xunit.Abstractions;

namespace RaspberryPi.NET.Tests
{
    public class AccessPointTests
    {
        private readonly AutoMocker autoMocker;

        public AccessPointTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();
            this.autoMocker.Use<ILogger<AccessPoint>>(new TestOutputHelperLogger<AccessPoint>(testOutputHelper));

            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            fileSystemMock.Setup(f => f.Exists("/bin/bash"))
                .Returns(true);

            fileSystemMock.Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string p, string c) => testOutputHelper.WriteLine(
                    $"WriteAllText:{Environment.NewLine}" +
                    $"{p}{Environment.NewLine}" +
                    $"{c}"));

            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            processRunnerMock.Setup(p => p.ExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CmdResult(0, Enumerable.Empty<string>(), Enumerable.Empty<string>()));
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

        [Fact(Skip = "To be implemented")]
        public async Task ShouldConfigure()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtlMock = this.autoMocker.GetMock<ISystemCtl>();
            var wpaMock = this.autoMocker.GetMock<IWPA>();
            wpaMock.Setup(w => w.GetCountryCode())
                .ReturnsAsync("CH");

            var accessPoint = this.autoMocker.CreateInstance<AccessPoint>();

            // Act
            await accessPoint.ConfigureAsync("testssid", "testpsdk", IPAddress.Parse("192.168.50.100"), 99);

            // Assert
            fileSystemMock.VerifyNoOtherCalls();
            processRunnerMock.VerifyNoOtherCalls();
            wpaMock.VerifyNoOtherCalls();

            systemCtlMock.Verify(s => s.IsActive("hostapd@wlan0.service"), Times.Once);
            systemCtlMock.Verify(s => s.StartService("hostapd@wlan0.service"), Times.Once);
            systemCtlMock.Verify(s => s.EnableService("hostapd@wlan0.service"), Times.Once);

            systemCtlMock.Verify(s => s.IsActive("dnsmasq.service"), Times.Once);
            systemCtlMock.Verify(s => s.StartService("dnsmasq.service"), Times.Once);
            systemCtlMock.Verify(s => s.EnableService("dnsmasq.service"), Times.Once);

            systemCtlMock.VerifyNoOtherCalls();
        }
    }
}
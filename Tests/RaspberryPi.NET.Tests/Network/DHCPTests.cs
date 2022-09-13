using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using RaspberryPi.NET.Tests.Logging;
using RaspberryPi.NET.Tests.TestData;
using RaspberryPi.Network;
using RaspberryPi.Process;
using RaspberryPi.Services;
using RaspberryPi.Storage;
using Xunit;
using Xunit.Abstractions;

namespace RaspberryPi.NET.Tests.Network
{
    public class DHCPTests
    {
        private readonly AutoMocker autoMocker;

        public DHCPTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();
            this.autoMocker.Use<ILogger<DHCP>>(new TestOutputHelperLogger<DHCP>(testOutputHelper));

            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            fileSystemMock.Setup(f => f.File.Exists(DHCP.DhcpdConfFilePath))
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
        public async Task ShouldSetIPAddress_ForAccessPoint()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            fileSystemMock.Setup(f => f.FileStreamFactory.CreateStreamReader(DHCP.DhcpdConfFilePath, FileMode.Open, FileAccess.Read))
                .Returns(() => new StreamReader(Files.GetDhcpdConfStream()));

            var configStream = new MemoryStream();
            await Files.GetDhcpdConfStream().CopyToAsync(configStream);
            fileSystemMock.Setup(f => f.FileStreamFactory.Create(DHCP.DhcpdConfFilePath, FileMode.Open, FileAccess.ReadWrite))
                .Returns(() => configStream);
            
            var networkInterfaceMocks = NetworkInterfaces.GetNetworkInterfaceMocks();
            var networkInterfaceMock = this.autoMocker.GetMock<INetworkInterfaceService>();
            networkInterfaceMock.Setup(n => n.GetAllNetworkInterfaces())
                .Returns(networkInterfaceMocks.Select(m => m.Object));

            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtlMock = this.autoMocker.GetMock<ISystemCtl>();

            var iface = "wlan0";
            var ip = IPAddress.Parse("192.168.1.50");
            var netmask = IPAddress.Parse("255.255.255.0");
            var gateway = IPAddress.Parse("192.168.1.1");
            var dnsServer = IPAddress.Parse("192.168.1.2");
            var forAP = true;

            var dhcp = this.autoMocker.CreateInstance<DHCP>();

            // Act
            await dhcp.SetIPAddressAsync(iface, ip, netmask, gateway, dnsServer, forAP);

            // Assert
            fileSystemMock.Verify(f => f.File.Exists(DHCP.DhcpdConfFilePath), Times.Once);
            fileSystemMock.Verify(f => f.FileStreamFactory.CreateStreamReader(DHCP.DhcpdConfFilePath, FileMode.Open, FileAccess.Read), Times.Once);
            fileSystemMock.Verify(f => f.FileStreamFactory.Create(DHCP.DhcpdConfFilePath, FileMode.Open, FileAccess.ReadWrite), Times.Once);
            fileSystemMock.VerifyNoOtherCalls();

            processRunnerMock.VerifyNoOtherCalls();

            systemCtlMock.Verify(s => s.RestartService(DHCP.DhcpdService), Times.Once);
            systemCtlMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSetIPAddress_NotForAccessPoint()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            fileSystemMock.Setup(f => f.FileStreamFactory.CreateStreamReader(DHCP.DhcpdConfFilePath, FileMode.Open, FileAccess.Read))
                .Returns(() => new StreamReader(Files.GetDhcpdConfStream()));

            var configStream = new MemoryStream();
            await Files.GetDhcpdConfStream().CopyToAsync(configStream);
            fileSystemMock.Setup(f => f.FileStreamFactory.Create(DHCP.DhcpdConfFilePath, FileMode.Open, FileAccess.ReadWrite))
                .Returns(() => configStream);
            
            var networkInterfaceMocks = NetworkInterfaces.GetNetworkInterfaceMocks();
            var networkInterfaceMock = this.autoMocker.GetMock<INetworkInterfaceService>();
            networkInterfaceMock.Setup(n => n.GetAllNetworkInterfaces())
                .Returns(networkInterfaceMocks.Select(m => m.Object));

            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtlMock = this.autoMocker.GetMock<ISystemCtl>();

            var iface = "wlan0";
            var ip = IPAddress.Parse("192.168.1.50");
            var netmask = IPAddress.Parse("255.255.255.0");
            var gateway = IPAddress.Parse("192.168.1.1");
            var dnsServer = IPAddress.Parse("192.168.1.2");
            var forAP = (bool?)null;

            var dhcp = this.autoMocker.CreateInstance<DHCP>();

            // Act
            await dhcp.SetIPAddressAsync(iface, ip, netmask, gateway, dnsServer, forAP);

            // Assert
            fileSystemMock.Verify(f => f.File.Exists(DHCP.DhcpdConfFilePath), Times.Once);
            fileSystemMock.Verify(f => f.FileStreamFactory.CreateStreamReader(DHCP.DhcpdConfFilePath, FileMode.Open, FileAccess.Read), Times.Once);
            fileSystemMock.Verify(f => f.FileStreamFactory.Create(DHCP.DhcpdConfFilePath, FileMode.Open, FileAccess.ReadWrite), Times.Once);
            fileSystemMock.VerifyNoOtherCalls();

            processRunnerMock.Verify(p => p.ExecuteCommand("ip link set wlan0 down", It.IsAny<CancellationToken>()), Times.Once);
            processRunnerMock.Verify(p => p.ExecuteCommand("ip link set wlan0 up", It.IsAny<CancellationToken>()), Times.Once);
            processRunnerMock.VerifyNoOtherCalls();

            systemCtlMock.VerifyNoOtherCalls();
        }
    }
}
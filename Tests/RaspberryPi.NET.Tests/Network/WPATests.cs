using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using RaspberryPi.Internals.ResourceLoader;
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
    public class WPATests
    {
        private readonly AutoMocker autoMocker;

        public WPATests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();
            this.autoMocker.Use<ILogger<WPA>>(new TestOutputHelperLogger<WPA>(testOutputHelper));

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
        public async Task ShouldGetSSIDs()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();

            var wpaSupplicantConfStream = Files.GetWPASupplicantConfStream();
            var wpaSupplicantConfStreamReader = new StreamReader(wpaSupplicantConfStream);
            fileSystemMock.Setup(f => f.FileStreamFactory.CreateStreamReader(WPA.WpaSupplicantConfFilePath, FileMode.Open, FileAccess.Read))
                .Returns(() => wpaSupplicantConfStreamReader);

            fileSystemMock.Setup(f => f.File.Exists(WPA.WpaSupplicantConfFilePath))
                .Returns(true);

            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtlMock = this.autoMocker.GetMock<ISystemCtl>();

            var wpa = this.autoMocker.CreateInstance<WPA>();

            // Act
            var ssids = await wpa.GetSSIDs();

            // Assert
            ssids.Should().HaveCount(1);
            ssids.ElementAt(0).Should().Be("galliker");
        }

        [Fact]
        public async Task ShouldGetCountryCode()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();

            var wpaSupplicantConfStream = Files.GetWPASupplicantConfStream();
            var wpaSupplicantConfStreamReader = new StreamReader(wpaSupplicantConfStream);
            fileSystemMock.Setup(f => f.FileStreamFactory.CreateStreamReader(WPA.WpaSupplicantConfFilePath, FileMode.Open, FileAccess.Read))
                .Returns(() => wpaSupplicantConfStreamReader);

            fileSystemMock.Setup(f => f.File.Exists(WPA.WpaSupplicantConfFilePath))
                .Returns(true);

            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtlMock = this.autoMocker.GetMock<ISystemCtl>();

            var wpa = this.autoMocker.CreateInstance<WPA>();

            // Act
            var countryCode = await wpa.GetCountryCode();

            // Assert
            countryCode.Should().Be("CH");
        }
    }
}
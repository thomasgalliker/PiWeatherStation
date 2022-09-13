using System.Threading;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using RaspberryPi.NET.Tests.Logging;
using RaspberryPi.Process;
using RaspberryPi.Services;
using Xunit;
using Xunit.Abstractions;

namespace RaspberryPi.NET.Tests
{
    public class SystemCtlTests
    {
        private readonly AutoMocker autoMocker;
        private const string ServiceName = "serviceName";

        public SystemCtlTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();
            this.autoMocker.Use<ILogger<SystemCtl>>(new TestOutputHelperLogger<SystemCtl>(testOutputHelper));
        }

        [Fact]
        public void ShouldStartService_Success()
        {
            // Arrange
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            processRunnerMock.Setup(p => p.ExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CommandLineResult(0, "", ""));

            var systemCtl = this.autoMocker.CreateInstance<SystemCtl>();

            // Act
            var result = systemCtl.StartService(ServiceName);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldStartService_Failed()
        {
            // Arrange
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            processRunnerMock.Setup(p => p.ExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CommandLineResult(99, "", "Service does not exist"));

            var systemCtl = this.autoMocker.CreateInstance<SystemCtl>();

            // Act
            var result = systemCtl.StartService(ServiceName);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldReloadDaemon()
        {
            // Arrange
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            processRunnerMock.Setup(p => p.ExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CommandLineResult(0, "", ""));

            var systemCtl = this.autoMocker.CreateInstance<SystemCtl>();

            // Act
            var result = systemCtl.ReloadDaemon();

            // Assert
            result.Should().BeTrue();
        }
    }
}
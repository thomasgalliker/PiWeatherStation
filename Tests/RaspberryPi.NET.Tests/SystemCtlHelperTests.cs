using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using RaspberryPi.NET.Tests.Logging;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace RaspberryPi.NET.Tests
{
    public class SystemCtlHelperTests
    {
        private readonly AutoMocker autoMocker;
        private const string ServiceName = "serviceName";

        public SystemCtlHelperTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();
            this.autoMocker.Use<ILogger<SystemCtlHelper>>(new TestOutputHelperLogger<SystemCtlHelper>(testOutputHelper));
        }

        [Fact]
        public void ShouldStartService_Success()
        {
            // Arrange
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            processRunnerMock.Setup(p => p.ExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CmdResult(0, Enumerable.Empty<string>(), Enumerable.Empty<string>()));

            var systemCtlHelper = this.autoMocker.CreateInstance<SystemCtlHelper>();

            // Act
            var result = systemCtlHelper.StartService(ServiceName);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldStartService_Failed()
        {
            // Arrange
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            processRunnerMock.Setup(p => p.ExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CmdResult(99, Enumerable.Empty<string>(), new List<string> { "Service does not exist" }));

            var systemCtlHelper = this.autoMocker.CreateInstance<SystemCtlHelper>();

            // Act
            var result = systemCtlHelper.StartService(ServiceName);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldReloadDaemon()
        {
            // Arrange
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            processRunnerMock.Setup(p => p.ExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CmdResult(0, Enumerable.Empty<string>(), Enumerable.Empty<string>()));

            var systemCtlHelper = this.autoMocker.CreateInstance<SystemCtlHelper>();

            // Act
            var result = systemCtlHelper.ReloadDaemon();

            // Assert
            result.Should().BeTrue();
        }
    }
}
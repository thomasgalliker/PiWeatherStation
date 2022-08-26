using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using RaspberryPi.NET.Tests.Logging;
using Xunit;
using Xunit.Abstractions;

namespace RaspberryPi.NET.Tests
{
    public class LinuxServiceConfiguratorTests
    {
        private readonly AutoMocker autoMocker;
        private const string ServiceName = "serviceName";

        public LinuxServiceConfiguratorTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();
            this.autoMocker.Use<ILogger<LinuxServiceConfigurator>>(new TestOutputHelperLogger<LinuxServiceConfigurator>(testOutputHelper));
        }

        [Fact]
        public void ShouldConfigureServiceByInstanceName()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            fileSystemMock.Setup(f => f.Exists("/bin/bash"))
                .Returns(true);

            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            processRunnerMock.Setup(p => p.ExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CmdResult(0, Enumerable.Empty<string>(), Enumerable.Empty<string>()));

            var serviceConfigurationState = new ServiceConfigurationState
            {
                Install = true
            };

            var linuxServiceConfigurator = this.autoMocker.CreateInstance<LinuxServiceConfigurator>();

            // Act
            linuxServiceConfigurator.ConfigureServiceByInstanceName(ServiceName, "exePath", "instance", "service description", serviceConfigurationState);

            // Assert
            //result.Should().BeTrue();
        }
    }
}
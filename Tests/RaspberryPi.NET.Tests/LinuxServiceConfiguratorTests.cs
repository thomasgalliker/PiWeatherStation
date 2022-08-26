using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using RaspberryPi.Internals;
using RaspberryPi.NET.Tests.Logging;
using RaspberryPi.Services;
using RaspberryPi.Storage;
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

            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            fileSystemMock.Setup(f => f.Exists("/bin/bash"))
                .Returns(true);

            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            processRunnerMock.Setup(p => p.ExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CmdResult(0, Enumerable.Empty<string>(), Enumerable.Empty<string>()));
        }

        [Fact]
        public void ShouldInstallService()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtlHelperMock = this.autoMocker.GetMock<ISystemCtlHelper>();

            var linuxServiceConfigurator = this.autoMocker.CreateInstance<LinuxServiceConfigurator>();

            // Act
            linuxServiceConfigurator.InstallService(ServiceName, "execStart", "serviceDescription", "userName", new List<string> { "dependency1" });

            // Assert
            VerifyPrerequisites(fileSystemMock);
            fileSystemMock.Verify(f => f.WriteAllText("/etc/systemd/system/serviceName.service", It.IsAny<string>()), Times.Once);
            fileSystemMock.VerifyNoOtherCalls();

            processRunnerMock.Verify(p => p.ExecuteCommand(It.Is<CommandLineInvocation>(i => 
                i.Executable == "/bin/bash" &&
                i.Arguments == "-c \"chmod 644 /etc/systemd/system/serviceName.service\""), It.IsAny<CancellationToken>()), Times.Once);
            VerifyPrerequisites(processRunnerMock);
            processRunnerMock.VerifyNoOtherCalls();

            systemCtlHelperMock.Verify(s => s.EnableService(ServiceName), Times.Once);
            systemCtlHelperMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldReinstallService()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtlHelperMock = this.autoMocker.GetMock<ISystemCtlHelper>();

            var linuxServiceConfigurator = this.autoMocker.CreateInstance<LinuxServiceConfigurator>();

            // Act
            linuxServiceConfigurator.ReinstallService(ServiceName, "execStart", "serviceDescription", "userName", new List<string> { "dependency1" });

            // Assert
            VerifyPrerequisites(fileSystemMock);
            fileSystemMock.Verify(f => f.Delete("/etc/systemd/system/serviceName.service"), Times.Once);
            fileSystemMock.Verify(f => f.WriteAllText("/etc/systemd/system/serviceName.service", It.IsAny<string>()), Times.Once);
            fileSystemMock.VerifyNoOtherCalls();

            processRunnerMock.Verify(p => p.ExecuteCommand(It.Is<CommandLineInvocation>(i =>
                i.Executable == "/bin/bash" &&
                i.Arguments == "-c \"chmod 644 /etc/systemd/system/serviceName.service\""), It.IsAny<CancellationToken>()), Times.Once);
            VerifyPrerequisites(processRunnerMock);
            processRunnerMock.VerifyNoOtherCalls();

            systemCtlHelperMock.Verify(s => s.StopService(ServiceName), Times.Once);
            systemCtlHelperMock.Verify(s => s.DisableService(ServiceName), Times.Once);
            systemCtlHelperMock.Verify(s => s.EnableService(ServiceName), Times.Once);
            systemCtlHelperMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldUninstallService()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtlHelperMock = this.autoMocker.GetMock<ISystemCtlHelper>();

            var linuxServiceConfigurator = this.autoMocker.CreateInstance<LinuxServiceConfigurator>();

            // Act
            linuxServiceConfigurator.UninstallService(ServiceName);

            // Assert
            VerifyPrerequisites(fileSystemMock);
            fileSystemMock.Verify(f => f.Delete("/etc/systemd/system/serviceName.service"), Times.Once);
            fileSystemMock.VerifyNoOtherCalls();

            VerifyPrerequisites(processRunnerMock);
            processRunnerMock.VerifyNoOtherCalls();

            systemCtlHelperMock.Verify(s => s.StopService(ServiceName), Times.Once);
            systemCtlHelperMock.Verify(s => s.DisableService(ServiceName), Times.Once);
            systemCtlHelperMock.VerifyNoOtherCalls();
        }

        private static void VerifyPrerequisites(Mock<IFileSystem> fileSystemMock)
        {
            fileSystemMock.Verify(f => f.Exists("/bin/bash"), Times.Once);
        }

        private static void VerifyPrerequisites(Mock<IProcessRunner> processRunnerMock)
        {
            processRunnerMock.Verify(p => p.ExecuteCommand(It.Is<CommandLineInvocation>(i =>
                i.Executable == "/bin/bash" &&
                i.Arguments == "-c \"sudo -vn 2> /dev/null\""), It.IsAny<CancellationToken>()), Times.Once);
            processRunnerMock.Verify(p => p.ExecuteCommand(It.Is<CommandLineInvocation>(i =>
                i.Executable == "/bin/bash" &&
                i.Arguments == "-c \"command -v systemctl >/dev/null\""), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
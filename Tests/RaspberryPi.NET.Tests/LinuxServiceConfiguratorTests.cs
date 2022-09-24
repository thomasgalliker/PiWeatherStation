using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using RaspberryPi.NET.Tests.Logging;
using RaspberryPi.Process;
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
        private static readonly ServiceDefinition TestServiceDefinition = new ServiceDefinition(ServiceName)
        {
            ServiceType = ServiceType.Simple,
            WorkingDirectory = "workingDirectory",
            ExecStart = "execStart",
            ExecStop = "execStop",
            KillSignal = "killSignal",
            KillMode = KillMode.Process,
            ServiceDescription = "serviceDescription",
            UserName = "userName",
            GroupName = "groupName",
            Restart = ServiceRestart.No,
            RestartSec = null,
            AfterServices = new[]
            {
                "network-online.target",
                "firewalld.service"
            },
            WantsServices = new[]
            {
                "network-online.target"
            },
            Environments = new[]
            {
                "ASPNETCORE_ENVIRONMENT=Production",
                "DOTNET_PRINT_TELEMETRY_MESSAGE=false",
                "DOTNET_ROOT=/home/pi/.dotnet"
            }
        };

        public LinuxServiceConfiguratorTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();
            this.autoMocker.Use<ILogger<LinuxServiceConfigurator>>(new TestOutputHelperLogger<LinuxServiceConfigurator>(testOutputHelper));

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
                .Returns(new CommandLineResult(0));
            processRunnerMock.Setup(p => p.TryExecuteCommand(It.IsAny<CommandLineInvocation>(), It.IsAny<CancellationToken>()))
                .Returns(new CommandLineResult(0));
        }

        [Fact]
        public void ShouldInstallService()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtl = this.autoMocker.GetMock<ISystemCtl>();

            var linuxServiceConfigurator = this.autoMocker.CreateInstance<LinuxServiceConfigurator>();

            // Act
            linuxServiceConfigurator.InstallService(TestServiceDefinition);

            // Assert
            VerifyPrerequisites(fileSystemMock);
            fileSystemMock.Verify(f => f.File.WriteAllText("/etc/systemd/system/serviceName.service", It.IsAny<string>()), Times.Once);
            fileSystemMock.VerifyNoOtherCalls();

            processRunnerMock.Verify(p => p.ExecuteCommand(It.Is<CommandLineInvocation>(i =>
                i.Executable == "/bin/bash" &&
                i.Arguments == "-c \"chmod 644 /etc/systemd/system/serviceName.service\""), It.IsAny<CancellationToken>()), Times.Once);
            VerifyPrerequisites(processRunnerMock);
            processRunnerMock.VerifyNoOtherCalls();

            systemCtl.Verify(s => s.EnableService(ServiceName), Times.Once);
            systemCtl.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldReinstallService()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtl = this.autoMocker.GetMock<ISystemCtl>();

            var linuxServiceConfigurator = this.autoMocker.CreateInstance<LinuxServiceConfigurator>();

            // Act
            linuxServiceConfigurator.ReinstallService(TestServiceDefinition);

            // Assert
            VerifyPrerequisites(fileSystemMock);
            fileSystemMock.Verify(f => f.File.Delete("/etc/systemd/system/serviceName.service"), Times.Once);
            fileSystemMock.Verify(f => f.File.WriteAllText("/etc/systemd/system/serviceName.service", It.IsAny<string>()), Times.Once);
            fileSystemMock.VerifyNoOtherCalls();

            processRunnerMock.Verify(p => p.ExecuteCommand(It.Is<CommandLineInvocation>(i =>
                i.Executable == "/bin/bash" &&
                i.Arguments == "-c \"chmod 644 /etc/systemd/system/serviceName.service\""), It.IsAny<CancellationToken>()), Times.Once);
            VerifyPrerequisites(processRunnerMock);
            processRunnerMock.VerifyNoOtherCalls();

            systemCtl.Verify(s => s.StopService(ServiceName), Times.Once);
            systemCtl.Verify(s => s.DisableService(ServiceName), Times.Once);
            systemCtl.Verify(s => s.EnableService(ServiceName), Times.Once);
            systemCtl.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldUninstallService()
        {
            // Arrange
            var fileSystemMock = this.autoMocker.GetMock<IFileSystem>();
            var processRunnerMock = this.autoMocker.GetMock<IProcessRunner>();
            var systemCtl = this.autoMocker.GetMock<ISystemCtl>();

            var linuxServiceConfigurator = this.autoMocker.CreateInstance<LinuxServiceConfigurator>();

            // Act
            linuxServiceConfigurator.UninstallService(ServiceName);

            // Assert
            VerifyPrerequisites(fileSystemMock);
            fileSystemMock.Verify(f => f.File.Delete("/etc/systemd/system/serviceName.service"), Times.Once);
            fileSystemMock.VerifyNoOtherCalls();

            VerifyPrerequisites(processRunnerMock);
            processRunnerMock.VerifyNoOtherCalls();

            systemCtl.Verify(s => s.StopService(ServiceName), Times.Once);
            systemCtl.Verify(s => s.DisableService(ServiceName), Times.Once);
            systemCtl.VerifyNoOtherCalls();
        }

        private static void VerifyPrerequisites(Mock<IFileSystem> fileSystemMock)
        {
            fileSystemMock.Verify(f => f.File.Exists("/bin/bash"), Times.Once);
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
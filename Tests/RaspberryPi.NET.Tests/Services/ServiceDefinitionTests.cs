using FluentAssertions;
using RaspberryPi.Services;
using Xunit;

namespace RaspberryPi.NET.Tests.Services
{
    public class ServiceDefinitionTests
    {
        [Fact]
        public void ShouldGetSystemdUnitFile_Default()
        {
            // Arrange
            var serviceDefinition = new ServiceDefinition("serviceName");

            // Act
            var result = serviceDefinition.GetSystemdUnitFile();

            // Assert
            result.Should().Be(
                "[Unit]\r\n" +
                "\r\n" +
                "[Service]\r\n" +
                "Type=oneshot\r\n" +
                "SyslogIdentifier=serviceName\r\n" +
                "\r\n" +
                "[Install]\r\n" +
                "WantedBy=multi-user.target");
        }

        [Fact]
        public void ShouldGetSystemdUnitFile_Sample()
        {
            // Arrange
            var serviceDefinition = new ServiceDefinition("serviceName")
            {
                ServiceDescription = "Test service description",
                ServiceType = ServiceType.Notify,
                WorkingDirectory = "/home/pi/directory",
                ExecStart = "/home/pi/directory/executable",
                ExecStop = "execStop",
                KillSignal = "killSignal",
                KillMode = KillMode.Process,
                Restart = ServiceRestart.No,
                UserName = "pi",
                GroupName = "pi",
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

            // Act
            var result = serviceDefinition.GetSystemdUnitFile();

            // Assert
            result.Should().Be(
                "[Unit]\r\n" +
                "Description=Test service description\r\n" +
                "After=network-online.target firewalld.service\r\n" +
                "Wants=network-online.target\r\n" +
                "\r\n" +
                "[Service]\r\n" +
                "Type=notify\r\n" +
                "WorkingDirectory=/home/pi/directory\r\n" +
                "ExecStart=/home/pi/directory/executable\r\n" +
                "ExecStop=execStop\r\n" +
                "KillSignal=killSignal\r\n" +
                "KillMode=process\r\n" +
                "SyslogIdentifier=serviceName\r\n" +
                "\r\n" +
                "User=pi\r\n" +
                "Group=pi\r\n" +
                "\r\n" +
                "Restart=no\r\n" +
                "\r\n" +
                "Environment=ASPNETCORE_ENVIRONMENT=Production\r\n" +
                "Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false\r\n" +
                "Environment=DOTNET_ROOT=/home/pi/.dotnet\r\n" +
                "\r\n" +
                "[Install]\r\n" +
                "WantedBy=multi-user.target");
        }
    }
}

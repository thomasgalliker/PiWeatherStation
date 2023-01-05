using System.Device.I2c;
using FluentAssertions;
using Iot.Device.Bmxx80;
using Moq;
using Moq.AutoMock;
using UnitsNet;
using WeatherDisplay.Services.Hardware;
using Xunit;

namespace WeatherDisplay.Tests.Services.Hardware
{
    public class SensorAccessServiceTests
    {
        private readonly AutoMocker autoMocker;

        public SensorAccessServiceTests()
        {
            this.autoMocker = new AutoMocker();
        }

        [Fact]
        public void ShouldInitialize()
        {
            // Arrange
            var bme680Mock = this.autoMocker.GetMock<IBme680>();

            var bme680FactoryMock = this.autoMocker.GetMock<IBme680Factory>();
            bme680FactoryMock.Setup(f => f.Create(It.IsAny<I2cConnectionSettings>(), It.IsAny<Temperature>()))
                .Returns(bme680Mock.Object);

            var sensorAccessService = this.autoMocker.CreateInstance<SensorAccessService>();

            // Act
            sensorAccessService.Initialize();

            // Assert
            sensorAccessService.Bme680.Should().Be(bme680Mock.Object);

            bme680Mock.Verify(b => b.Reset());
            bme680Mock.VerifyNoOtherCalls();
        }
    }
}

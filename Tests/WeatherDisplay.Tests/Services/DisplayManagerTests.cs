using System.Collections.Generic;
using DisplayService.Services;
using DisplayService.Settings;
using DisplayService.Tests.Services;
using Moq;
using Moq.AutoMock;
using SkiaSharp;
using WeatherDisplay.Model;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Services;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests
{
    public class DisplayManagerTests
    {
        private readonly TestHelper testHelper;
        private readonly AutoMocker autoMocker;
        private readonly Mock<IAppSettings> appSettingsMock;

        public DisplayManagerTests(ITestOutputHelper testOutputHelper)
        {
            this.testHelper = new TestHelper(testOutputHelper);
            this.autoMocker = new AutoMocker();

            this.appSettingsMock = this.autoMocker.GetMock<IAppSettings>();
            this.appSettingsMock.SetupGet(r => r.Title)
                .Returns("Test");
            this.appSettingsMock.SetupGet(r => r.Places)
                .Returns(new List<Place> { new Place { Name = "Test Place", Longitude = 1d, Latitude = 2d } });

            var renderSettingsMock = this.autoMocker.GetMock<IRenderSettings>();
            renderSettingsMock.SetupGet(r => r.Height)
                .Returns(480);
            renderSettingsMock.SetupGet(r => r.Width)
                .Returns(800);
            renderSettingsMock.SetupGet(r => r.BackgroundColor)
                .Returns(SKColors.White.ToString());

            this.autoMocker.Use<IRenderService>(this.autoMocker.CreateInstance<RenderService>());
        }

        [Fact]
        public void ShouldRenderWeatherActions()
        {
            // Arrange
            var openWeatherMapServiceMock = this.autoMocker.GetMock<IOpenWeatherMapService>();
            openWeatherMapServiceMock.SetupSequence(w => w.GetWeatherInfoAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new WeatherResponse { LocationName = "Test Location", Temperature = 1.2f, UnitSystem = "metric" })
                .ReturnsAsync(new WeatherResponse { LocationName = "Test Location", Temperature = 12.34f, UnitSystem = "metric" })
                .ReturnsAsync(new WeatherResponse { LocationName = "Test Location", Temperature = 123.456f, UnitSystem = "metric" })
                .ReturnsAsync(new WeatherResponse { LocationName = "Test Location", Temperature = 12.34f, UnitSystem = "metric" })
                .ReturnsAsync(new WeatherResponse { LocationName = "Test Location", Temperature = 1.2f, UnitSystem = "metric" })
                ;

            var testDisplay = new TestDisplay(800, 480);
            this.autoMocker.Use<IDisplay>(testDisplay);

            var timerMocks = new List<Mock<ITimerService>>();
            var timerServiceFactoryMock = this.autoMocker.GetMock<ITimerServiceFactory>();
            timerServiceFactoryMock.Setup(f => f.Create())
                .Returns(() => { var mock = new Mock<ITimerService>(); timerMocks.Add(mock); return mock.Object; });

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.AddWeatherRenderActions(openWeatherMapServiceMock.Object, this.appSettingsMock.Object);
            displayManager.StartAsync();

            // Act
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));

            // Assert
            openWeatherMapServiceMock.Verify(w => w.GetWeatherInfoAsync(It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(5));

            var bitmapStream = testDisplay.GetDisplayImage();
            this.testHelper.WriteFile(bitmapStream);
        }
    }
}
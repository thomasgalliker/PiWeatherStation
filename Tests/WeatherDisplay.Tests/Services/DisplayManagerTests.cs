using System;
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
        private readonly Mock<IDateTime> dateTimeMock;
        private readonly TestDisplay testDisplay;

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

            this.testDisplay = new TestDisplay(800, 480);
            this.autoMocker.Use<IDisplay>(this.testDisplay);

            this.dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            this.dateTimeMock.SetupGet(d => d.Now)
                .Returns(DateTime.Now);

            this.autoMocker.Use<IRenderService>(this.autoMocker.CreateInstance<RenderService>());
        }

        [Fact]
        public void ShouldRenderWeatherActions()
        {
            // Arrange
            var openWeatherMapServiceMock = this.autoMocker.GetMock<IOpenWeatherMapService>();
            openWeatherMapServiceMock.Setup(w => w.GetCurrentWeatherAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new WeatherInfo { Name = "Test Location", Main = new TemperatureInfo { Temperature = Temperature.FromCelsius(-99d) }, });

            var timerMocks = new List<Mock<ITimerService>>();
            var timerServiceFactoryMock = this.autoMocker.GetMock<ITimerServiceFactory>();
            timerServiceFactoryMock.Setup(f => f.Create())
                .Returns(() => { var mock = new Mock<ITimerService>(); timerMocks.Add(mock); return mock.Object; });

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.AddWeatherRenderActions(openWeatherMapServiceMock.Object, this.dateTimeMock.Object, this.appSettingsMock.Object);
            displayManager.StartAsync();

            // Act
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            this.testHelper.WriteFile(bitmapStream);

            openWeatherMapServiceMock.Verify(w => w.GetCurrentWeatherAsync(It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(5));
        }

        [Fact]
        public void ShouldRenderWeatherActions_TemperatureChanges()
        {
            // Arrange
            var openWeatherMapServiceMock = this.autoMocker.GetMock<IOpenWeatherMapService>();
            openWeatherMapServiceMock.SetupSequence(w => w.GetCurrentWeatherAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new WeatherInfo { Name = "Test Location", Main = new TemperatureInfo { Temperature = new Temperature(1.2f, TemperatureUnit.Celsius) }, })
                .ReturnsAsync(new WeatherInfo { Name = "Test Location", Main = new TemperatureInfo { Temperature = new Temperature(12.34f, TemperatureUnit.Celsius) }, })
                .ReturnsAsync(new WeatherInfo { Name = "Test Location", Main = new TemperatureInfo { Temperature = new Temperature(123.456f, TemperatureUnit.Celsius) }, })
                .ReturnsAsync(new WeatherInfo { Name = "Test Location", Main = new TemperatureInfo { Temperature = new Temperature(12.34f, TemperatureUnit.Celsius) }, })
                .ReturnsAsync(new WeatherInfo { Name = "Test Location", Main = new TemperatureInfo { Temperature = new Temperature(1.8f, TemperatureUnit.Celsius) }, })
                ;

            var timerMocks = new List<Mock<ITimerService>>();
            var timerServiceFactoryMock = this.autoMocker.GetMock<ITimerServiceFactory>();
            timerServiceFactoryMock.Setup(f => f.Create())
                .Returns(() => { var mock = new Mock<ITimerService>(); timerMocks.Add(mock); return mock.Object; });

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.AddWeatherRenderActions(openWeatherMapServiceMock.Object, this.dateTimeMock.Object, this.appSettingsMock.Object);
            displayManager.StartAsync();

            // Act
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));
            timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            this.testHelper.WriteFile(bitmapStream);

            openWeatherMapServiceMock.Verify(w => w.GetCurrentWeatherAsync(It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(5));
        }

        [Fact]
        public void ShouldRenderWeatherActions_DateTimeChanges()
        {
            // Arrange
            var openWeatherMapServiceMock = this.autoMocker.GetMock<IOpenWeatherMapService>();
            openWeatherMapServiceMock.Setup(w => w.GetCurrentWeatherAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new WeatherInfo { Name = "Test Location", Main = new TemperatureInfo { Temperature = Temperature.FromCelsius(-99d) }, });

            var beginOfYear = new DateTime(2000, 01, 01, 23, 59, 59, DateTimeKind.Local);
            var endOfYear = beginOfYear.AddYears(1).AddDays(-1);
            var numberOfDaysInYear = (int)endOfYear.Subtract(beginOfYear).TotalDays;

            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            var dateTimeSetupSequence = dateTimeMock.SetupSequence(d => d.Now);
            for (var i = 0; i <= numberOfDaysInYear; i++)
            {
                dateTimeSetupSequence.Returns(beginOfYear.AddDays(i));
                dateTimeSetupSequence.Returns(beginOfYear.AddDays(i));
            }

            var timerMocks = new List<Mock<ITimerService>>();
            var timerServiceFactoryMock = this.autoMocker.GetMock<ITimerServiceFactory>();
            timerServiceFactoryMock.Setup(f => f.Create())
                .Returns(() => { var mock = new Mock<ITimerService>(); timerMocks.Add(mock); return mock.Object; });

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.AddWeatherRenderActions(openWeatherMapServiceMock.Object, dateTimeMock.Object, this.appSettingsMock.Object);
            displayManager.StartAsync();

            // Act
            for (var i = 0; i < numberOfDaysInYear; i++)
            {
                timerMocks.ForEach((t) => t.Raise(t => t.Elapsed += null, new TimerElapsedEventArgs()));
            }

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            this.testHelper.WriteFile(bitmapStream);

            //dateTimeMock.Verify(d => d.Now, Times.Exactly(numberOfDaysInYear * 2));
        }
    }
}
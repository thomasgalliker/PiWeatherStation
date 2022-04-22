using System;
using System.Collections.Generic;
using System.Threading;
using DisplayService.Services;
using DisplayService.Settings;
using DisplayService.Tests.Services;
using Moq;
using Moq.AutoMock;
using NCrontab;
using SkiaSharp;
using WeatherDisplay.Model;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Resources;
using WeatherDisplay.Services;
using WeatherDisplay.Tests.Testdata;
using Xunit;
using Xunit.Abstractions;
using WeatherDisplay.Tests.Extensions;
using IDateTime = DisplayService.Services.IDateTime;
using NCrontab.Scheduler;

namespace WeatherDisplay.Tests
{
    public class WeatherDisplayRenderingTests
    {
        private readonly TestHelper testHelper;
        private readonly AutoMocker autoMocker;
        private readonly Mock<IAppSettings> appSettingsMock;
        private readonly Mock<IDateTime> dateTimeMock;
        private readonly TestDisplay testDisplay;
        private readonly Mock<IOpenWeatherMapService> openWeatherMapServiceMock;
        private readonly Mock<ITranslationService> translationServiceMock;

        public WeatherDisplayRenderingTests(ITestOutputHelper testOutputHelper)
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

            this.openWeatherMapServiceMock = this.autoMocker.GetMock<IOpenWeatherMapService>();
            this.openWeatherMapServiceMock.Setup(w => w.GetCurrentWeatherAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(WeatherInfos.GetTestWeatherInfo());
            this.openWeatherMapServiceMock.Setup(w => w.GetWeatherOneCallAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(OneCallWeatherInfos.GetTestWeatherInfo());
            this.openWeatherMapServiceMock.Setup(w => w.GetWeatherIconAsync(It.IsAny<WeatherCondition>(), It.IsAny<IWeatherIconMapping>()))
                .ReturnsAsync(Icons.Sun);

            this.translationServiceMock = this.autoMocker.GetMock<ITranslationService>();

            this.autoMocker.Use<IRenderService>(this.autoMocker.CreateInstance<RenderService>());
        }

        [Fact]
        public void ShouldRenderWeatherActions()
        {
            // Arrange
            var taskIds = new List<Guid>();
            var schedulerMock = this.autoMocker.GetMock<IScheduler>();
            schedulerMock.Setup(s => s.AddTask(It.IsAny<IScheduledTask>())).
                Callback<IScheduledTask>(t => { taskIds.Add(t.Id); });

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.AddWeatherRenderActions(this.openWeatherMapServiceMock.Object, this.translationServiceMock.Object, this.dateTimeMock.Object, this.appSettingsMock.Object);
            _ = displayManager.StartAsync();

            // Act
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            this.testHelper.WriteFile(bitmapStream);

            this.openWeatherMapServiceMock.Verify(w => w.GetCurrentWeatherAsync(It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(5));
        }

        [Fact]
        public void ShouldRenderWeatherActions_TemperatureChanges()
        {
            // Arrange
            this.openWeatherMapServiceMock.SetupSequence(w => w.GetCurrentWeatherAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(WeatherInfos.GetTestWeatherInfo(Temperature.FromCelsius(1.2f)))
                .ReturnsAsync(WeatherInfos.GetTestWeatherInfo(Temperature.FromCelsius(12.34f)))
                .ReturnsAsync(WeatherInfos.GetTestWeatherInfo(Temperature.FromCelsius(123.456f)))
                .ReturnsAsync(WeatherInfos.GetTestWeatherInfo(Temperature.FromCelsius(12.34f)))
                .ReturnsAsync(WeatherInfos.GetTestWeatherInfo(Temperature.FromCelsius(1.8f)))
                ;

            var taskIds = new List<Guid>();
            var schedulerMock = this.autoMocker.GetMock<IScheduler>();
            schedulerMock.Setup(s => s.AddTask(It.IsAny<IScheduledTask>())).
                Callback<IScheduledTask>(t => { taskIds.Add(t.Id); });

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.AddWeatherRenderActions(this.openWeatherMapServiceMock.Object, this.translationServiceMock.Object, this.dateTimeMock.Object, this.appSettingsMock.Object);
            _ = displayManager.StartAsync();

            // Act
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            this.testHelper.WriteFile(bitmapStream);

            this.openWeatherMapServiceMock.Verify(w => w.GetCurrentWeatherAsync(It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(5));
        }

        [Fact]
        public void ShouldRenderWeatherActions_DateTimeChanges_Hours()
        {
            // Arrange
            var startDateTime = new DateTime(2000, 01, 01, 0, 0, 0, DateTimeKind.Local);
            var endDateTime = startDateTime.AddDays(3);
            var numberOfHours = (int)endDateTime.Subtract(startDateTime).TotalHours;

            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            dateTimeMock.SetupSequence(d => d.Now, startDateTime, n => n.AddHours(1));

            var taskIds = new List<Guid>();
            var schedulerMock = this.autoMocker.GetMock<IScheduler>();
            schedulerMock.Setup(s => s.AddTask(It.IsAny<IScheduledTask>())).
                 Callback<IScheduledTask>(t => { taskIds.Add(t.Id); });

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.AddWeatherRenderActions(this.openWeatherMapServiceMock.Object, this.translationServiceMock.Object, dateTimeMock.Object, this.appSettingsMock.Object);
            _ = displayManager.StartAsync();

            // Act
            for (var i = 0; i < numberOfHours; i++)
            {
                schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            }

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldRenderWeatherActions_DateTimeChanges_Days()
        {
            // Arrange
            var beginOfYear = new DateTime(2000, 01, 01, 23, 59, 59, DateTimeKind.Local);
            var endOfYear = beginOfYear.AddYears(1).AddDays(-1);
            var numberOfDaysInYear = (int)endOfYear.Subtract(beginOfYear).TotalDays;

            var dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            var dateTimeSetupSequence = dateTimeMock.SetupSequence(d => d.Now);
            for (var i = 0; i <= numberOfDaysInYear; i++)
            {
                dateTimeSetupSequence.Returns(beginOfYear.AddDays(i));
            }

            var taskIds = new List<Guid>();
            var schedulerMock = this.autoMocker.GetMock<IScheduler>();
            schedulerMock.Setup(s => s.AddTask(It.IsAny<IScheduledTask>())).
                  Callback<IScheduledTask>(t => { taskIds.Add(t.Id); });

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.AddWeatherRenderActions(this.openWeatherMapServiceMock.Object, this.translationServiceMock.Object, dateTimeMock.Object, this.appSettingsMock.Object);
            _ = displayManager.StartAsync();

            // Act
            for (var i = 0; i < numberOfDaysInYear; i++)
            {
                schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            }

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            this.testHelper.WriteFile(bitmapStream);
        }
    }
}
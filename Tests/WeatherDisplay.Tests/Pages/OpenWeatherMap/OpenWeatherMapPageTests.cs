using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DisplayService;
using DisplayService.Devices;
using DisplayService.Services;
using DisplayService.Tests.Services;
using FluentAssertions;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.ReadResult;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using NCrontab.Scheduler;
using OpenWeatherMap;
using OpenWeatherMap.Models;
using SkiaSharp;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Pages.OpenWeatherMap;
using WeatherDisplay.Resources;
using WeatherDisplay.Services.Hardware;
using WeatherDisplay.Services.Navigation;
using WeatherDisplay.Tests.Extensions;
using WeatherDisplay.Tests.Testdata;
using Xunit;
using Xunit.Abstractions;
using IDateTime = DisplayService.Services.IDateTime;

namespace WeatherDisplay.Tests.Pages.OpenWeatherMap
{
    public class OpenWeatherMapPageTests
    {
        private readonly TestHelper testHelper;
        private readonly AutoMocker autoMocker;
        private readonly Mock<IDateTime> dateTimeMock;
        private readonly TestDisplay testDisplay;
        private readonly Mock<IOpenWeatherMapService> openWeatherMapServiceMock;

        public OpenWeatherMapPageTests(ITestOutputHelper testOutputHelper)
        {
            this.testHelper = new TestHelper(testOutputHelper);
            this.autoMocker = new AutoMocker();
            var openWeatherMapPageOptionsMock = this.autoMocker.GetMock<IOptionsMonitor<OpenWeatherMapPageOptions>>();
            openWeatherMapPageOptionsMock.Setup(o => o.CurrentValue)
                .Returns(new OpenWeatherMapPageOptions
                {
                    Places = new List<Place>
                    {
                        new Place
                        {
                            Name = "Test Place 1",
                            Latitude = 47.1111111d,
                            Longitude = 8.1111111d
                        },
                        new Place
                        {
                            Name = "Test Place 2",
                            Latitude = 47.2222222d,
                            Longitude = 8.2222222d
                        }
                    }
                });

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
            this.openWeatherMapServiceMock.Setup(w => w.GetWeatherOneCallAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<OneCallOptions>()))
                .ReturnsAsync(OneCallWeatherInfos.GetTestWeatherInfo());
            this.openWeatherMapServiceMock.Setup(w => w.GetWeatherIconAsync(It.IsAny<WeatherCondition>(), It.IsAny<IWeatherIconMapping>()))
                .ReturnsAsync(Icons.Sun);

            var bme680Mock = this.autoMocker.GetMock<IBme680>();
            bme680Mock.Setup(b => b.ReadAsync())
                .ReturnsAsync(new Bme680ReadResult(UnitsNet.Temperature.FromDegreesCelsius(20), null, UnitsNet.RelativeHumidity.FromPercent(50), null));

            var sensorAccessServiceMock = this.autoMocker.GetMock<ISensorAccessService>();
            sensorAccessServiceMock.SetupGet(f => f.Bme680)
                .Returns(bme680Mock.Object);

            var schedulerMock = this.autoMocker.GetMock<IScheduler>();

            var schedulerFactoryMock = this.autoMocker.GetMock<ISchedulerFactory>();
            schedulerFactoryMock.Setup(f => f.Create())
                .Returns(schedulerMock.Object);

            this.autoMocker.Use<IRenderService>(this.autoMocker.CreateInstance<RenderService>());
            this.autoMocker.Use<IDisplayManager>(this.autoMocker.CreateInstance<DisplayManager>());
        }

        [Fact]
        public async Task ShouldRenderWeatherActions()
        {
            // Arrange
            var taskIds = new List<Guid>();
            var schedulerMock = this.autoMocker.GetMock<IScheduler>();
            schedulerMock.Setup(s => s.AddTask(It.IsAny<IScheduledTask>())).
                Callback<IScheduledTask>(t => { taskIds.Add(t.Id); });

            INavigatedTo page = this.autoMocker.CreateInstance<OpenWeatherMapPage>();
            await page.OnNavigatedToAsync(parameters: null);

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            _ = displayManager.StartAsync();

            // Act
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);

            this.openWeatherMapServiceMock.Verify(w => w.GetWeatherOneCallAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<OneCallOptions>()), Times.Exactly(4));
            this.openWeatherMapServiceMock.Verify(w => w.GetWeatherIconAsync(It.IsAny<WeatherCondition>(), It.IsAny<IWeatherIconMapping>()), Times.Exactly(32));
            this.openWeatherMapServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRenderWeatherActions_WithMultiplePlaces()
        {
            // Arrange
            var taskIds = new List<Guid>();
            var schedulerMock = this.autoMocker.GetMock<IScheduler>();
            schedulerMock.Setup(s => s.AddTask(It.IsAny<IScheduledTask>())).
                Callback<IScheduledTask>(t => { taskIds.Add(t.Id); });

            INavigatedTo page = this.autoMocker.CreateInstance<OpenWeatherMapPage>();
            await page.OnNavigatedToAsync(parameters: null);

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            _ = displayManager.StartAsync();

            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));

            // Act
            for (var i = 0; i < 3; i++)
            {
                taskIds.Clear();
                await page.OnNavigatedToAsync(parameters: null);
                schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            }

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);

            this.openWeatherMapServiceMock.Verify(w => w.GetWeatherOneCallAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<OneCallOptions>()), Times.Exactly(4));
            this.openWeatherMapServiceMock.Verify(w => w.GetWeatherIconAsync(It.IsAny<WeatherCondition>(), It.IsAny<IWeatherIconMapping>()), Times.Exactly(32));
            this.openWeatherMapServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRenderWeatherActions_TemperatureChanges()
        {
            // Arrange
            this.openWeatherMapServiceMock.SetupSequence(w => w.GetWeatherOneCallAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<OneCallOptions>()))
                .ReturnsAsync(() => { var i = OneCallWeatherInfos.GetTestWeatherInfo(); i.CurrentWeather.Temperature = Temperature.FromCelsius(1.2f); return i; })
                .ReturnsAsync(() => { var i = OneCallWeatherInfos.GetTestWeatherInfo(); i.CurrentWeather.Temperature = Temperature.FromCelsius(12.34f); return i; })
                .ReturnsAsync(() => { var i = OneCallWeatherInfos.GetTestWeatherInfo(); i.CurrentWeather.Temperature = Temperature.FromCelsius(123.456f); return i; })
                .ReturnsAsync(() => { var i = OneCallWeatherInfos.GetTestWeatherInfo(); i.CurrentWeather.Temperature = Temperature.FromCelsius(12.34f); return i; })
                .ReturnsAsync(() => { var i = OneCallWeatherInfos.GetTestWeatherInfo(); i.CurrentWeather.Temperature = Temperature.FromCelsius(1.8f); return i; })
                ;

            var taskIds = new List<Guid>();
            var schedulerMock = this.autoMocker.GetMock<IScheduler>();
            schedulerMock.Setup(s => s.AddTask(It.IsAny<IScheduledTask>())).
                Callback<IScheduledTask>(t => { taskIds.Add(t.Id); });

            INavigatedTo page = this.autoMocker.CreateInstance<OpenWeatherMapPage>();
            await page.OnNavigatedToAsync(parameters: null);

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            _ = displayManager.StartAsync();

            // Act
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);

            this.openWeatherMapServiceMock.Verify(w => w.GetWeatherOneCallAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<OneCallOptions>()), Times.Exactly(4));
            this.openWeatherMapServiceMock.Verify(w => w.GetWeatherIconAsync(It.IsAny<WeatherCondition>(), It.IsAny<IWeatherIconMapping>()), Times.Exactly(32));
            this.openWeatherMapServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRenderWeatherActions_DateTimeChanges_Hours()
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

            INavigatedTo page = this.autoMocker.CreateInstance<OpenWeatherMapPage>();
            await page.OnNavigatedToAsync(parameters: null);

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            _ = displayManager.StartAsync();

            // Act
            for (var i = 0; i < numberOfHours; i++)
            {
                schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            }

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);

            this.openWeatherMapServiceMock.Verify(w => w.GetWeatherOneCallAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<OneCallOptions>()), Times.Exactly(72));
            this.openWeatherMapServiceMock.Verify(w => w.GetWeatherIconAsync(It.IsAny<WeatherCondition>(), It.IsAny<IWeatherIconMapping>()), Times.Exactly(576));
            this.openWeatherMapServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRenderWeatherActions_DateTimeChanges_Days()
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

            INavigatedTo page = this.autoMocker.CreateInstance<OpenWeatherMapPage>();
            await page.OnNavigatedToAsync(parameters: null);

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            _ = displayManager.StartAsync();

            // Act
            for (var i = 0; i < numberOfDaysInYear; i++)
            {
                schedulerMock.Raise(s => s.Next += null, new ScheduledEventArgs(DateTime.Now, taskIds.ToArray()));
            }

            // Assert
            var bitmapStream = this.testDisplay.GetDisplayImage();
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);

            this.openWeatherMapServiceMock.Verify(w => w.GetWeatherOneCallAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<OneCallOptions>()), Times.Exactly(365));
            this.openWeatherMapServiceMock.Verify(w => w.GetWeatherIconAsync(It.IsAny<WeatherCondition>(), It.IsAny<IWeatherIconMapping>()), Times.Exactly(2920));
            this.openWeatherMapServiceMock.VerifyNoOtherCalls();
        }
    }
}
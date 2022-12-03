using System;
using System.Collections.Generic;
using DisplayService.Services;
using DisplayService.Settings;
using DisplayService.Tests.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using NCrontab.Scheduler;
using OpenWeatherMap;
using OpenWeatherMap.Models;
using SkiaSharp;
using WeatherDisplay.Compilations;
using WeatherDisplay.Model;
using WeatherDisplay.Resources;
using WeatherDisplay.Tests.Extensions;
using WeatherDisplay.Tests.Testdata;
using Xunit;
using Xunit.Abstractions;
using IDateTime = DisplayService.Services.IDateTime;

namespace WeatherDisplay.Tests.Compilations
{
    public class OpenWeatherDisplayCompilationTests
    {
        private readonly TestHelper testHelper;
        private readonly AutoMocker autoMocker;
        private readonly Mock<IDateTime> dateTimeMock;
        private readonly TestDisplay testDisplay;
        private readonly Mock<IOpenWeatherMapService> openWeatherMapServiceMock;

        public OpenWeatherDisplayCompilationTests(ITestOutputHelper testOutputHelper)
        {
            this.testHelper = new TestHelper(testOutputHelper);
            this.autoMocker = new AutoMocker();
            var openWeatherDisplayCompilationOptionsMock = this.autoMocker.GetMock<IOptionsMonitor<OpenWeatherDisplayCompilationOptions>>();
            openWeatherDisplayCompilationOptionsMock.Setup(o => o.CurrentValue)
                .Returns(new OpenWeatherDisplayCompilationOptions
                {
                    Places = new List<Place>
                    {
                        new Place
                        {
                            Name = "Test Place",
                            Latitude = 47.1823761d,
                            Longitude = 8.4611036d
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

            this.autoMocker.Use<IRenderService>(this.autoMocker.CreateInstance<RenderService>());
            this.autoMocker.Use<IDisplayManager>(this.autoMocker.CreateInstance<DisplayManager>());
        }

        [Fact]
        public void ShouldRenderWeatherActions()
        {
            // Arrange
            var taskIds = new List<Guid>();
            var schedulerMock = this.autoMocker.GetMock<IScheduler>();
            schedulerMock.Setup(s => s.AddTask(It.IsAny<IScheduledTask>())).
                Callback<IScheduledTask>(t => { taskIds.Add(t.Id); });

            IDisplayCompilation displayCompilation = this.autoMocker.CreateInstance<OpenWeatherDisplayCompilation>();
            displayCompilation.AddRenderActions();

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.StartAsync();

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
        public void ShouldRenderWeatherActions_TemperatureChanges()
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

            IDisplayCompilation displayCompilation = this.autoMocker.CreateInstance<OpenWeatherDisplayCompilation>();
            displayCompilation.AddRenderActions();

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.StartAsync();

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

            IDisplayCompilation displayCompilation = this.autoMocker.CreateInstance<OpenWeatherDisplayCompilation>();
            displayCompilation.AddRenderActions();

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.StartAsync();

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

            IDisplayCompilation displayCompilation = this.autoMocker.CreateInstance<OpenWeatherDisplayCompilation>();
            displayCompilation.AddRenderActions();

            IDisplayManager displayManager = this.autoMocker.CreateInstance<DisplayManager>();
            displayManager.StartAsync();

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
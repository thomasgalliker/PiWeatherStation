using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DisplayService.Services;
using DisplayService.Settings;
using DisplayService.Tests.Services;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using SkiaSharp;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Resources;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.OpenWeatherMap;
using WeatherDisplay.Tests.Extensions;
using WeatherDisplay.Tests.Testdata;
using Xunit;
using Xunit.Abstractions;
using IDateTime = DisplayService.Services.IDateTime;

namespace WeatherDisplay.Tests
{
    public class TemperatureDiagramUnitTests
    {
        private readonly TestHelper testHelper;
        private readonly AutoMocker autoMocker;
        private readonly Mock<IAppSettings> appSettingsMock;
        private readonly Mock<IDateTime> dateTimeMock;
        private readonly TestDisplay testDisplay;
        private readonly Mock<IOpenWeatherMapService> openWeatherMapServiceMock;
        private readonly Mock<ITranslationService> translationServiceMock;

        public TemperatureDiagramUnitTests(ITestOutputHelper testOutputHelper)
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

            this.dateTimeMock = this.autoMocker.GetMock<IDateTime>();
            this.dateTimeMock.SetupGet(d => d.Now)
                .Returns(DateTime.Now);

            this.openWeatherMapServiceMock = this.autoMocker.GetMock<IOpenWeatherMapService>();
            this.openWeatherMapServiceMock.Setup(w => w.GetWeatherOneCallAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<OneCallOptions>()))
                .ReturnsAsync(OneCallWeatherInfos.GetTestWeatherInfo());
            this.openWeatherMapServiceMock.Setup(w => w.GetWeatherIconAsync(It.IsAny<WeatherCondition>(), It.IsAny<IWeatherIconMapping>()))
                .ReturnsAsync(Icons.Sun);

            this.translationServiceMock = this.autoMocker.GetMock<ITranslationService>();

            this.autoMocker.Use<IRenderService>(this.autoMocker.CreateInstance<RenderService>());
        }

        [Theory]
        [ClassData(typeof(SimpleTemperatureSetsTestData))]
        public void ShouldDrawSimpleTemperatureSets(TemperatureSet[] temperatureSets, DateTime now, string testName)
        {
            // Arrange
            var screen = new SKBitmap(800, 480);
            var canvas = new SKCanvas(screen);

            var temperatureDiagram = new TemperatureDiagram();

            Func<IEnumerable<TemperatureSet>, (Temperature Min, Temperature Max)> temperatureRangeSelector = (s) => { return (s.Min(x => x.Min) - 1, s.Max(x => x.Max) + 1); };
            Func<IEnumerable<float>, (float Min, float Max)> precipitationRangeSelector = (s) => { return (0f, s.Max() + 10); };

            var precipitation = temperatureSets.Select(t => (float)t.Rain).ToArray();

            var temperatureDiagramOptions = TemperatureDiagramOptions.Default;

            // Act
            temperatureDiagram.Draw(canvas, screen.Width, screen.Height, temperatureSets, precipitation, temperatureRangeSelector, now, temperatureDiagramOptions);

            // Assert
            var bitmapStream = screen.ToStream();
            this.testHelper.WriteFile(bitmapStream, $"{nameof(ShouldDrawSimpleTemperatureSets)}_{testName}");
        }

        internal class SimpleTemperatureSetsTestData : TheoryData<TemperatureSet[], DateTime, string>
        {
            public SimpleTemperatureSetsTestData()
            {
                this.Add(new[]
                {
                    new TemperatureSet{ DateTime = new DateTime(2000, 1, 1, 00, 00, 00), Min = Temperature.FromCelsius(9d), Avg = Temperature.FromCelsius(10d), Max = Temperature.FromCelsius(11d) },
                    new TemperatureSet{ DateTime = new DateTime(2000, 1, 2, 23, 59, 59), Min = Temperature.FromCelsius(9d), Avg = Temperature.FromCelsius(10d), Max = Temperature.FromCelsius(11d) },
                },
                new DateTime(2000, 1, 1, 12, 00, 00),
                "LinearStartToEnd");

                this.Add(new[]
                {
                    new TemperatureSet{ DateTime = new DateTime(2000, 1, 1, 12, 00, 00), Min = Temperature.FromCelsius(10d), Avg = Temperature.FromCelsius(11d), Max = Temperature.FromCelsius(12d) },
                    new TemperatureSet{ DateTime = new DateTime(2000, 1, 2, 12, 00, 00), Min = Temperature.FromCelsius(11d), Avg = Temperature.FromCelsius(12d), Max = Temperature.FromCelsius(13d) },
                },
                new DateTime(2000, 1, 1, 12, 00, 00),
                "LinearMidToMid");

                this.Add(new[]
                {
                    new TemperatureSet{ DateTime = new DateTime(2000, 1, 1, 06, 00, 00), Min = Temperature.FromCelsius(11d), Avg = Temperature.FromCelsius(11d), Max = Temperature.FromCelsius(11d) },
                    new TemperatureSet{ DateTime = new DateTime(2000, 1, 1, 12, 00, 00), Min = Temperature.FromCelsius(11d), Avg = Temperature.FromCelsius(11d), Max = Temperature.FromCelsius(11d) },
                    new TemperatureSet{ DateTime = new DateTime(2000, 1, 2, 06, 00, 00), Min = Temperature.FromCelsius(13d), Avg = Temperature.FromCelsius(14d), Max = Temperature.FromCelsius(15d) },
                    new TemperatureSet{ DateTime = new DateTime(2000, 1, 2, 12, 00, 00), Min = Temperature.FromCelsius(13d), Avg = Temperature.FromCelsius(14d), Max = Temperature.FromCelsius(15d) },
                },
                new DateTime(2000, 1, 2, 06, 00, 00),
                "VariableMinMax");
            }
        }
    }
}
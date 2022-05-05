using System.Net.Http;
using System.Threading.Tasks;
using DisplayService.Tests.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using Moq.Contrib.HttpClient;
using WeatherDisplay.Services;
using WeatherDisplay.Tests.Logging;
using WeatherDisplay.Tests.Testdata;
using Xunit;
using Xunit.Abstractions;

namespace WeatherDisplay.Tests.Services;

public class OpenWeatherMapServiceTests
{
    private readonly AutoMocker autoMocker;
    private readonly Mock<HttpMessageHandler> httpMessageHandlerMock;
    private readonly TestHelper testHelper;

    public OpenWeatherMapServiceTests(ITestOutputHelper testOutputHelper)
    {
        this.testHelper = new TestHelper(testOutputHelper);
        this.autoMocker = new AutoMocker();

        this.httpMessageHandlerMock = this.autoMocker.GetMock<HttpMessageHandler>();
        this.autoMocker.Use(this.httpMessageHandlerMock.CreateClient());

        this.autoMocker.Use<ILogger<OpenWeatherMapService>>(new TestOutputHelperLogger<OpenWeatherMapService>(testOutputHelper));

        var openWeatherMapConfigurationMock = this.autoMocker.GetMock<IOpenWeatherMapConfiguration>();
        openWeatherMapConfigurationMock.SetupGet(c => c.Language)
            .Returns("en");
        openWeatherMapConfigurationMock.SetupGet(c => c.ApiKey)
            .Returns("apikey");
        openWeatherMapConfigurationMock.SetupGet(c => c.UnitSystem)
            .Returns("metric");
    }

    [Fact]
    public async Task ShouldGetCurrentWeatherAsync()
    {
        // Arrange
        var latitude = 1.1111111111d;
        var longitude = 1.2222222222d;

        this.httpMessageHandlerMock.SetupRequest(HttpMethod.Get, r => r.RequestUri.LocalPath == "/data/2.5/weather")
            .ReturnsResponse(WeatherInfos.GetTestWeatherInfoJson(), "application/json");

        IOpenWeatherMapService openWeatherMapService = this.autoMocker.CreateInstance<OpenWeatherMapService>();

        // Act
        var weatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(latitude, longitude);

        // Assert
        weatherInfo.Should().NotBeNull();

        var expectedWeatherInfo = WeatherInfos.GetTestWeatherInfo();
        weatherInfo.Should().BeEquivalentTo(expectedWeatherInfo);

        this.httpMessageHandlerMock.VerifyRequest(HttpMethod.Get,
            "https://api.openweathermap.org:443/data/2.5/weather?lat=1.1111&lon=1.2222&units=metric&lang=en&appid=apikey",
            Times.Once());

        this.httpMessageHandlerMock.VerifyNoOtherCalls();
    }

    [Theory]
    [ClassData(typeof(OneCallTestData))]
    public async Task ShouldGetWeatherOneCallAsync(OneCallOptions oneCallOptions, string expectedUri)
    {
        // Arrange
        var latitude = 1.1111111111d;
        var longitude = 1.2222222222d;

        this.httpMessageHandlerMock.SetupRequest(HttpMethod.Get, r => r.RequestUri.LocalPath == "/data/2.5/onecall")
            .ReturnsResponse(OneCallWeatherInfos.GetTestWeatherInfoJson(), "application/json");

        IOpenWeatherMapService openWeatherMapService = this.autoMocker.CreateInstance<OpenWeatherMapService>();

        // Act
        var oneCallWeatherInfo = await openWeatherMapService.GetWeatherOneCallAsync(latitude, longitude, oneCallOptions);

        // Assert
        oneCallWeatherInfo.Should().NotBeNull();

        var expectedWeatherInfo = OneCallWeatherInfos.GetTestWeatherInfo();
        oneCallWeatherInfo.Should().BeEquivalentTo(expectedWeatherInfo);

        this.httpMessageHandlerMock.VerifyRequest(HttpMethod.Get,
            expectedUri,
            Times.Once());

        this.httpMessageHandlerMock.VerifyNoOtherCalls();
    }

    public class OneCallTestData : TheoryData<OneCallOptions, string>
    {
        public OneCallTestData()
        {
            this.Add(
                OneCallOptions.Default,
                "https://api.openweathermap.org:443/data/2.5/onecall?lat=1.1111&lon=1.2222&units=metric&lang=en&appid=apikey");

            this.Add(new OneCallOptions
            {
                IncludeCurrentWeather = false,
                IncludeMinutelyForecasts = false,
                IncludeHourlyForecasts = false,
                IncludeDailyForecasts = true,
            },
            "https://api.openweathermap.org:443/data/2.5/onecall?lat=1.1111&lon=1.2222&exclude=current,minutely,hourly&units=metric&lang=en&appid=apikey");
        }
    }
    [Fact]
    public async Task ShouldGetAirPollutionAsync()
    {
        // Arrange
        var latitude = 47.0907124d;
        var longitude = 8.0559381d;

        var logger = new NullLogger<OpenWeatherMapService>();
        var configuration = new OpenWeatherMapConfiguration
        {
            ApiKey = "001c4dffbe586e8e2542fb379031bc99",
            UnitSystem = "metric",
            Language = "de",
            VerboseLogging = true
        };

        IOpenWeatherMapService openWeatherMapService = new OpenWeatherMapService(logger, configuration);

        // Act
        var pollutionInfo = await openWeatherMapService.GetAirPollutionAsync(latitude, longitude);

        // Assert
        pollutionInfo.Should().NotBeNull();

        var expectedWeatherInfo = OneCallWeatherInfos.GetTestWeatherInfo();
        pollutionInfo.Should().BeEquivalentTo(expectedWeatherInfo);

    }
}
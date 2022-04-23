using System.Net.Http;
using System.Threading.Tasks;
using DisplayService.Tests.Services;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Moq.Contrib.HttpClient;
using WeatherDisplay.Services;
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
    }

    [Fact]
    public async Task ShouldGetWeatherOneCallAsync()
    {
        // Arrange
        var latitude = 1.1111111111d;
        var longitude = 1.2222222222d;

        this.httpMessageHandlerMock.SetupRequest(HttpMethod.Get, r => r.RequestUri.LocalPath == "/data/2.5/onecall")
            .ReturnsResponse(OneCallWeatherInfos.GetTestWeatherInfoJson(), "application/json");

        IOpenWeatherMapService openWeatherMapService = this.autoMocker.CreateInstance<OpenWeatherMapService>();

        // Act
        var oneCallWeatherInfo = await openWeatherMapService.GetWeatherOneCallAsync(latitude, longitude);

        // Assert
        oneCallWeatherInfo.Should().NotBeNull();
        
        var expectedWeatherInfo = OneCallWeatherInfos.GetTestWeatherInfo();
        oneCallWeatherInfo.Should().BeEquivalentTo(expectedWeatherInfo);

        this.httpMessageHandlerMock.VerifyRequest(HttpMethod.Get,
            "https://api.openweathermap.org:443/data/2.5/onecall?lat=1.1111&lon=1.2222&exclude=current,minutely,hourly&units=metric&lang=en&appid=apikey",
            Times.Once());
    }
}
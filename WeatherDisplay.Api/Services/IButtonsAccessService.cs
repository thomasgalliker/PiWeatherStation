namespace WeatherDisplay.Api.Services
{
    public interface IButtonsAccessService
    {
        Task HandleButtonPress(int buttonId);
        
        Task HandleButtonHolding(int buttonId);
    }
}
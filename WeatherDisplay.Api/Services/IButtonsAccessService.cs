namespace WeatherDisplay.Api.Services
{
    public interface IButtonsAccessService
    {
        void InitializeButtons();

        Task HandleButtonPress(int buttonId);
        
        Task HandleButtonHolding(int buttonId);
    }
}
namespace WeatherDisplay.Api.Services.Hardware
{
    public interface IButtonsAccessService
    {
        void InitializeButtons();

        Task HandleButtonPress(int buttonId);

        Task HandleButtonHolding(int buttonId);
    }
}
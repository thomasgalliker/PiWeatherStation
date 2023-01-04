using System.Threading.Tasks;

namespace WeatherDisplay.Services.Hardware
{
    public interface IButtonsAccessService
    {
        void Initialize();

        Task HandleButtonPress(int buttonId);

        Task HandleButtonHolding(int buttonId);
    }
}
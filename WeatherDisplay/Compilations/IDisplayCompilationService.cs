using System.Threading.Tasks;

namespace WeatherDisplay.Compilations
{
    public interface IDisplayCompilationService
    {
        Task SelectDisplayCompilationAsync(string name);
    }
}
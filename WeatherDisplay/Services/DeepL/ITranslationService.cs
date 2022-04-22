using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherDisplay.Services
{
    public interface ITranslationService
    {
        Task<IEnumerable<string>> Translate(string targetLanguage, params string[] texts);

        Task<IEnumerable<string>> Translate(string sourceLanguage, string targetLanguage, params string[] texts);
    }
}
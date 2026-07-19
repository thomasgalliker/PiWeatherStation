using System.Text.Json.Serialization;

namespace WeatherDisplay.Model.DeepL
{
    public class TranslationResult
    {
        public TranslationResult()
        {
            this.Translations = new List<Translation>();
        }

        [JsonPropertyName("translations")]
        public IEnumerable<Translation> Translations { get; set; }
    }
}

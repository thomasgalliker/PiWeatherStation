using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.DeepL
{
    public class TranslationResult
    {
        public TranslationResult()
        {
            this.Translations = new List<Translation>();
        }

        [JsonProperty("translations")]
        public IEnumerable<Translation> Translations { get; set; }
    }
}
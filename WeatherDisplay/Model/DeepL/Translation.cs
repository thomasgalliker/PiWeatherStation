using Newtonsoft.Json;

namespace WeatherDisplay.Model.DeepL
{
    public class Translation
    {
        [JsonProperty("detected_source_language")]
        public string DetectedSourceLanguage { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
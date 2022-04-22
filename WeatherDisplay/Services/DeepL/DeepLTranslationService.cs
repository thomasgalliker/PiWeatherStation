using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherDisplay.Model.DeepL;

namespace WeatherDisplay.Services
{
    public sealed class DeepLTranslationService : ITranslationService
    {
        private const string ProApiBaseUrl = "https://api.deepl.com/v2/translate";
        private const string FreeApiBaseUrl = "https://api-free.deepl.com/v2/translate";

        private readonly string apiBaseUrl;

        private readonly HttpClient httpClient;
        private readonly string authKey;

        public DeepLTranslationService(IDeepLTranslationConfiguration deepLTranslationConfiguration)
            : this(new HttpClient(), deepLTranslationConfiguration)
        {
        }

        public DeepLTranslationService(HttpClient httpClient, IDeepLTranslationConfiguration deepLTranslationConfiguration)
        {
            this.httpClient = httpClient;

            this.authKey = deepLTranslationConfiguration.AuthKey;
            this.apiBaseUrl = (this.authKey.EndsWith(":fx", StringComparison.OrdinalIgnoreCase) ? FreeApiBaseUrl : ProApiBaseUrl) + "?auth_key=" + this.authKey;
        }

        public Task<IEnumerable<string>> Translate(string targetLanguageCode, params string[] texts)
        {
            return this.Translate(null, targetLanguageCode, texts);
        }

        public async Task<IEnumerable<string>> Translate(string sourceLanguageCode, string targetLanguageCode, params string[] texts)
        {
            if (texts == null)
            {
                throw new ArgumentNullException(nameof(texts));
            }

            if (texts.Count() == 0)
            {
                throw new ArgumentException($"{nameof(texts)} must not be empty", nameof(texts));
            }

            var parameters = this.GetParameters(texts, sourceLanguageCode, targetLanguageCode);

            using (HttpContent httpContent = new FormUrlEncodedContent(parameters))
            {
                var httpResponseMessage = await this.httpClient.PostAsync(this.apiBaseUrl, httpContent);
                httpResponseMessage.EnsureSuccessStatusCode();

                var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                var translationResult = JsonConvert.DeserializeObject<TranslationResult>(jsonResponse);
                return translationResult.Translations.Select(x => x.Text ?? "");
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetParameters(IEnumerable<string> texts, string sourceLanguageCode, string targetLanguageCode)
        {
            var parameters = texts.Select(x => Kvp("text", x)).ToList();
            parameters.Add(Kvp("target_lang", targetLanguageCode?.ToUpperInvariant() ?? throw new ArgumentNullException(nameof(targetLanguageCode))));
            parameters.Add(Kvp("split_sentences", "1"));
            parameters.Add(Kvp("preserve_formatting", "1"));
            parameters.Add(Kvp("auth_key", this.authKey));

            if (!string.IsNullOrWhiteSpace(sourceLanguageCode))
            {
                parameters.Add(Kvp("source_lang", sourceLanguageCode.ToUpperInvariant()));
            }

            return parameters;
        }

        private static KeyValuePair<string, string> Kvp(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }
    }
}
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace WeatherDisplay.Api.Services.Configuration
{
    public class WritableOptions<T> : IWritableOptions<T> where T : class, new()
    {
        private readonly IWebHostEnvironment environment;
        private readonly IOptionsMonitor<T> options;
        private readonly string section;
        private readonly string file;
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public WritableOptions(
            IWebHostEnvironment environment,
            IOptionsMonitor<T> options,
            string section,
            string file)
        {
            this.environment = environment;
            this.options = options;
            this.section = section;
            this.file = file;

            this.jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented,
            };
            this.jsonSerializerSettings.Converters.Add(new StringEnumConverter());
        }

        public T Value => this.options.CurrentValue;

        public T Get(string name) => this.options.Get(name);

        public void UpdateProperty<TValue>(Expression<Func<T, TValue>> propertySelector, TValue value)
        {
            var fileProvider = this.environment.ContentRootFileProvider;
            var fileInfo = fileProvider.GetFileInfo(this.file);
            var jObject = GetJsonContent(fileInfo);

            var propertyUpdater = PropertyUpdater<T, TValue>.GetPropertyUpdater(() => propertySelector);

            var sectionObject = jObject[this.section];
            if (sectionObject == null)
            {
                sectionObject = new JObject();
                jObject[this.section] = sectionObject;
            }

            sectionObject[propertyUpdater.Name] = JToken.FromObject(value);

            var updatedFileContent = JsonConvert.SerializeObject(jObject, this.jsonSerializerSettings);
            File.WriteAllText(fileInfo.PhysicalPath, updatedFileContent);
        }

        public void Update(Action<T> options)
        {
            this.Update(t => options(t));
        }

        public void Update(Func<T, T> options)
        {
            var fileProvider = this.environment.ContentRootFileProvider;
            var fileInfo = fileProvider.GetFileInfo(this.file);
            var jObject = GetJsonContent(fileInfo);

            var sectionObject = this.DeserializeSection(jObject);

            sectionObject = options(sectionObject);

            jObject[this.section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject, this.jsonSerializerSettings));

            var updatedFileContent = JsonConvert.SerializeObject(jObject, this.jsonSerializerSettings);
            File.WriteAllText(fileInfo.PhysicalPath, updatedFileContent);
        }

        private static JObject GetJsonContent(Microsoft.Extensions.FileProviders.IFileInfo fileInfo)
        {
            JObject jObject;
            if (fileInfo.Exists)
            {
                var fileContent = File.ReadAllText(fileInfo.PhysicalPath);
                jObject = JsonConvert.DeserializeObject<JObject>(fileContent);
            }
            else
            {
                jObject = new JObject();
            }

            return jObject;
        }

        private T DeserializeSection(JObject jObject)
        {
            T sectionObject;
            if (jObject.TryGetValue(this.section, out var section))
            {
                sectionObject = JsonConvert.DeserializeObject<T>(section.ToString(), this.jsonSerializerSettings);
            }
            else
            {
                sectionObject = this.Value ?? new T();
            }

            return sectionObject;
        }
    }
}
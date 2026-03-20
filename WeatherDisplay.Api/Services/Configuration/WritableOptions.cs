using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace WeatherDisplay.Api.Services.Configuration
{
    public class WritableOptions<T> : IWritableOptions<T> where T : class, new()
    {
        private readonly IWebHostEnvironment environment;
        private readonly IOptionsMonitor<T> options;
        private readonly string section;
        private readonly string file;
        private readonly JsonSerializerOptions jsonSerializerOptions;

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

            this.jsonSerializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                WriteIndented = true,
            };
            this.jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public T Value => this.options.CurrentValue;

        public T Get(string name) => this.options.Get(name);

        public void UpdateProperty<TValue>(Expression<Func<T, TValue>> propertySelector, TValue value)
        {
            var fileProvider = this.environment.ContentRootFileProvider;
            var fileInfo = fileProvider.GetFileInfo(this.file);
            var jsonObject = GetJsonContent(fileInfo);

            var propertyUpdater = PropertyUpdater<T, TValue>.GetPropertyUpdater(() => propertySelector);

            if (jsonObject[this.section] is not JsonObject sectionObject)
            {
                sectionObject = [];
                jsonObject[this.section] = sectionObject;
            }

            sectionObject[propertyUpdater.Name] = JsonSerializer.SerializeToNode(value, this.jsonSerializerOptions);

            var updatedFileContent = jsonObject.ToJsonString(this.jsonSerializerOptions);
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
            var jsonObject = GetJsonContent(fileInfo);

            var sectionObject = this.DeserializeSection(jsonObject);

            sectionObject = options(sectionObject);

            jsonObject[this.section] = JsonSerializer.SerializeToNode(sectionObject, this.jsonSerializerOptions);

            var updatedFileContent = jsonObject.ToJsonString(this.jsonSerializerOptions);
            File.WriteAllText(fileInfo.PhysicalPath, updatedFileContent);
        }

        private static JsonObject GetJsonContent(Microsoft.Extensions.FileProviders.IFileInfo fileInfo)
        {
            if (fileInfo.Exists)
            {
                var fileContent = File.ReadAllText(fileInfo.PhysicalPath);
                var node = JsonNode.Parse(fileContent);
                return node as JsonObject ?? [];
            }

            return [];
        }

        private T DeserializeSection(JsonObject jsonObject)
        {
            T sectionObject;
            if (jsonObject.TryGetPropertyValue(this.section, out var section) && section != null)
            {
                sectionObject = section.Deserialize<T>(this.jsonSerializerOptions);
            }
            else
            {
                sectionObject = this.Value ?? new T();
            }

            return sectionObject;
        }
    }
}

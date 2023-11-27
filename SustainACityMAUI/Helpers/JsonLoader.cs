using System.Reflection;
using System.Text.Json;

namespace SustainACityMAUI.Helpers;

public class JsonLoader
{
    private readonly string _resourceName;
    private readonly Dictionary<string, Type> _questTypes;

    public JsonLoader(string resourceName, Dictionary<string, Type> questTypes = null)
    {
        _resourceName = $"SustainACityMAUI.Resources.Data.{resourceName}";
        _questTypes = questTypes ?? new Dictionary<string, Type>();
    }

    public List<T> LoadData<T>()
    {
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(JsonLoader)).Assembly;
        using Stream stream = assembly.GetManifestResourceStream(_resourceName) ?? throw new InvalidOperationException($"Resource not found: {_resourceName}");
        using StreamReader reader = new(stream);

        string json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (_questTypes.Count > 0)
        {
            options.Converters.Add(new QuestConverter(_questTypes));
        }

        var data = JsonSerializer.Deserialize<List<T>>(json, options);
        return data ?? throw new InvalidOperationException($"Could not load data: {_resourceName}");
    }
}
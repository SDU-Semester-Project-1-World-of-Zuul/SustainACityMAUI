using System.Text.Json;
using System.Reflection;

namespace SustainACityMAUI.Helpers;

public class JsonLoader
{
    private readonly string _resourceName;

    public JsonLoader(string resourceName)
    {
        _resourceName = $"SustainACityMAUI.Resources.Data.{resourceName}";
    }

    public List<T> LoadData<T>()
    {
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(JsonLoader)).Assembly;
        using Stream stream = assembly.GetManifestResourceStream(_resourceName) ?? throw new InvalidOperationException($"Resource not found: {_resourceName}");
        using StreamReader reader = new(stream);

        string json = reader.ReadToEnd();
        var data = JsonSerializer.Deserialize<List<T>>(json);
        return data ?? throw new InvalidOperationException($"Could not load data: {_resourceName}");
    }
}
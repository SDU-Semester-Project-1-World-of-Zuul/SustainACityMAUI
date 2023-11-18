using SustainACityMAUI.Models;
using System.Text.Json;
using System.Reflection;

namespace SustainACityMAUI.Helpers;

public class JsonLoader
{
    private readonly string _resourceName;

    public JsonLoader(string resourceName)
    {
        _resourceName = "SustainACityMAUI.Resources.Data." + resourceName;
    }

    public List<Room> LoadRooms()
    {
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(JsonLoader)).Assembly;
        using Stream stream = assembly.GetManifestResourceStream(_resourceName) ?? throw new InvalidOperationException($"Resource not found: {_resourceName}");
        using StreamReader reader = new(stream);

        string json = reader.ReadToEnd();
        var rooms = JsonSerializer.Deserialize<List<Room>>(json);
        return rooms ?? throw new InvalidOperationException($"Could not load rooms: {_resourceName}");
    }
}
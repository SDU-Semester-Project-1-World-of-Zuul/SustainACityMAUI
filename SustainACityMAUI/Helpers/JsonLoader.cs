using SustainACityMAUI.Models;
using System.Text.Json;
using System.Reflection;
using System.IO;

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
        try
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(JsonLoader)).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(_resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Resource {_resourceName} not found. Ensure it's set as an EmbeddedResource and the name is correct.");
                }
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    var rooms = JsonSerializer.Deserialize<List<Room>>(json);
                    return rooms ?? new List<Room>();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading rooms: {ex.Message}");
            return new List<Room>();
        }
    }
}
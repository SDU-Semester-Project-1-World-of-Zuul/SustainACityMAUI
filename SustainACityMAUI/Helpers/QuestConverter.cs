using System.Text.Json;
using System.Text.Json.Serialization;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.Helpers;

public class QuestConverter : JsonConverter<Quest>
{
    private readonly Dictionary<string, Type> _questTypes;

    public QuestConverter(Dictionary<string, Type> questTypes)
    {
        _questTypes = questTypes;
    }

    public override Quest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        var type = root.GetProperty("Type").GetString();

        if (_questTypes.TryGetValue(type, out var questType))
        {
            return JsonSerializer.Deserialize(root.GetRawText(), questType, options) as Quest;
        }
        else
        {
            throw new JsonException($"Unknown quest type: {type}");
        }
    }

    public override void Write(Utf8JsonWriter writer, Quest value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
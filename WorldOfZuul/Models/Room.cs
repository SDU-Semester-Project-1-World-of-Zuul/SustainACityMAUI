using System.Text.Json;

namespace WorldOfZuul.Models;

public class Room
{
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public (int X, int Y) Coordinates => (X, Y);

    public static List<Room> LoadRooms(string filePath)
    {
        string jsonText = File.ReadAllText(filePath);
        JsonSerializerOptions options = new(){ PropertyNameCaseInsensitive = true };

        return JsonSerializer.Deserialize<List<Room>>(jsonText, options);
    }
}
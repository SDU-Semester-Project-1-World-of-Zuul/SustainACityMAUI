using System.Text.Json;

namespace SustainACityMAUI.Models;

public class RoomLoader
{
    private readonly string _filePath;

    public RoomLoader(string filePath)
    {
        _filePath = filePath;
    }

    public Dictionary<(int, int), Room> LoadRooms()
    {
        string jsonText = File.ReadAllText(_filePath);
        List<Room> rooms = JsonSerializer.Deserialize<List<Room>>(jsonText);
        return rooms.ToDictionary(room => room.Coordinates, room => room);
    }
}
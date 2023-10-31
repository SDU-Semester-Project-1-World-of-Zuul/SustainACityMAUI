using System.Text.Json;

namespace SustainACityMAUI.Models;

/// <summary> Loads room data from a json file. </summary>
public class RoomLoader
{
    private readonly string _filePath;

    /// <summary> Initializes with the path to the room data file. </summary>
    public RoomLoader(string filePath)
    {
        _filePath = filePath;
    }

    /// <summary> Reads and deserializes rooms from the file. </summary>
    public Dictionary<(int, int), Room> LoadRooms()
    {
        // Read file content
        string jsonText = File.ReadAllText(_filePath);

        // Deserialize rooms
        List<Room> rooms = JsonSerializer.Deserialize<List<Room>>(jsonText);

        // Convert to dictionary
        return rooms.ToDictionary(room => room.Coordinates, room => room);
    }
}
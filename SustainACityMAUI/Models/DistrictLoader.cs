using System.Text.Json;

namespace SustainACityMAUI.Models;

/// <summary> Loads District data from a json file. </summary>
public class DistrictLoader
{
    private readonly string _filePath;

    /// <summary> Initializes with the path to the District data file. </summary>
    public DistrictLoader(string filePath)
    {
        _filePath = filePath;
    }

    /// <summary> Reads and deserializes Districts from the file. </summary>
    public Dictionary<(int, int), District> LoadDistricts()
    {
        // Read file content
        string jsonText = File.ReadAllText(_filePath);

        // Deserialize Districts
        List<District> districts = JsonSerializer.Deserialize<List<District>>(jsonText);

        // Convert to dictionary
        return districts.ToDictionary(district => district.Coordinates, district => district);
    }
}
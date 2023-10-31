namespace SustainACityMAUI.Models;

/// <summary> Represents a location in the game. </summary>
public class Room
{
    public string ShortDescription { get; set; } // Brief room info
    public string LongDescription { get; set; }  // Detailed room info
    public int X { get; set; }                   // Horizontal coordinate
    public int Y { get; set; }                   // Vertical coordinate
    public (int X, int Y) Coordinates => (X, Y); // Room's (X, Y) position
    public NPC Resident { get; set; }            // Room's resident NPC
}
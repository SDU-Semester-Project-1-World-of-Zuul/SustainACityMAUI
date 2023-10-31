namespace SustainACityMAUI.Models;

/// <summary> Represents a location in the game. </summary>
public class District
{
    public string ShortDescription { get; set; } // Brief District info
    public string LongDescription { get; set; }  // Detailed District info
    public int X { get; set; }                   // Horizontal coordinate
    public int Y { get; set; }                   // Vertical coordinate
    public (int X, int Y) Coordinates => (X, Y); // District's (X, Y) position
    public NPC Resident { get; set; }            // District's resident NPC
}
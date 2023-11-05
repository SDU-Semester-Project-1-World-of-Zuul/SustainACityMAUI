namespace SustainACityMAUI.Models;

/// <summary> Represents the stats of the player. </summary>
public class Player
{
    public string Name { get; set; }
    public Room CurrentRoom { get; set; }
    public Stack<(int X, int Y)> MovementHistory { get; } = new Stack<(int X, int Y)>();
    public int Score { get; set; }
    // Player-specific methods and properties
}
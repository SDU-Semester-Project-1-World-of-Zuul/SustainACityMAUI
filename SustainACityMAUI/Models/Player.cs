namespace SustainACityMAUI.Models;

/// <summary> Represents the stats of the player. </summary>
public class Player
{
    public string Name { get; set; }
    public Room CurrentRoom { get; set; }
    public NPC CurrentNPC { get; set; }
    public Stack<(int X, int Y)> MovementHistory { get; } = new Stack<(int X, int Y)>();
    public List<Item> Inventory { get; } = new List<Item>();
    public int Score { get; set; }
}
namespace WorldOfZuul.Models;

public class Room
{
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public (int X, int Y) Coordinates => (X, Y);
    public NPC Resident { get; set; }
}
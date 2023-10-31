namespace SustainACityMAUI.Models;

/// <summary> Represents a non-player character in the game. </summary>
public class NPC
{
    public string Name { get; set; }    // NPC's name.
    public Quest Quest { get; set; }    //  Quest associated with the NPC.
}
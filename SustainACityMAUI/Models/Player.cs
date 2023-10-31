namespace SustainACityMAUI.Models;

/// <summary> Represents the stats of the player. </summary>
public class Player
{
    public District CurrentDistrict { get; set; }   // The District the player is currently in.
    public District PreviousDistrict { get; set; }  // The District the player was in previously.
    public int Score { get; set; }          // The player's current score.

    // Add other player modifiers here
}
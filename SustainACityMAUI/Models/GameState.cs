namespace SustainACityMAUI.Models;

/// <summary> Represents the current state of the game. </summary>
public class GameState
{
    public Room CurrentRoom { get; set; }   // The room the player is currently in.
    public Room PreviousRoom { get; set; }  // The room the player was in previously.
    public int Score { get; set; }          // The player's current score.

    // Add other game modifiers here
}
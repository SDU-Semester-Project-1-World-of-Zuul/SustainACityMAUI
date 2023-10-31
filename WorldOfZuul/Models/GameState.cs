
namespace WorldOfZuul.Models;

public class GameState
{
    public Room CurrentRoom { get; set; }
    public Room PreviousRoom { get; set; }
    public int Score { get; set; }

    // Add other game modifiers here
}
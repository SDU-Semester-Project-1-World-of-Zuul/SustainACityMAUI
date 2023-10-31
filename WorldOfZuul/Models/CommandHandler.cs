namespace WorldOfZuul.Models;

public class CommandHandler
{
    private readonly GameState _gameState;
    private readonly Dictionary<(int, int), Room> _roomMap;
    private readonly Dictionary<string, Func<string>> _commandActions;

    public CommandHandler(GameState gameState, Dictionary<(int, int), Room> roomMap)
    {
        _gameState = gameState;
        _roomMap = roomMap;

        _commandActions = new()
            {
                {"look", () => $"\n{_gameState.CurrentRoom.LongDescription}"},
                {"back", Return},
                {"north", () => Move(0, -1, "north")},
                {"south", () => Move(0, 1, "south")},
                {"east", () => Move(1, 0, "east")},
                {"west", () => Move(-1, 0, "west")},
                {"help", CommandHandler.Help}
            };
    }

    public string Handle(string userInput)
    {
        return _commandActions.ContainsKey(userInput) ? _commandActions[userInput].Invoke() : "\nI don't know that command.";
    }
    public static string Help()
    {
        return "\nYou are lost. You are alone. You wander" +
               "\naround the university.\n" +
               "\nNavigate by typing 'north', 'south', 'east', or 'west'." +
               "\nType 'look' for more details." +
               "\nType 'back' to go to the previous room." +
               "\nType 'help' to print this message again.";
    }

    private string Move(int x, int y, string direction)
    {
        var newCoordinates = (_gameState.CurrentRoom.Coordinates.X + x, _gameState.CurrentRoom.Coordinates.Y + y);

        if (_roomMap.ContainsKey(newCoordinates))
        {
            _gameState.PreviousRoom = _gameState.CurrentRoom;
            _gameState.CurrentRoom = _roomMap[newCoordinates];

            return $"You moved {direction}.\n{_gameState.CurrentRoom.ShortDescription}";
        }
        return $"You can't go '{direction}'!";
    }

    private string Return()
    {
        if (_gameState.PreviousRoom != null)
        {
            _gameState.CurrentRoom = _gameState.PreviousRoom;
            return $"\nYou returned to the previous room.\n{_gameState.CurrentRoom.ShortDescription}";
        }
        else
        {
            return "\nYou cannot go back from here!";
        }
    }
}

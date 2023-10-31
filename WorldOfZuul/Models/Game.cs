namespace WorldOfZuul.Models;

public class Game
{
    private readonly GameState _gameState;
    private readonly Dictionary<(int, int), Room> roomMap = new();
    private readonly Dictionary<string, Func<string>> commandActions;

    public Game()
    {
        // Load rooms from JSON file
        string roomsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rooms.json");
        List<Room> rooms = Room.LoadRooms(roomsFilePath);

        // Populate the roomMap dictionary with the loaded rooms
        roomMap = rooms.ToDictionary(room => room.Coordinates, room => room);

        // Set the initial current room
        _gameState = new() { CurrentRoom = roomMap[(0, 0)] };

        // List of game commands and their actions
        commandActions = new()
        {
            {"look", () => $"\n{_gameState.CurrentRoom.LongDescription}"},
            {"back", Return},
            {"north", () => Move(0, -1, "north")},
            {"south", () => Move(0, 1, "south")},
            {"east", () => Move(1, 0, "east")},
            {"west", () => Move(-1, 0, "west")},
            {"help", Help}
        };
    }

    public string ExecuteCommand(string userInput)
    {
        if (commandActions.ContainsKey(userInput))
        {
            return commandActions[userInput].Invoke();
        }
        else
        {
            return "\nI don't know that command.";
        }
    }

    public string Welcome()
    {
        return "\nWelcome to the World of Zuul!" +
               "\nWorld of Zuul is a new, incredibly boring adventure game." +
               $"{Help()}\n";
    }

    private string Help()
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

        if (roomMap.ContainsKey(newCoordinates))
        {
            _gameState.PreviousRoom = _gameState.CurrentRoom;
            _gameState.CurrentRoom = roomMap[newCoordinates];

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
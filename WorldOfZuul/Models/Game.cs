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
        List<Room> rooms = Room.LoadRoomsFromJson(roomsFilePath);

        // Populate the roomMap dictionary with the loaded rooms
        roomMap = rooms.ToDictionary(room => room.Coordinates, room => room);

        // Set the initial current room
        _gameState = new GameState { CurrentRoom = roomMap[(0, 0)] };

        // List of game commands and their actions
        commandActions = new Dictionary<string, Func<string>>
        {
            {"look", () => $"\n{_gameState.CurrentRoom.LongDescription}"},
            {"back", GoBack},
            {"north", () => Move("north")},
            {"south", () => Move("south")},
            {"east", () => Move("east")},
            {"west", () => Move("west")},
            {"help", PrintHelp}
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

    public string PrintWelcome()
    {
        return "\nWelcome to the World of Zuul!" +
               "\nWorld of Zuul is a new, incredibly boring adventure game." +
               $"{PrintHelp()}\n";
    }

    private string PrintHelp()
    {
        return "\nYou are lost. You are alone. You wander" +
               "\naround the university.\n" +
               "\nNavigate by typing 'north', 'south', 'east', or 'west'." +
               "\nType 'look' for more details." +
               "\nType 'back' to go to the previous room." +
               "\nType 'help' to print this message again.";
    }

    private string Move(string direction)
    {
        int x = _gameState.CurrentRoom.Coordinates.X;
        int y = _gameState.CurrentRoom.Coordinates.Y;

        switch (direction)
        {
            case "north":
                y--;
                break;
            case "south":
                y++;
                break;
            case "east":
                x++;
                break;
            case "west":
                x--;
                break;
        }

        var targetCoordinates = (x, y);

        if (roomMap.ContainsKey(targetCoordinates))
        {
            _gameState.PreviousRoom = _gameState.CurrentRoom;
            _gameState.CurrentRoom = roomMap[targetCoordinates];

            return $"You moved {direction}.\n{_gameState.CurrentRoom.ShortDescription}";
        }
        else
        {
            return $"You can't go '{direction}'!";
        }
    }


    private string GoBack()
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
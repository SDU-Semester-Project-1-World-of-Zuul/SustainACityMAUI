namespace WorldOfZuul.Models
{
    public class Game
    {
        private GameState _gameState;
        private XMLRoomLoader _roomLoader;
        private readonly Dictionary<string, Func<string>> commandActions;

        public Game()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string roomsFilePath = System.IO.Path.Combine(appDirectory, "Rooms.xml");
            _roomLoader = new XMLRoomLoader(roomsFilePath);
            var rooms = _roomLoader.GetRooms();
            _gameState = new GameState { CurrentRoom = rooms["Outside"] };

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
            return $"\nWelcome to the World of Zuul!" +
                   $"\nWorld of Zuul is a new, incredibly boring adventure game." +
                   $"{PrintHelp()}\n";
        }

        private string PrintHelp()
        {
            return $"\nYou are lost. You are alone. You wander" +
                   $"\naround the university.\n" +
                   $"\nNavigate by typing 'north', 'south', 'east', or 'west'." +
                   $"\nType 'look' for more details." +
                   $"\nType 'back' to go to the previous room." +
                   $"\nType 'help' to print this message again.";
        }

        private string Move(string direction)
        {
            if (_gameState.CurrentRoom.Exits.ContainsKey(direction))
            {
                // Update the previous room before moving to the new room
                _gameState.PreviousRoom = _gameState.CurrentRoom;
                _gameState.CurrentRoom = _gameState.CurrentRoom.Exits[direction];
                return $"\nYou moved {direction}.\n{_gameState.CurrentRoom.ShortDescription}";
            }
            else
            {
                return $"\nYou can't go '{direction}'!";
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
                return $"\nYou cannot go back from here!";
            }
        }
    }
}
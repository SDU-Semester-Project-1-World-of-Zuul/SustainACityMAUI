namespace WorldOfZuul.Models;

public class Game
{
    private readonly GameState _gameState;
    private readonly Dictionary<(int, int), Room> roomMap = new();
    private readonly CommandHandler _commandHandler;

    public Game()
    {
        RoomLoader roomLoader = new(Path.Combine(AppContext.BaseDirectory, "Data//Rooms.json"));
        roomMap = roomLoader.LoadRooms();
        _gameState = new() { CurrentRoom = roomMap[(0, 0)] };
        _commandHandler = new(_gameState, roomMap);
    }

    public string ExecuteCommand(string userInput)
    {
        return _commandHandler.Handle(userInput);
    }

    public static string Welcome()
    {
        return "\nWelcome to the World of Zuul!" +
               "\nWorld of Zuul is a new, incredibly boring adventure game." +
               $"{CommandHandler.Help()}\n";
    }
}
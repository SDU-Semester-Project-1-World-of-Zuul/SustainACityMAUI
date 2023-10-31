namespace SustainACityMAUI.Models;

/// <summary> Represents the main game logic and flow. </summary>
public class Game
{
    private readonly Player _player;
    private readonly Dictionary<(int, int), District> districtMap = new();
    private readonly CommandHandler _commandHandler;
    private readonly Random _random = new();

    /// <summary> Initializes game elements like Districts and state. </summary>
    public Game()
    {
        DistrictLoader districtLoader = new(Path.Combine(AppContext.BaseDirectory, "Data//Districts.json"));
        districtMap = districtLoader.LoadDistricts();
        _player = new() { CurrentDistrict = districtMap[(0, 0)] };
        _commandHandler = new(_player, districtMap);
    }

    /// <summary> Processes a user command and returns the game's response. </summary>
    public string ExecuteCommand(string userInput)
    {
        return _commandHandler.Handle(userInput);
    }

    /// <summary> Returns the game's logo. </summary>
    public static string Logo()
    {
        return "\n\n" + @"
███████╗██╗   ██╗███████╗████████╗ █████╗ ██╗███╗   ███╗     █████╗      ██████╗██╗████████╗██╗   ██╗
██╔════╝██║   ██║██╔════╝╚══██╔══╝██╔══██╗██║██╔██╗ ██╔╝    ██╔══██╗    ██╔════╝██║╚══██╔══╝╚██╗ ██╔╝
███████╗██║   ██║███████╗   ██║   ███████║██║██║╚██╗██║     ███████║    ██║     ██║   ██║    ╚████╔╝  
╚════██║██║   ██║╚════██║   ██║   ██╔══██║██║██║ ╚████║     ██╔══██║    ██║     ██║   ██║     ╚██╔╝   
███████║╚██████╔╝███████║   ██║   ██║  ██║██║██║  ╚███║     ██║  ██║    ╚██████╗██║   ██║      ██║    
╚══════╝ ╚═════╝ ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝╚═╝   ╚══╝     ╚═╝  ╚═╝     ╚═════╝╚═╝   ╚═╝      ╚═╝     " + "\n\n";
    }

    /// <summary> Provides an introductory message to guide players. </summary>
    public static string Welcome()
    {
        return "Welcome to SustainACity, a game centered around the Sustainable Development Goals (SDGs). " +
               "Navigate through the city, make decisions, and see their impact on sustainability. " +
               CommandHandler.Help() + "\n";
    }

    /// <summary> Occasionally triggers a disaster, affecting the game state. </summary>
    public string TriggerPotentialDisaster()
    {
        // 10% chance for a disaster
        if (_random.Next(100) < 10)
        {
            Disaster disaster = new();
            string disasterEvent = disaster.TriggerRandomDisaster();
            _player.Score -= 10; // Score penalty

            return $"\n{disasterEvent}\nScore Penalty! Current Score: {_player.Score}\n";
        }

        return string.Empty;
    }
}
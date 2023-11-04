namespace SustainACityMAUI.Models;

/// <summary> Handles user commands and game interactions. </summary>
public class CommandHandler
{
    private readonly Player _player;
    private readonly Dictionary<(int, int), District> _districtMap;
    private readonly Dictionary<string, Func<string>> _commandActions;

    /// <summary> Initializes the command handler with game state and District map. </summary>
    public CommandHandler(Player player, Dictionary<(int, int), District> districtMap)
    {
        _player = player;
        _districtMap = districtMap;

        // Maps user input to corresponding actions.
        _commandActions = new()
        {
            {"look", () => Look()},
            {"back", Return},
            {"north", () => Move(0, -1, "north")},
            {"south", () => Move(0, 1, "south")},
            {"east", () => Move(1, 0, "east")},
            {"west", () => Move(-1, 0, "west")},
            {"help", Help},
            {"talk", PresentChoices},
            {"1", () => Talk("good")},
            {"2", () => Talk("neutral")},
            {"3", () => Talk("bad")}
        };
    }

    /// <summary> Processes user input and returns game response. </summary>
    public string Handle(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
            return "\nI don't know that command.";

        return _commandActions.ContainsKey(userInput) ? _commandActions[userInput].Invoke() : "\nI don't know that command.";
    }

    private string Look()
    {
        return $"\n{_player.CurrentDistrict.LongDescription}";
    }

    /// <summary> Provides game directions to the user. </summary>
    private static string Help()
    {
        return "\nMove using 'north', 'south', 'east', 'west'." +
               "\nSee details with 'look'." +
               "\nReturn with 'back'." +
               "\nEngage quests with 'talk' and select using '1', '2', or '3'." +
               "\nFor this guide, type 'help'.";
    }

    /// <summary> Moves the player to a new District. </summary>
    private string Move(int x, int y, string direction)
    {
        var newCoordinates = (_player.CurrentDistrict.Coordinates.X + x, _player.CurrentDistrict.Coordinates.Y + y);

        if (_districtMap.ContainsKey(newCoordinates))
        {
            _player.PreviousDistrict = _player.CurrentDistrict;
            _player.CurrentDistrict = _districtMap[newCoordinates];

            return $"You moved {direction}.\n{_player.CurrentDistrict.ShortDescription}";
        }
        return $"You can't go '{direction}'!";
    }

    /// <summary> Returns player to the previous District. </summary>
    private string Return()
    {
        if (_player.PreviousDistrict != null)
        {
            _player.CurrentDistrict = _player.PreviousDistrict;
            return $"\nYou returned to the previous District.\n{_player.CurrentDistrict.ShortDescription}";
        }
        else
        {
            return "\nYou cannot go back from here!";
        }
    }

    /// <summary> Engages a quest dialogue based on player's choice. </summary>
    private string Talk(string choiceType)
    {
        var npc = _player.CurrentDistrict.Resident;
        if (npc == null || npc.Quest == null)
        {
            return "There's no one here offering a quest!";
        }

        var choice = npc.Quest.GetChoice(choiceType);
        if (choice == null)
        {
            return "Invalid choice!";
        }

        _player.Score += choice.Score;
        return $"{choice.Outcome} Your score is now {_player.Score}.";
    }

    /// <summary> Presents available quest choices to the player. </summary>
    private string PresentChoices()
    {
        var npc = _player.CurrentDistrict.Resident;
        if (npc == null || npc.Quest == null)
        {
            return "There's no one here offering a quest!";
        }

        // Present the choices to the user based of the NPCs options.
        return $"Quest:\t{npc.Quest.Description}" +
               $"\n1.\t{npc.Quest.Good.Action}" +
               $"\n2.\t{npc.Quest.Neutral.Action}" +
               $"\n3.\t{npc.Quest.Bad.Action}" +
               "\nType the number corresponding to your choice.";
    }
}
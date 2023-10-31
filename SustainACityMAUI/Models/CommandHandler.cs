﻿namespace SustainACityMAUI.Models;

/// <summary> Handles user commands and game interactions. </summary>
public class CommandHandler
{
    private readonly GameState _gameState;
    private readonly Dictionary<(int, int), Room> _roomMap;
    private readonly Dictionary<string, Func<string>> _commandActions;

    /// <summary> Initializes the command handler with game state and room map. </summary>
    public CommandHandler(GameState gameState, Dictionary<(int, int), Room> roomMap)
    {
        _gameState = gameState;
        _roomMap = roomMap;

        // Maps user input to corresponding actions.
        _commandActions = new()
        {
            {"look", () => $"\n{_gameState.CurrentRoom.LongDescription}"},
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
        return _commandActions.ContainsKey(userInput) ? _commandActions[userInput].Invoke() : "\nI don't know that command.";
    }

    /// <summary> Provides game directions to the user. </summary>
    public static string Help()
    {
        return "\nIn a world tied to Sustainable Development Goals (SDGs):" +
               "\n\nMove using 'north', 'south', 'east', 'west'." +
               "\nSee details with 'look'." +
               "\nReturn with 'back'." +
               "\nEngage quests with 'talk' and select using '1', '2', or '3'." +
               "\nFor this guide, type 'help'.";
    }

    /// <summary> Moves the player to a new room. </summary>
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

    /// <summary> Returns player to the previous room. </summary>
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

    /// <summary> Engages a quest dialogue based on player's choice. </summary>
    public string Talk(string choiceType)
    {
        var npc = _gameState.CurrentRoom.Resident;
        if (npc == null || npc.Quest == null)
        {
            return "There's no one here offering a quest!";
        }

        var choice = npc.Quest.GetChoice(choiceType);
        if (choice == null)
        {
            return "Invalid choice!";
        }

        _gameState.Score += choice.Score;
        return $"{choice.Outcome} Your score is now {_gameState.Score}.";
    }

    /// <summary> Presents available quest choices to the player. </summary>
    public string PresentChoices()
    {
        var npc = _gameState.CurrentRoom.Resident;
        if (npc == null || npc.Quest == null)
        {
            return "There's no one here offering a quest!";
        }

        // Present the choices to the user based of the NPCs options.
        return $"Quest: {npc.Quest.Description}\n" +
               $"1. {npc.Quest.Good.Action}\n" +
               $"2. {npc.Quest.Neutral.Action}\n" +
               $"3. {npc.Quest.Bad.Action}\n" +
               $"Type the number corresponding to your choice.";
    }
}
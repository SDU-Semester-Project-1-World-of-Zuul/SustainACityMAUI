using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SustainACityMAUI.Models;
using SustainACityMAUI.Commands;
using SustainACityMAUI.Helpers;

namespace SustainACityMAUI.ViewModels;

/// <summary> Manages game interactions for the MainPage. </summary>
public class Game : ViewModel
{
    private string _gameOutput;
    private readonly Player _player;
    private readonly Dictionary<(int, int), Room> _roomMap;
    private readonly NavigationService _navigationService;
    private string _userInput;
    private readonly Queue<string> _outputQueue = new Queue<string>();
    private bool _isTyping = false;

    public ICommand MoveNorthCommand { get; }
    public ICommand MoveSouthCommand { get; }
    public ICommand MoveEastCommand { get; }
    public ICommand MoveWestCommand { get; }
    public ICommand BackCommand { get; }
    public ICommand LookCommand { get; }
    public ICommand TalkCommand { get; }
    public ICommand HelpCommand { get; }
    public ICommand SubmitCommand { get; }

    /// <summary> Sets up the game and initializes commands. </summary>
    public Game()
    {
        _roomMap = new Dictionary<(int, int), Room>();
        JsonLoader jsonLoader = new("SustainACityMAUI.Resources.Data.rooms.json");
        _roomMap = jsonLoader.LoadRooms().ToDictionary(room => (room.X, room.Y));
        _player = new() { CurrentRoom = _roomMap.GetValueOrDefault((0, 0))! };
        _navigationService = new();

        // Initialize commands
        MoveNorthCommand = new MoveCommand(_player, _roomMap, Direction.North, AppendToOutput);
        MoveSouthCommand = new MoveCommand(_player, _roomMap, Direction.South, AppendToOutput);
        MoveEastCommand = new MoveCommand(_player, _roomMap, Direction.East, AppendToOutput);
        MoveWestCommand = new MoveCommand(_player, _roomMap, Direction.West, AppendToOutput);
        BackCommand = new BackCommand(_player, _roomMap, AppendToOutput);
        LookCommand = new LookCommand(_player, AppendToOutput);
        HelpCommand = new HelpCommand(AppendToOutput);
        TalkCommand = new TalkCommand(_player, _navigationService, AppendToOutput);
    }

    /// <summary> Represents the game's visual output. </summary>
    public string GameOutput
    {
        get { return _gameOutput; }
        set
        {
            _gameOutput = value;
            OnPropertyChanged();
        }
    }

    // Temp for testing TODO Buttons instead
    public string UserInput
    {
        get { return _userInput; }
        set
        {
            _userInput = value;
            OnPropertyChanged();
        }
    }

    /// <summary> Adds text to the game output with a typewriter effect. </summary>
    private void AppendToOutput(string text)
    {
        _outputQueue.Enqueue(text);
        if (!_isTyping)
        {
            _isTyping = true;
            _ = TypewriterPrintAsync();
        }
    }

    /// <summary> Processes the output queue with a typewriter effect. </summary>
    private async Task TypewriterPrintAsync()
    {
        while (_outputQueue.Count > 0)
        {
            var message = _outputQueue.Dequeue();
            foreach (char character in message)
            {
                GameOutput += character;
                OnPropertyChanged(nameof(GameOutput));
                await Task.Delay(20); // Or your preferred delay
            }
        }
        _isTyping = false;
    }
}
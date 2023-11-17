using System.Windows.Input;
using SustainACityMAUI.Models;
using SustainACityMAUI.Commands;
using SustainACityMAUI.Helpers;

namespace SustainACityMAUI.ViewModels;

/// <summary> Manages game interactions for the MainPage. </summary>
public class Game : ViewModel
{
    private string _dialogBox;
    private string _speaker;
    private readonly Player _player;
    private readonly Dictionary<(int, int), Room> _roomMap;
    private string _userInput;
    private readonly Queue<string> _outputQueue = new();
    private bool _isTyping = false;
    private bool _skipDialog = false;

    public ICommand MoveNorthCommand { get; }
    public ICommand MoveSouthCommand { get; }
    public ICommand MoveEastCommand { get; }
    public ICommand MoveWestCommand { get; }
    public ICommand BackCommand { get; }
    public ICommand LookCommand { get; }
    public ICommand TalkCommand { get; }
    public ICommand HelpCommand { get; }
    public ICommand SubmitCommand { get; }
    public ICommand SkipDialogCommand { get; }

    /// <summary> Sets up the game and initializes commands. </summary>
    public Game()
    {
        _roomMap = new Dictionary<(int, int), Room>();
        JsonLoader jsonLoader = new("rooms.json");
        _roomMap = jsonLoader.LoadRooms().ToDictionary(room => (room.X, room.Y));
        _player = new() { CurrentRoom = _roomMap.GetValueOrDefault((0, 0))! };

        // Initialize commands
        MoveNorthCommand = new MoveCommand(_player, _roomMap, Direction.North, AppendDialog, OnPlayerMoved);
        MoveSouthCommand = new MoveCommand(_player, _roomMap, Direction.South, AppendDialog, OnPlayerMoved);
        MoveEastCommand = new MoveCommand(_player, _roomMap, Direction.East, AppendDialog, OnPlayerMoved);
        MoveWestCommand = new MoveCommand(_player, _roomMap, Direction.West, AppendDialog, OnPlayerMoved);
        BackCommand = new BackCommand(_player, _roomMap, AppendDialog);
        LookCommand = new LookCommand(_player, AppendDialog);
        TalkCommand = new TalkCommand(_player, AppendDialog);
        HelpCommand = new HelpCommand(async (message) => await PopupAsync("Help", message, "Ok"));
        SkipDialogCommand = new Microsoft.Maui.Controls.Command(() => _skipDialog = true);
    }

    public event Action ScrollToBottomRequested;

    public string CurrentRoomImagePath { get => _player.CurrentRoom.ImgPath ?? null; }

    /// <summary> Represents game dialog. </summary>
    public string DialogBox
    {
        get => _dialogBox;
        set
        {
            _dialogBox = value;
            OnPropertyChanged();
            ScrollToBottomRequested?.Invoke(); // Request to scroll
            OnPropertyChanged(nameof(IsDialogVisible)); // Notify property change for visibility
        }
    }

    public string Speaker
    {
        get => _speaker;
        set
        {
            _speaker = value;
            OnPropertyChanged();
        }
    }

    public string UserInput
    {
        get => _userInput;
        set
        {
            _userInput = value;
            OnPropertyChanged();
        }
    }

    public bool IsDialogVisible => !string.IsNullOrEmpty(DialogBox);

    public void OnPlayerMoved()
    {
        OnPropertyChanged(nameof(CurrentRoomImagePath));
    }

    /// <summary> Adds text to the dialog box with an effect. </summary>
    private void AppendDialog(string speaker, string text)
    {
        _skipDialog = false; // Reset skip dialog flag
        DialogBox = ""; // Clear dialog box
        Speaker = speaker ?? "Narrator"; // If null use Narrator

        _outputQueue.Enqueue(text);
        if (!_isTyping)
        {
            _isTyping = true;
            _ = DialogEffectAsync();
        }
    }

    private async Task DialogEffectAsync()
    {
        while (_outputQueue.Count > 0)
        {
            var message = _outputQueue.Dequeue();

            for (int i = 0; i < message.Length; i++)
            {
                DialogBox += message[i];

                if (!(_skipDialog && message[i] != '.') || i == message.Length - 1)
                {
                    await Task.Delay(30);
                }

                if (_skipDialog && message[i] == '.')
                {
                    _skipDialog = false;
                }
            }
            _isTyping = false;
        }
    }

    public static async Task PopupAsync(string popupName, string message, string cancel)
    {
        await App.Current.MainPage.DisplayAlert(popupName, message, cancel);
    }
}
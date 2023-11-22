using SustainACityMAUI.Commands;
using SustainACityMAUI.Helpers;
using SustainACityMAUI.Models;
using System.Windows.Input;

namespace SustainACityMAUI.ViewModels;

/// <summary> Manages game interactions for the MainPage. </summary>
public class Game : BaseViewModel
{
    private string _dialogBox;
    private string _speaker;
    private readonly Dictionary<(int, int), Room> _roomMap;
    private string _userInput;
    private readonly Queue<string> _outputQueue = new();
    private bool _isTyping = false;
    private bool _skipDialog = false;
    private bool _isInventoryVisible;

    public ICommand MoveNorthCommand => new MoveCommand(Player, _roomMap, Direction.North, AppendDialog, OnPlayerMoved);
    public ICommand MoveSouthCommand => new MoveCommand(Player, _roomMap, Direction.South, AppendDialog, OnPlayerMoved);
    public ICommand MoveEastCommand => new MoveCommand(Player, _roomMap, Direction.East, AppendDialog, OnPlayerMoved);
    public ICommand MoveWestCommand => new MoveCommand(Player, _roomMap, Direction.West, AppendDialog, OnPlayerMoved);
    public ICommand BackCommand => new BackCommand(Player, _roomMap, AppendDialog, OnPlayerMoved);
    public ICommand LookCommand => new LookCommand(Player, AppendDialog);
    public ICommand TalkCommand => new TalkCommand(Player, AppendDialog);
    public ICommand HelpCommand => new HelpCommand(async (message) => await PopupAsync("Help", message, "Ok"));
    public ICommand SkipDialogCommand => new Command(() => _skipDialog = true);
    public ICommand InventoryCommand => new Command(() => IsInventoryVisible = !IsInventoryVisible);

    /// <summary> Sets up the game. </summary>
    public Game()
    {
        // Initialization
        var jsonLoader = new JsonLoader("rooms.json");
        _roomMap = jsonLoader.LoadData<Room>().ToDictionary(room => (room.X, room.Y));

        Player = new() { CurrentRoom = _roomMap.GetValueOrDefault((0, 0))! };
    }

    public event Action ScrollToBottomRequested;

    public Player Player { get; }

    public string CurrentRoomImagePath => Player.CurrentRoom.ImgPath;
    public bool IsDialogVisible => !string.IsNullOrEmpty(DialogBox);

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
            if (_speaker != value)
            {
                DialogBox = "";
            }

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

    public bool IsInventoryVisible
    {
        get => _isInventoryVisible;
        set
        {
            _isInventoryVisible = value;
            OnPropertyChanged();
        }
    }

    public void OnPlayerMoved()
    {
        OnPropertyChanged(nameof(CurrentRoomImagePath));
    }

    /// <summary> Adds text to the dialog box with an effect. </summary>
    private void AppendDialog(string speaker, string text)
    {
        _skipDialog = false; // Reset skip dialog flag
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
        while (_outputQueue.Any())
        {
            var message = _outputQueue.Dequeue();
            await TypeOutMessage(message);
        }
    }

    private async Task TypeOutMessage(string message)
    {
        _isTyping = true;

        for (int i = 0; i < message.Length; i++)
        {
            if (_skipDialog)
            {
                i = SkipToNextPeriod(message, i); // Skip to the next period
            }
            else
            {
                DialogBox += message[i];
                if (_outputQueue.Count == 0) // Only delay if not skipping and no new message
                {
                    await Task.Delay(30);
                }
            }
        }

        _isTyping = false;
    }

    private int SkipToNextPeriod(string message, int currentIndex)
    {
        int nextPeriod = message.IndexOf('.', currentIndex);
        if (nextPeriod == -1) nextPeriod = message.Length - 1;

        DialogBox += message[currentIndex..(nextPeriod + 1)];
        _skipDialog = false;

        return nextPeriod; // Return updated index
    }

    public static async Task PopupAsync(string popupName, string message, string cancel)
    {
        await App.Current.MainPage.DisplayAlert(popupName, message, cancel);
    }
}
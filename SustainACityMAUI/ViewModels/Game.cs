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
    private readonly Queue<DialogItem> _dialogQueue = new();
    private bool _isTyping = false;
    private bool _skipDialog = false;
    private bool _isInventoryVisible;
    private List<string> _responseOptions = new();
    private CancellationTokenSource _cancellationTokenSource = new();
    private const string _makeshiftBuffer = "\u200B\u200B\u200B\u200B\u200B"; // Five zero-width spaces
    private const string _narrator = "Narrator";
    private const string _delimiter = "\n\n---\n";

    public ICommand MoveNorthCommand => new MoveCommand(Player, _roomMap, Direction.North, DialogWrite, OnPlayerMoved);
    public ICommand MoveSouthCommand => new MoveCommand(Player, _roomMap, Direction.South, DialogWrite, OnPlayerMoved);
    public ICommand MoveEastCommand => new MoveCommand(Player, _roomMap, Direction.East, DialogWrite, OnPlayerMoved);
    public ICommand MoveWestCommand => new MoveCommand(Player, _roomMap, Direction.West, DialogWrite, OnPlayerMoved);
    public ICommand BackCommand => new BackCommand(Player, _roomMap, DialogWrite, OnPlayerMoved);
    public ICommand LookCommand => new LookCommand(Player, DialogWrite);
    public ICommand TalkCommand => new TalkCommand(Player, options => ResponseOptions = options, DialogWrite);
    public ICommand HelpCommand => new HelpCommand(async (message) => await PopupAsync("Help", message, "Ok"));
    public ICommand SkipDialogCommand => new Command(SkipDialog);

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
    public bool IsOptionsVisible => !_isTyping && ResponseOptions.Count > 0;
    public bool AreActionButtonsEnabled => ResponseOptions.Count == 0;

    public List<string> ResponseOptions
    {
        get => _responseOptions;
        set
        {
            _responseOptions = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(AreActionButtonsEnabled)); // Notify change for button enabled state
        }
    }

    public string DialogBox
    {
        get => _dialogBox;
        set
        {
            _dialogBox = value; // Set the dialog text
            OnPropertyChanged(); // Notify the change
            ScrollToBottomRequested?.Invoke();              // Request to scroll
            OnPropertyChanged(nameof(IsDialogVisible));     // Notify property change for visibility
            OnPropertyChanged(nameof(IsOptionsVisible));    // Notify property change for visibility
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

    public bool IsInventoryVisible
    {
        get => _isInventoryVisible;
        set
        {
            _isInventoryVisible = value;
            OnPropertyChanged();
        }
    }

    private void DialogWrite(string speaker, string text)
    {
        var dialogItem = new DialogItem
        {
            Speaker = speaker ?? _narrator,
            Text = _makeshiftBuffer + text + "\n"
        };

        _dialogQueue.Enqueue(dialogItem);
        _ = ProcessDialogQueueAsync();
    }

    private async Task ProcessDialogQueueAsync()
    {
        if (_dialogQueue.Any())
        {
            var dialogItem = _dialogQueue.Dequeue();

            if (Speaker != dialogItem.Speaker)
            {
                // Append a delimiter or newline to indicate a new speaker
                dialogItem.Text = _delimiter + dialogItem.Text;
                Speaker = dialogItem.Speaker;
            }

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new();
            _skipDialog = false;

            await DisplayTextAsync(dialogItem.Text, _cancellationTokenSource.Token);
        }
    }

    private async Task DisplayTextAsync(string text, CancellationToken cancelToken)
    {
        _isTyping = true;
        int i = 0;

        try
        {
            for (i = 0; i < text.Length; i++)
            {
                if (_skipDialog)
                {
                    var (newText, newIndex) = SkipToNextPeriod(text, i);
                    DialogBox += newText;
                    i = newIndex;
                }
                else
                {
                    DialogBox += text[i];
                    if (_dialogQueue.Count == 0)    // Only delay if not skipping and no new message
                    {
                        await Task.Delay(30, cancelToken);
                    }
                }
            }
        }
        catch (TaskCanceledException)
        {
            DialogBox += text[(i + 1)..]; // Add remaining text on cancellation
        }
        finally
        {
            _isTyping = false;
            OnPropertyChanged(nameof(IsOptionsVisible));
        }
    }

    private (string, int) SkipToNextPeriod(string text, int index)
    {
        int nextPeriod = text.IndexOf('.', index);
        nextPeriod = nextPeriod != -1 ? nextPeriod : text.Length - 1;
        _skipDialog = false;

        return (text[index..(nextPeriod + 1)], nextPeriod);  // Return updated index
    }

    private void SkipDialog()
    {
        if (_isTyping)
        {
            if (!_skipDialog)
            {
                _skipDialog = true;
            }
            else
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new();
            }
        }
    }

    public void OnPlayerMoved()
    {
        OnPropertyChanged(nameof(CurrentRoomImagePath));
    }

    public static async Task PopupAsync(string popupName, string message, string cancel)
    {
        await App.Current.MainPage.DisplayAlert(popupName, message, cancel);
    }
}
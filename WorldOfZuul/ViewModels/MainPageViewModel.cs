using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WorldOfZuul.Models;

namespace WorldOfZuul.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    private string _gameOutput;
    private string _userInput;
    private readonly Game _game;

    public MainPageViewModel()
    {
        _game = new Game();
        ProcessInputCommand = new Command<string>(ProcessInput);
        _gameOutput = Game.Welcome();
    }

    public string GameOutput
    {
        get { return _gameOutput; }
        set
        {
            _gameOutput = value;
            OnPropertyChanged();
        }
    }

    public string UserInput
    {
        get { return _userInput; }
        set
        {
            _userInput = value;
            OnPropertyChanged();
        }
    }

    public ICommand ProcessInputCommand { get; }

    private void ProcessInput(string userInput)
    {
        _gameOutput += $"\n> {userInput}\n";
        _gameOutput += _game.ExecuteCommand(userInput); // Processes game choices
        GameOutput = _gameOutput; // Notify the View of changes
        UserInput = ""; // Clear the input
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
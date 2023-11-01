using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.ViewModels;

/// <summary> Manages game interactions for the MainPage. </summary>
public class MainPageViewModel : INotifyPropertyChanged
{
    private string _gameOutput;
    private string _userInput;
    private readonly Game _game;

    /// <summary> Sets up the game and initializes commands. </summary>
    public MainPageViewModel()
    {
        _game = new Game();
        SubmitCommand = new Command<string>(SubmitInput);
        AppendToOutput(Game.SplashScreen());
        TypeEffectAsync(_game.Introduction(), 25);
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

    /// <summary> Holds the user's current input. </summary>
    public string UserInput
    {
        get { return _userInput; }
        set
        {
            _userInput = value;
            OnPropertyChanged();
        }
    }

    /// <summary> Processes the user's input. </summary>
    public ICommand SubmitCommand { get; }

    /// <summary> Animates text typing. </summary>
    private async void TypeEffectAsync(string message, int typingDelay)
    {
        foreach (char character in message)
        {
            AppendToOutput(character.ToString());
            await Task.Delay(typingDelay);
        }
    }

    /// <summary> Handles user input and game responses. </summary>
    private void SubmitInput(string input)
    {
        AppendToOutput($"\n\n> {input}\n");
        TypeEffectAsync(_game.ExecuteCommand(input), 25);
        UserInput = "";
    }

    /// <summary> Adds text to the game output. </summary>
    private void AppendToOutput(string text)
    {
        GameOutput += text;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary> Notifies UI of property changes. </summary>
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
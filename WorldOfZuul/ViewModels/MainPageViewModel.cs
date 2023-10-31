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
        SubmitCommand = new Command<string>(SubmitInput);
        AppendToOutput(Game.Logo());
        TypeEffectAsync(Game.Welcome(), 25);
    }

    public string GameOutput
    {
        get { return _gameOutput; }
        set
        {
            _gameOutput = value;
            RaisePropertyChanged();
        }
    }

    public string UserInput
    {
        get { return _userInput; }
        set
        {
            _userInput = value;
            RaisePropertyChanged();
        }
    }

    public ICommand SubmitCommand { get; }

    private async void TypeEffectAsync(string message, int typingDelay)
    {
        foreach (char character in message)
        {
            _gameOutput += character;
            GameOutput = _gameOutput;
            await Task.Delay(typingDelay);
        }
    }

    private void SubmitInput(string input)
    {
        AppendToOutput($"\n> {input}\n");
        AppendToOutput(_game.TriggerPotentialDisaster()); // Might trigger a disaster
        TypeEffectAsync(_game.ExecuteCommand(input), 25);
        UserInput = "";
    }

    private void AppendToOutput(string text)
    {
        _gameOutput += text;
        GameOutput = _gameOutput;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
using SustainACityMAUI.Models;
using SustainACityMAUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SustainACityMAUI.ViewModels;

public class PasswordMinigame : BaseViewModel
{
    public string backroundImg => "computer.jpeg";

    // Fields
    private readonly Player _player;
    private string _inputString = "";
    private string _displayText = "";
    private string _masterPassword = "6666";

    public ICommand AddCharCommand { get; private set; }
    public ICommand DeleteCharCommand { get; private set; }
    public ICommand EnterPasswordCommand { get; private set; }

    public string InputString
    {
        get => _inputString;
        private set
        {
            if (_inputString != value)
            {
                _inputString = value;
                OnPropertyChanged();
                DisplayText = FormatText(_inputString);

                ((Command)DeleteCharCommand).ChangeCanExecute();
            }
        }
    }

    public string DisplayText
    {
        get => _displayText;
        private set
        {
            if (_displayText != value)
            {
                _displayText = value;
                OnPropertyChanged();
            }
        }
    }

    public string MasterPassword
    {
        get => _masterPassword;
        private set
        {
            if (_masterPassword == _displayText)
            {
                Application.Current.MainPage.DisplayAlert("Score", $"Congrats! 🎉. Your score is 666", "Go Back");
            }
        }
    }

    public PasswordMinigame(Player player)
    {
        _player = player;
        // Command to add the key to the input string
        AddCharCommand = new Command<string>((key) => InputString += key);

        // Command to delete a character from the input string when allowed
        DeleteCharCommand =
            new Command(
                // Command will strip a character from the input string
                () => InputString = InputString.Substring(0, InputString.Length - 1),

                // CanExecute is processed here to return true when there's something to delete
                () => InputString.Length > 0
            );
        EnterPasswordCommand =
            new Command(
                () => MasterPassword = MasterPassword
            );
    }

    string FormatText(string str)
    {
        string formatted = str;
        return formatted;
    }
}

using SustainACityMAUI.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SustainACityMAUI.ViewModels;

public class StartViewModel : ViewModel
{
    public StartViewModel()
    {
        StartGameCommand = new Command(StartGame);
        ExitGameCommand = new Command(ExitGame);
    }

    // Command to start the game
    public ICommand StartGameCommand { get; }

    // Command to exit the game
    public ICommand ExitGameCommand { get; }

    private void StartGame()
    {
        Application.Current.MainPage.Navigation.PushAsync(new GameView());
    }

    private void ExitGame()
    {
        // Add logic to exit the game here
        // For example, close the application
        Application.Current.Quit();
    }
}
using SustainACityMAUI.Views;
using System.Windows.Input;

namespace SustainACityMAUI.ViewModels;

public class StartMenu : ViewModel
{
    public ICommand StartGameCommand { get; }
    public ICommand AboutCommand { get; }
    public ICommand ExitGameCommand { get; }
    public StartMenu()
    {
        StartGameCommand = new Command(StartGame);
        ExitGameCommand = new Command(ExitGame);
        AboutCommand = new Command(OpenGitHubRepo);
    }

    private void StartGame()
    {
        Application.Current.MainPage.Navigation.PushAsync(new GamePage());
    }

    private void OpenGitHubRepo()
    {
        const string githubRepoUrl = "https://github.com/SDU-Semester-Project-1-World-of-Zuul/SustainACityMAUI/tree/Dev";

        Launcher.OpenAsync(new Uri(githubRepoUrl));
    }

    private void ExitGame()
    {
        Application.Current.Quit();
    }
}
using SustainACityMAUI.Helpers;
using SustainACityMAUI.Views;
using System.Windows.Input;

namespace SustainACityMAUI.ViewModels;

public class StartMenu : BaseViewModel
{
    private readonly Uri RepoUrl = new("https://github.com/SDU-Semester-Project-1-World-of-Zuul/SustainACityMAUI/tree/Dev");
    public ICommand StartGameCommand => new Command(async () => await NavigationService.NavigateToPageAsync("Game"));
    public ICommand AboutCommand => new Command(() => Launcher.OpenAsync(RepoUrl));
    public ICommand ExitGameCommand => new Command(() => Application.Current.Quit());
}
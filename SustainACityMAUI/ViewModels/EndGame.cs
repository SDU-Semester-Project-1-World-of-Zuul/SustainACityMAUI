using SustainACityMAUI.Helpers;
using SustainACityMAUI.Models;
using System.Windows.Input;

namespace SustainACityMAUI.ViewModels;

public class EndGame : BaseViewModel
{
    private Player _player;
    public string PlayerScore => $"Your Score: {_player.Score}";
    public string CongratulatoryMessage => "Congratulations on completing the game!";

    public ICommand RestartGameCommand => new Command(RestartGame);
    public ICommand ExitGameCommand => new Command(() => Application.Current.Quit());

    public string CreditsText => "Game developed by: SustainTeam\n"
        + "Senior Slacker: PhongsakonKonrad\n"
        + "Minute-Taker: respectFullorian\n"
        + "Pull Master: Konyak\n"
        + "Der Anführer: rikkiyoyo\n"
        + "Artistic Visionary: respectfulola\n"
        + "Backend Senior Dev: respectMathias\n"
        + "Game Art: DALL·E 4\n"
        + "Copy-Paste Specialist: StackOverflow\n" 
        + "Special thanks to: SDU faculty, contributors, mentors, and supporters for their invaluable guidance and support.\n"
        + "...";

    public EndGame(Player player)
    {
        _player = player;
    }

    private async void RestartGame()
    {
        await NavigationService.NavigateBackAsync();
        await NavigationService.NavigateBackAsync();
    }
}
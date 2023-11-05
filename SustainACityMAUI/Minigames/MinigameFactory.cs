using SustainACityMAUI.Models;

namespace SustainACityMAUI.Minigames;
public class MinigameFactory
{
    public async Task CreateMinigameAsync(string minigameName, Player player)
    {
        switch (minigameName)
        {
            case "SchoolQuizMinigame":
                var view = new SchoolQuizMinigameView(player);
                await Application.Current.MainPage.Navigation.PushAsync(view);
                break;
            default:
                throw new ArgumentException("Invalid minigame name\n");
        }
    }

    public async void NavigateBackAsync()
    {
        // Assuming you have navigation setup similar to this
        await Shell.Current.GoToAsync(".."); // ".." navigates up the navigation stack
    }
}
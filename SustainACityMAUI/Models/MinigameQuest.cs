using SustainACityMAUI.Helpers;
using System.Text.Json.Serialization;

namespace SustainACityMAUI.Models;

public class MinigameQuest : Quest
{
    public string MinigamePage { get; set; }
    public MinigameQuest(string minigamePage)
    {
        MinigamePage = minigamePage;
    }

    public override async Task Execute(Player player)
    {
        bool pageLaunched = await NavigationService.NavigateToPageAsync(MinigamePage, player);

        if (!pageLaunched)
        {
            // Handle the failed navigation
            HandleFailedNavigation();
        }
    }

    private void HandleFailedNavigation()
    {
        Application.Current.MainPage.DisplayAlert("Error", "Failed to launch the minigame.", "OK");
    }
}
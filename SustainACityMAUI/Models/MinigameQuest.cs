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
        await NavigationService.NavigateToPageAsync(MinigamePage, player);
    }
}
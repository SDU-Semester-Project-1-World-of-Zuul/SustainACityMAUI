using SustainACityMAUI.Models;

namespace SustainACityMAUI.Helpers;

public static class NavigationService
{
    public static async Task<bool> NavigateToMinigameAsync(Player player)
    {
        // Combines the minigame name with the namespace
        string typeName = $"SustainACityMAUI.Views.{player.CurrentRoom.NPC.Minigame}Page";

        // Get the Type from the minigame name
        Type viewType = Type.GetType(typeName);

        // Check if the type is a Page and create an instance
        if (viewType?.IsSubclassOf(typeof(Page)) == true)
        {
            Page view = (Page)Activator.CreateInstance(viewType, player);
            await Application.Current.MainPage.Navigation.PushAsync(view);

            return true;
        }
        else
        {
            return false;
        }
    }

    public static async void NavigateBackAsync()
    {
        await Shell.Current.GoToAsync(".."); // ".." navigates up the navigation stack
    }
}
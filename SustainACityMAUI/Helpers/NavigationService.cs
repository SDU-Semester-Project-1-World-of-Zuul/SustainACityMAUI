using SustainACityMAUI.Models;

namespace SustainACityMAUI.Helpers;

public class NavigationService
{
    public async Task NavigateToMinigameAsync(string minigameName, Player player)
    {
        // Combines the minigame name with the namespace
        string typeName = $"SustainACityMAUI.Views.{minigameName}Page";

        // Get the Type from the minigame name
        Type viewType = Type.GetType(typeName);

        // Check if the type is a Page and create an instance
        if (viewType?.IsSubclassOf(typeof(Page)) == true)
        {
            Page view = (Page)Activator.CreateInstance(viewType, player);
            await Application.Current.MainPage.Navigation.PushAsync(view);
        }
        else
        {
            throw new ArgumentException($"No page found for minigame name: {minigameName}", nameof(minigameName));
        }
    }

    public async void NavigateBackAsync()
    {
        await Shell.Current.GoToAsync(".."); // ".." navigates up the navigation stack
    }
}
namespace SustainACityMAUI.Helpers;

public static class NavigationService
{
    public static async Task<bool> NavigateToPageAsync(string pageName, object parameter = null)
    {
        var typeName = $"SustainACityMAUI.Views.{pageName}Page";

        // Get the Type from the page name
        var viewType = Type.GetType(typeName);

        // Check if the type is a Page and create an instance
        if (viewType?.IsSubclassOf(typeof(Page)) == true)
        {
            Page view = parameter != null
                ? (Page)Activator.CreateInstance(viewType, parameter)
                : (Page)Activator.CreateInstance(viewType);

            await Application.Current.MainPage.Navigation.PushAsync(view);

            return true;
        }
        else
        {
            return false;
        }
    }

    public static async Task NavigateBackAsync()
    {
        await Shell.Current.GoToAsync(".."); // ".." navigates up the navigation stack
    }
}
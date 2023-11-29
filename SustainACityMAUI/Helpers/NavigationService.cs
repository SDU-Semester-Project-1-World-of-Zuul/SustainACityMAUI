namespace SustainACityMAUI.Helpers
{
    public static class NavigationService
    {
        private static readonly Page _page = Application.Current.MainPage;
        private static readonly INavigation _navigation = _page.Navigation;
        public static async Task NavigateToPageAsync(string pageName, object parameter = null)
        {
            try
            {
                var typeName = $"SustainACityMAUI.Views.{pageName}Page, SustainACityMAUI";
                var viewType = Type.GetType(typeName, throwOnError: false, ignoreCase: true);

                if (viewType?.IsSubclassOf(typeof(Page)) == true)
                {
                    Page view = (parameter != null)
                        ? Activator.CreateInstance(viewType, parameter) as Page
                        : Activator.CreateInstance(viewType) as Page;

                    if (view != null)
                    {
                        await _navigation.PushAsync(view);
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Page type '{typeName}' could not be found.");
                }
            }
            catch (Exception exception)
            {
                await HandleFailedNavigation(exception);
            }
        }

        public static async Task NavigateBackAsync()
        {
            try
            {

                // First, check if there are any modal pages
                if (_navigation.NavigationStack.Count > 1)
                {
                    await _navigation.PopAsync();
                }
                else
                {
                    throw new InvalidOperationException("No pages to navigate back to.");
                }
            }
            catch (Exception exception)
            {
                await HandleFailedNavigation(exception);
            }
        }


        private static async Task HandleFailedNavigation(Exception exception)
        {
            // Detailed error message
            string errorMessage = $"An error occurred during navigation. Please try again.\n\n" +
                                  $"Error Details:\n{exception.Message}";

            // Logging the error for debugging purposes
            await Console.Out.WriteLineAsync($"Navigation Error: {exception}");

            // Displaying a more user-friendly and informative alert
            await _page.DisplayAlert("Navigation Error", errorMessage, "OK");
        }
    }
}
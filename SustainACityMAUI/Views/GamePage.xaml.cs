using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();
        BindingContext = new Game();
        if (BindingContext is Game viewModel)
        {
            viewModel.ScrollToBottomRequested += ScrollToBottom;
        }
    }

    private async void ScrollToBottom()
    {
        await Task.Delay(100); // Wait for the UI to update with the new content

        if (dialogScrollView.Content is Label label)
        {
            var scrollPosition = label.Height - dialogScrollView.Height;
            if (scrollPosition > 0)
            {
                await dialogScrollView.ScrollToAsync(0, scrollPosition, true);
            }
        }
    }
}
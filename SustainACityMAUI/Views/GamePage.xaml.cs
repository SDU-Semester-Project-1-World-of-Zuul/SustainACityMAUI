using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class GamePage : ContentPage
{
    private readonly Game viewModel;

    public GamePage()
    {
        InitializeComponent();
        viewModel = new Game();
        BindingContext = viewModel;

        viewModel.ScrollToBottomRequested += ScrollToBottom;
    }

    private async void ScrollToBottom()
    {
        await Task.Delay(50); // Wait for the UI to update with the new content

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
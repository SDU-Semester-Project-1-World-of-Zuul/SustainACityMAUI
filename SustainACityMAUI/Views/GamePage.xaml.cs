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

        dialogScrollView.Content.SizeChanged += Content_SizeChanged;
    }

    private async void Content_SizeChanged(object sender, EventArgs e)
    {
        var scrollPosition = dialogScrollView.ContentSize.Height - dialogScrollView.Height;

        if (scrollPosition > 0)
        {
            await dialogScrollView.ScrollToAsync(0, scrollPosition, true);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (dialogScrollView.Content != null)
        {
            dialogScrollView.Content.SizeChanged += Content_SizeChanged;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (dialogScrollView?.Content != null)
        {
            dialogScrollView.Content.SizeChanged -= Content_SizeChanged;
        }
    }
}
using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();
        BindingContext = new Game();
    }
}
using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class GameView : ContentPage
{
    public GameView()
    {
        InitializeComponent();
        BindingContext = new GameViewModel();
    }
}
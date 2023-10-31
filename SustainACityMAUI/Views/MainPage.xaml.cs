using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }
}
using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class StartMenuPage : ContentPage
{
	public StartMenuPage()
	{
		InitializeComponent();
        BindingContext = new StartMenu();
    }
}
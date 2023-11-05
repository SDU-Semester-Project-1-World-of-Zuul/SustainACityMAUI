using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class StartView : ContentPage
{
	public StartView()
	{
		InitializeComponent();
        BindingContext = new StartViewModel();
    }
}
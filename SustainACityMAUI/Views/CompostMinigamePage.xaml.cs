using SustainACityMAUI.Models;
using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class CompostMinigamePage : ContentPage
{
	private readonly CompostMinigame viewModel;
	public CompostMinigamePage(Player player)
	{
		InitializeComponent();
        viewModel = new CompostMinigame(player);
        BindingContext = viewModel;
	}
}
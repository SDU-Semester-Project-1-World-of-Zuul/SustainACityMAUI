using SustainACityMAUI.ViewModels;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.Views;

public partial class BalancingActMinigamePage : ContentPage
{
	public BalancingActMinigamePage(Player player)
	{
		InitializeComponent();
        BindingContext = new BalancingActMinigame(player);
    }
}
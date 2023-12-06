using SustainACityMAUI.ViewModels;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.Views;

public partial class EndGamePage : ContentPage
{
	public EndGamePage(Player player)
	{
		InitializeComponent();
        BindingContext = new EndGame(player);
    }
}
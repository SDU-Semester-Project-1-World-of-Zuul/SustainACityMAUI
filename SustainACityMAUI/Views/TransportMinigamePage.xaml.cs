using SustainACityMAUI.ViewModels;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.Views;

public partial class TransportMinigamePage : ContentPage
{
	public TransportMinigamePage(Player player)
	{
		InitializeComponent();
		BindingContext = new TransportMinigame(player);
    }
}
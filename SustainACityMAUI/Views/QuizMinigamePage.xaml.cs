using SustainACityMAUI.Models;
using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class QuizMinigamePage : ContentPage
{
	public QuizMinigamePage(Player player)
	{
        InitializeComponent();
        BindingContext = new QuizMinigame(player);
    }
}
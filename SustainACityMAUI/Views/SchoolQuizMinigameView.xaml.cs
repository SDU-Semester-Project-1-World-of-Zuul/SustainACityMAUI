using SustainACityMAUI.Models;
using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class SchoolQuizMinigameView : ContentPage
{
	public SchoolQuizMinigameView(Player player)
	{
        InitializeComponent();
        BindingContext = new SchoolQuizMinigameViewModel(player);
    }
}
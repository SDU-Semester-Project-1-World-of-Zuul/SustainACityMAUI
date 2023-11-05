using SustainACityMAUI.Models;

namespace SustainACityMAUI.Minigames;

public partial class SchoolQuizMinigameView : ContentPage
{
	public SchoolQuizMinigameView(Player player)
	{
        InitializeComponent();
        BindingContext = new SchoolQuizMinigameViewModel(player);
    }
}
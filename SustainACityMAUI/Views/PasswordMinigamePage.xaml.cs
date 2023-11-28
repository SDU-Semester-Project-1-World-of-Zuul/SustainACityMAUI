using SustainACityMAUI.ViewModels;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.Views;

public partial class PasswordMinigamePage : ContentPage
{
    public PasswordMinigamePage(Player player)
    {
        InitializeComponent();
        BindingContext = new PasswordMinigame(player);
    }
}
using SustainACityMAUI.Models;
using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class CrisisManagementMinigamePage : ContentPage
{
    public CrisisManagementMinigamePage(Player player)
    {
        InitializeComponent();
        BindingContext = new CrisisManagementMinigame(player);
    }

}
using SustainACityMAUI.Models;
using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class WordGuessPage : ContentPage
{
    public WordGuessPage(Player player)
    {
        InitializeComponent();
        BindingContext = new WordGuess(player);
    }
}
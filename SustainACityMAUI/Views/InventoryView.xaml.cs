using SustainACityMAUI.ViewModels;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.Views;

public partial class InventoryView : ContentView
{
	public InventoryView()
	{
		InitializeComponent();
	}

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (BindingContext is Player player)
        {
            this.BindingContext = new Inventory(player);
        }
    }
}
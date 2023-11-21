using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views;

public partial class InventoryView : ContentView
{
	public InventoryView()
	{
		InitializeComponent();
		BindingContext = new Inventory();
	}
}
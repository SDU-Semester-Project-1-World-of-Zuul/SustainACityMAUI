using System.Collections.ObjectModel;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.ViewModels;

public class Inventory : ViewModel
{
    public Inventory()
    {
    }

    public ObservableCollection<Item> Items { get; }
    private Item _selectedItem;

    public Item SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (_selectedItem != value)
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
    }

    // NEEDS TO BE IMPLEMENTED
}
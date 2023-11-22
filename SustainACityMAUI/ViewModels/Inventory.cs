using SustainACityMAUI.Models;

namespace SustainACityMAUI.ViewModels;

public class Inventory : BaseViewModel
{
    private readonly Player _player;
    private Item _selectedItem;

    public Inventory(Player player)
    {
        _player = player;
    }

    public List<Item> Items => _player.Inventory;

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

    // Pickup needs to look at player.currentroom.items and add them to player.inventory

    // Use needs to remove the item from player.inventory and use the effect of the item

    // (You can directly add an item to the inventory as a reward e.g. _player.Inventory.Add(new Item("name", "description", "effect", "image.png"));)
    // But effect has yet to be implemented
}
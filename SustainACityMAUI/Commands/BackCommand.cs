using SustainACityMAUI.Models;

namespace SustainACityMAUI.Commands;

public class BackCommand : BaseCommand
{
    private readonly Player _player;
    private readonly Dictionary<(int, int), Room> _roomMap;
    private readonly Action<string, string> _updateAction;
    private readonly Action _onMovedAction;

    public BackCommand(Player player, Dictionary<(int, int), Room> roomMap, Action<string, string> updateAction, Action onMovedAction)
    {
        _player = player;
        _roomMap = roomMap;
        _updateAction = updateAction;  // Assign the delegate
        _onMovedAction = onMovedAction;
    }

    public override bool CanExecute(object parameter)
    {
        return _player.MovementHistory != null;
    }

    public override void Execute(object parameter)
    {
        // Check if there is a previous room to go back to
        if (!_player.MovementHistory.Any())
        {
            _updateAction(null, "There's nowhere to go back to.\n");
            return;
        }

        // Get the last coordinates from the movement history
        var lastCoordinates = _player.MovementHistory.Pop();

        // Check if the room still exists (sanity check)
        if (_roomMap.TryGetValue(lastCoordinates, out Room lastRoom))
        {
            _player.CurrentRoom = lastRoom;
            _onMovedAction?.Invoke();
            _updateAction(null, $"You moved back to {_player.CurrentRoom.Name}.\n");
        }
        else
        {
            _updateAction(null, "The way back seems to be blocked now.\n");
        }
    }
}
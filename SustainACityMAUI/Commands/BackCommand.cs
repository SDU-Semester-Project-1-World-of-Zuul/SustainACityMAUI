using SustainACityMAUI.Models;

namespace SustainACityMAUI.Commands;

public class BackCommand : Command
{
    private readonly Player _player;
    private readonly Dictionary<(int, int), Room> _roomMap;
    private readonly Action<string> _updateAction;

    public BackCommand(Player player, Dictionary<(int, int), Room> roomMap, Action<string> updateAction)
    {
        _player = player;
        _roomMap = roomMap;
        _updateAction = updateAction;  // Assign the delegate
    }

    public override bool CanExecute(object parameter)
    {
        return _player.MovementHistory != null;
    }

    public override void Execute(object parameter)
    {
        // Check if there is a previous room to go back to
        if (_player.MovementHistory.Any())
        {
            // Get the last coordinates from the movement history
            var lastCoordinates = _player.MovementHistory.Pop();

            // Check if the room still exists (sanity check)
            if (_roomMap.TryGetValue(lastCoordinates, out Room lastRoom))
            {
                _player.CurrentRoom = lastRoom;
                _updateAction($"You moved back to {_player.CurrentRoom.Name}.\n");
            }
            else
            {
                _updateAction("The way back seems to be blocked now.\n");
            }
        }
        else
        {
            _updateAction("There's nowhere to go back to.\n");
        }
    }
}
using SustainACityMAUI.Models;

namespace SustainACityMAUI.Commands;

public class MoveCommand : BaseCommand
{
    private readonly Player _player;
    private readonly Dictionary<(int, int), Room> _roomMap;
    private readonly Direction _direction;
    private readonly Action<string, string> _updateAction;  // Delegate for updating the UI
    private readonly Action _onMovedAction;

    private readonly Dictionary<Direction, (int x, int y)> _directions = new()
    {
        {Direction.North, (0, -1)},
        {Direction.South, (0, 1)},
        {Direction.East, (1, 0)},
        {Direction.West, (-1, 0)}
    };

    public MoveCommand(Player player, Dictionary<(int, int), Room> roomMap, Direction direction, Action<string, string> updateAction, Action onMovedAction)
    {
        _player = player;
        _roomMap = roomMap;
        _direction = direction;
        _updateAction = updateAction;  // Assign the delegate
        _onMovedAction = onMovedAction;
    }

    public override bool CanExecute(object parameter)
    {
        return _directions.ContainsKey(_direction);
    }

    public override void Execute(object parameter)
    {
        if (!_directions.TryGetValue(_direction, out var movement))
        {
            throw new InvalidOperationException($"Invalid direction encountered: {_direction}");
        }

        // Calculate new Coordinates
        var deltaX = _player.CurrentRoom.X + movement.x;
        var deltaY = _player.CurrentRoom.Y + movement.y;
        var newCoordinates = (deltaX, deltaY);

        if (_roomMap.TryGetValue(newCoordinates, out Room newRoom))
        {
            _player.MovementHistory.Push((_player.CurrentRoom.X, _player.CurrentRoom.Y)); // Add old coordinates to the history
            _player.CurrentRoom = newRoom;

            _onMovedAction?.Invoke();
            _updateAction?.Invoke(null, $"You move {_direction} to {newRoom.Name}.\n");  // Update UI with the new room name
        }
        else
        {
            _updateAction?.Invoke(null, "You can't move in that direction.\n");  // Provide feedback that the move is not possible
        }
    }
}
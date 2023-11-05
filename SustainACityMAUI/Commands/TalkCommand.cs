using SustainACityMAUI.Models;
using SustainACityMAUI.Minigames;

namespace SustainACityMAUI.Commands;

public class TalkCommand : Command
{
    private readonly Player _player;
    private readonly MinigameFactory _minigameFactory;
    private readonly Action<string> _updateAction;

    public TalkCommand(Player player, MinigameFactory minigameFactory, Action<string> updateAction)
    {
        _player = player;
        _minigameFactory = minigameFactory;
        _updateAction = updateAction;
    }

    public override async void Execute(object parameter)
    {
        if (_player.CurrentRoom?.NPC != null)
        {
            if (!string.IsNullOrEmpty(_player.CurrentRoom.NPC.Minigame))
            {
                var minigameName = _player.CurrentRoom.NPC.Minigame;
                _updateAction($"You start talking to {_player.CurrentRoom.NPC.Name} and they challenge you to a game of {minigameName}.\n");

                try
                {
                    await _minigameFactory.CreateMinigameAsync(minigameName, _player);
                    _updateAction($"You finished playing {minigameName} with {_player.CurrentRoom.NPC.Name}.\n");
                }
                catch (Exception ex)
                {
                    _updateAction($"Failed to navigate to the minigame: {ex.Message}\n");
                }
            }
            else
            {
                _updateAction($"You talk to {_player.CurrentRoom.NPC.Name}, but they don't have a minigame for you.\n");
            }
        }
        else
        {
            _updateAction("There is no one here to talk to.\n");
        }
    }
}
using SustainACityMAUI.Models;
using SustainACityMAUI.Helpers;

namespace SustainACityMAUI.Commands;

public class TalkCommand : Command
{
    private readonly Player _player;
    private readonly NavigationService _navigationService;
    private readonly Action<string> _updateAction;

    public TalkCommand(Player player, NavigationService navigationService, Action<string> updateAction)
    {
        _player = player;
        _navigationService = navigationService;
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
                    await _navigationService.NavigateToMinigameAsync(minigameName, _player);
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
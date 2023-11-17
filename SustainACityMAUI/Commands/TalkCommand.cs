﻿using SustainACityMAUI.Models;
using SustainACityMAUI.Helpers;

namespace SustainACityMAUI.Commands;

public class TalkCommand : Command
{
    private readonly Player _player;
    private readonly Action<string, string> _updateAction;

    public TalkCommand(Player player, Action<string, string> updateAction)
    {
        _player = player;
        _updateAction = updateAction;
    }

    public override async void Execute(object parameter)
    {
        if (_player.CurrentRoom?.NPC == null)
        {
            _updateAction(null, "There is no one here to talk to.\n");
            return;
        }

        // Add NPC dialog and Quests into talk here
        // Add sub-commands (Minigame should be changed to its own sub-command)

        // Navigate to minigame logic
        bool canNavigate = await NavigationService.NavigateToMinigameAsync(_player);

        if (_player.CurrentRoom.NPC.Minigame == null)
        {
            _updateAction(null, $"You talk to {_player.CurrentRoom.NPC.Name}, but they don't have a minigame for you.\n");
        }
        else if (canNavigate)
        {
            _updateAction(null, $"You finished playing {_player.CurrentRoom.NPC.Minigame} with {_player.CurrentRoom.NPC.Name}.\n");
        }
        else
        {
            _updateAction(null, $"You talk to {_player.CurrentRoom.NPC.Name}, but they don't have a minigame for you.\n");
        }
    }
}
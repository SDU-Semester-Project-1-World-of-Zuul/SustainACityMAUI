using System;
using System.Windows.Input;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.Commands
{
    public class LookCommand : Command
    {
        private readonly Player _player;
        private readonly Action<string> _updateAction;

        public LookCommand(Player player, Action<string> updateAction)
        {
            _player = player;
            _updateAction = updateAction;
        }

        public override bool CanExecute(object parameter)
        {
            return _player.CurrentRoom != null;
        }

        public override void Execute(object parameter)
        {
            string description = (_player.CurrentRoom != null) ? $"{_player.CurrentRoom?.Description}\n" : "There is nothing to look at.\n";
            _updateAction(description);
        }
    }
}

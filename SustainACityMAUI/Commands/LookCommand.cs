using SustainACityMAUI.Models;

namespace SustainACityMAUI.Commands
{
    public class LookCommand : BaseCommand
    {
        private readonly Player _player;
        private readonly Action<string, string> _updateAction;

        public LookCommand(Player player, Action<string, string> updateAction)
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
            var description = $"{_player.CurrentRoom?.Description ?? "There is nothing to look at."}\n"; // Null-check
            _updateAction(null, description);
        }
    }
}
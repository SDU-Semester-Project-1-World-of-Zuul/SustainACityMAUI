using System.Text;

namespace SustainACityMAUI.Commands;

public class HelpCommand : Command
{
    private readonly Action<string> _updateAction;

    public HelpCommand(Action<string> updateAction)
    {
        _updateAction = updateAction;
    }

    public override void Execute(object parameter)
    {
        StringBuilder helpMessage = new();

        helpMessage.AppendLine("ACTION\t\tDESCRIPTION\n");
        helpMessage.AppendLine("[N] [S] [E] [W]\tMove through city districts.");
        helpMessage.AppendLine("[Look]\t\tDiscover location secrets.");
        helpMessage.AppendLine("[Talk]\t\tInteract for quests and info.");
        helpMessage.AppendLine("[Back]\t\tGo to previous location.");
        helpMessage.AppendLine("[❓]\t\tShow this help guide.");

        _updateAction(helpMessage.ToString());
    }
}
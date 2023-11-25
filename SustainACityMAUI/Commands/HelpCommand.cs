using System.Text;

namespace SustainACityMAUI.Commands;

public class HelpCommand : BaseCommand
{
    private readonly Action<string> _updateAction;

    public HelpCommand(Action<string> updateAction)
    {
        _updateAction = updateAction;
    }

    public override void Execute(object parameter)
    {
        var helpMessage = new StringBuilder();

        helpMessage.AppendLine("ACTION\t\tDESCRIPTION\n");
        helpMessage.AppendLine("[N] [S] [E] [W]\tMove through city districts.");
        helpMessage.AppendLine("[Look]\t\tDiscover location secrets.");
        helpMessage.AppendLine("[Talk]\t\tInteract for quests and info.");
        helpMessage.AppendLine("[Back]\t\tGo to previous location.");
        helpMessage.AppendLine("[Inventory]\tTo view and interact with items.");
        helpMessage.AppendLine("[❓]\t\tShow this help guide.");
        helpMessage.AppendLine("\nDIALOG OPTIONS\n");
        helpMessage.AppendLine("[Dialog Options]\tChoose from available responses in a conversation.");
        helpMessage.AppendLine("[→]\t\tPress to skip to the next part of the dialog.");

        _updateAction(helpMessage.ToString());
    }
}
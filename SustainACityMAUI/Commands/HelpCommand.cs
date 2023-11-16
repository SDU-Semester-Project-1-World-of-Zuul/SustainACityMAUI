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

        helpMessage.AppendLine("Here are the tools at your disposal:");
        helpMessage.AppendLine("➡️ 'N, S, E, W' - Use these compass buttons to explore the city's districts.");
        helpMessage.AppendLine("👁 'Look' - Reveal the secrets of a location and uncover hidden resources.");
        helpMessage.AppendLine("🗨️ 'Talk' - Engage with the citizens and wise entities that will offer quests and lore.");
        helpMessage.AppendLine("🔙 'Back' - Retrace your steps carefully to previously visited sites.");
        helpMessage.AppendLine("🆘 '❓' - Summon this guide anytime you seek wisdom or wish to review your tools.");
        helpMessage.AppendLine("Embark on your quest with courage and sustain our city's harmony with nature!");

        _updateAction($"{helpMessage}\n");
    }
}

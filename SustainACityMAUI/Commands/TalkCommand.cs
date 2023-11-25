using SustainACityMAUI.Helpers;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.Commands;

public class TalkCommand : BaseCommand
{
    private readonly Player _player;
    private readonly Action<string, string> _updateAction;
    private readonly Action<List<string>> _responseOptions;

    public TalkCommand(Player player, Action<List<string>> responseOptions, Action<string, string> updateAction)
    {
        _player = player;
        _updateAction = updateAction;
        _responseOptions = responseOptions;
    }

    public override void Execute(object parameter)
    {
        var response = (string)parameter;
        var npc = _player.CurrentRoom?.NPC;

        if (npc == null)
        {
            _updateAction(null, "There is no-one to talk to.");
            _responseOptions(new());
            return;
        }

        if (string.IsNullOrEmpty(response))
        {
            // First time talking to the NPC
            StartDialogue(npc);
        }
        else
        {
            // Player has selected a response
            RespondToDialogue(npc, response);
        }
    }

    private void StartDialogue(NPC npc)
    {
        var dialogue = npc.Dialogues.FirstOrDefault();
        if (dialogue != null)
        {
            _updateAction(npc.Name, dialogue.Text);
            _responseOptions(dialogue.Responses);
        }
    }

    private void RespondToDialogue(NPC npc, string response)
    {
        // Find the dialogue that contains the chosen response
        var dialogue = npc.Dialogues.Find(
            d => d.Responses.Contains(response)
            || d.FollowUpDialogue.Responses.Contains(response));
        if (dialogue == null)
        {
            // No dialogue found for the response, end the conversation
            _updateAction(npc.Name, "*Nods silently.*");
            _responseOptions(new());
            return;
        }

        // Check if the response is for starting a minigame
        if (response == dialogue.FollowUpDialogue.Responses[0] && npc.Quest.Minigame != null)
        {
            // Handle minigame response
            _ = HandleMinigameResponse(npc);
        }
        else if (dialogue.FollowUpDialogue != null)
        {
            // Continue with the follow-up dialogue based on the chosen response
            var nextDialogue = dialogue.FollowUpDialogue;

            _updateAction(npc.Name, nextDialogue.Text);
            _responseOptions(nextDialogue.Responses);
        }
        else
        {
            // End the dialogue if there is no follow-up
            _updateAction(npc.Name, "*Nods silently.*");
            _responseOptions(new());
        }
    }

    private async Task HandleMinigameResponse(NPC npc)
    {
        bool canNavigate = await NavigationService.NavigateToPageAsync(npc.Quest.Minigame, _player);
        if (canNavigate)
        {
            _updateAction(npc.Name, "*Is happily surprised you played their minigame*");
        }
        else
        {
            _updateAction(npc.Name, "Oh, seems like we can't do that right now...");
        }
        _responseOptions(new());
    }
}
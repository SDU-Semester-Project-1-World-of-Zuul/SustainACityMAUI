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
        var response = parameter as string;

        var npc = _player.CurrentRoom?.NPC;

        if (npc == null)
        {
            EndDialogue("There is no-one to talk to.");
            return;
        }

        if (string.IsNullOrEmpty(response))
        {
            StartDialogue(npc, npc.Dialogues.First().Id);
        }
        else
        {
            RespondToDialogue(npc, response);
        }
    }

    private void StartDialogue(NPC npc, string dialogueId)
    {
        var dialogue = npc.Dialogues.FirstOrDefault(d => d.Id == dialogueId);
        if (dialogue != null)
        {
            DisplayDialogue(npc.Name, dialogue.Text, dialogue.Responses);
        }
        else
        {
            EndDialogue("The conversation has ended.");
        }
    }

    private void RespondToDialogue(NPC npc, string responseText)
    {
        var response = npc.Dialogues
                        .SelectMany(d => d.Responses)
                        .FirstOrDefault(r => r.Text == responseText);

        // Check if the response text is "Accept" and create a new response
        if (responseText == "Accept")
        {
            response = new()
            {
                Text = "Accept",
                NextDialogueId = "acceptQuest"
            };
        }

        if (response != null)
        {
            if (response.NextDialogueId == "offerQuest")
            {
                OfferQuest(npc);
            }
            else if (responseText == "Accept")
            {
                _ = AcceptQuest(npc);
            }
            else
            {
                StartDialogue(npc, response.NextDialogueId);
            }
        }
        else
        {
            EndDialogue("You seem to be at a loss for words.");
        }
    }

    private void OfferQuest(NPC npc)
    {
        if (npc.Quest != null && !npc.Quest.IsCompleted)
        {
            _updateAction(npc.Name, npc.Quest.Description);
            _responseOptions(new(){ "Accept", "Decline" });
        }
        else
        {
            EndDialogue("No quest is available at this time.");
        }
    }

    private async Task AcceptQuest(NPC npc)
    {
        if (npc.Quest != null)
        {
            await npc.Quest.Execute(_player);
            npc.Quest.IsCompleted = true; // Or set this flag based on the quest completion status
            EndDialogue("You have completed the quest.");
        }
        else
        {
            EndDialogue("There is no quest to accept.");
        }
    }

    private void DisplayDialogue(string npcName, string text, List<DialogueResponse> responses)
    {
        _updateAction(npcName, text);
        _responseOptions(responses.Select(r => r.Text).ToList());
    }

    private void EndDialogue(string message)
    {
        _updateAction(null, message);
        _responseOptions(new());
    }
}
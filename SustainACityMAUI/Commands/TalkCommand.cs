using SustainACityMAUI.Models;

namespace SustainACityMAUI.Commands;

public class TalkCommand : BaseCommand
{
    private Player _player;
    private readonly Action<string, string> _updateAction;
    private readonly Action<List<string>> _responseOptions;
    private readonly Action<string> _updateNPCImagePath;
    private Dictionary<string, Action> _specialActions;
    private readonly List<DialogueResponse> _options = new()
    {
    new DialogueResponse { Text = "Oh, absolutely!", NextDialogueId = "acceptQuest" },
    new DialogueResponse { Text = "Maybe next time?", NextDialogueId = "declineQuest" }
    };

    public TalkCommand(Player player, Action<List<string>> responseOptions, Action<string, string> updateAction, Action<string> updateNPCImagePath)
    {
        _player = player;
        _updateAction = updateAction;
        _responseOptions = responseOptions;
        _updateNPCImagePath = updateNPCImagePath;
        _specialActions = new Dictionary<string, Action>
        {
        { "acceptQuest", async() => await AcceptQuest(_player.CurrentNPC) },
        { "declineQuest", () => EndDialogue("No worries! Not every quest is everyone's cup of tea. Maybe next time?")},
        { "offerQuest", () => OfferQuest(_player.CurrentNPC) }
        };
    }

    public override void Execute(object parameter)
    {
        var response = parameter as string;
        var npcs = _player.CurrentRoom?.NPCs;

        if (npcs?.Any() != true)
        {
            EndDialogue("Oh, how curious! It seems everyone has vanished into the ether. A moment of solitude, or perhaps something... else?");
            return;
        }

        if (_player.CurrentNPC != null && _player.CurrentRoom.NPCs.Contains(_player.CurrentNPC))
        {
            // Continue an ongoing dialogue with the current NPC
            HandleNPCInteraction(_player.CurrentNPC, response);
        }
        else
        {
            // New interaction or choosing an NPC to talk to
            ChooseNPC(npcs, response);
        }
    }

    private void ChooseNPC(List<NPC> npcs, string response)
    {
        var npcToTalk = (npcs.Count != 1) ? npcs.Find(npc => npc.Name == response) : npcs[0];

        if (npcToTalk != null)
        {
            _updateNPCImagePath(npcToTalk.ImgPath);
            _player.CurrentNPC = npcToTalk;
        }
        else
        {
            var npcResponses = npcs.ConvertAll(npc => new DialogueResponse { Text = npc.Name });
            DisplayDialogue(null, $"You notice {npcs.Count} intriguing individuals, each lost in their own world. Whose reality shall you step into?", npcResponses);
            return;
        }

        // Start dialogue with the first dialogue
        StartDialogue(_player.CurrentNPC, _player.CurrentNPC.Dialogues[0].Id);
    }

    private void HandleNPCInteraction(NPC npc, string response)
    {
        if (string.IsNullOrEmpty(response))
        {
            // Start dialogue with the first dialogue
            StartDialogue(npc, npc.Dialogues[0].Id);
        }
        else
        {
            // Continue an ongoing dialogue
            RespondToDialogue(response);
        }
    }

    private void StartDialogue(NPC npc, string dialogueId)
    {
        var dialogue = npc.Dialogues.Find(d => d.Id == dialogueId);
        if (dialogue != null)
        {
            DisplayDialogue(npc.Name, dialogue.Text, dialogue.Responses);
        }
        else
        {
            EndDialogue("Ah, the dialogue concludes, shrouded in mystery as it retreats into silence. Until our paths cross again in this curious cosmos.");
        }
    }

    private void RespondToDialogue(string responseText)
    {
        var response = _player.CurrentNPC.Dialogues
                                .SelectMany(d => d.Responses)
                                .Concat(_options)
                                .FirstOrDefault(r => r.Text == responseText);

        if (response != null)
        {
            HandleResponse(response);
        }
        else
        {
            EndDialogue("Hmm, speechless? Perhaps the words are hiding in the shadows, just out of reach. A curious predicament indeed!");
        }
    }

    private void HandleResponse(DialogueResponse response)
    {
        if (_specialActions.TryGetValue(response.NextDialogueId, out var action))
        {
            // Questing
            action.Invoke();
            return;
        }

        var nextDialogue = _player.CurrentNPC.Dialogues.Find(d => d.Id == response.NextDialogueId);
        if (nextDialogue != null)
        {
            if (nextDialogue.Responses.Count == 0)
            {
                // End Dialogue
                EndDialogue(nextDialogue.Text, _player.CurrentNPC.Name);
            }
            else
            {
                // Continue Dialogue
                StartDialogue(_player.CurrentNPC, response.NextDialogueId);
            }
        }
        else
        {
            // Catastrophic failure
            EndDialogue("Well, that took a turn into the unknown! Our chat concludes, shrouded in enigma. Until we unravel the mysteries again.");
        }
    }

    private void OfferQuest(NPC npc)
    {
        var availableQuest = npc.Quests.Find(q => !q.IsCompleted);
        if (availableQuest != null)
        {
            string questOfferMessage = $"Hey there! Fancy a task? {availableQuest.Title}\n{availableQuest.Description}";
            DisplayDialogue(npc.Name, questOfferMessage, _options);
        }
        else
        {
            EndDialogue("Alas, the stars are not aligned for a new quest at this moment. Perhaps when the cosmic winds shift, new paths will reveal themselves.");
        }
    }

    private async Task AcceptQuest(NPC npc)
    {
        var questToAccept = npc.Quests.Find(q => !q.IsCompleted);
        if (questToAccept != null)
        {
            await questToAccept.Execute(_player);
            questToAccept.IsCompleted = true;
            EndDialogue($"Quest complete! How fun was that? {questToAccept.Title}");
        }
        else
        {
            EndDialogue("Odd, no quest today. Perhaps the unseen forces aren't aligned just yet.");
        }
    }

    private void DisplayDialogue(string speaker, string text, List<DialogueResponse> responses)
    {
        _updateAction(speaker, text);
        _responseOptions(responses.ConvertAll(r => r.Text));
    }

    private void EndDialogue(string message, string speaker = null)
    {
        _updateAction(speaker, message);
        _responseOptions(new());
        _player.CurrentNPC = null;
    }
}
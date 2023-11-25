namespace SustainACityMAUI.Models;

public class Dialogue
{
    public string Text { get; set; }
    public List<string> Responses { get; set; }
    public Dialogue FollowUpDialogue { get; set; }
}
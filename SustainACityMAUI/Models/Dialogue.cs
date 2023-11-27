namespace SustainACityMAUI.Models;

public class Dialogue
{
    public string Id { get; set; }
    public string Text { get; set; }
    public List<DialogueResponse> Responses { get; set; }
}
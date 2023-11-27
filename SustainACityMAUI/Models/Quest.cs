namespace SustainACityMAUI.Models;

public abstract class Quest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public abstract Task Execute(Player player);
}
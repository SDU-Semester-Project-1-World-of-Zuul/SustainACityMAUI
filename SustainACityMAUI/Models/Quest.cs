namespace SustainACityMAUI.Models;

/// <summary> Represents a quest with multiple choice options. </summary>
public class Quest
{
    public string Description { get; set; }     // Quest's main narrative
    public ChoiceOption Good { get; set; }      // Positive choice option
    public ChoiceOption Neutral { get; set; }   // Neutral choice option
    public ChoiceOption Bad { get; set; }       // Negative choice option

    /// <summary> Retrieves a choice option based on its type. </summary>
    public ChoiceOption GetChoice(string choiceType)
    {
        return choiceType.ToLower() switch
        {
            "good" => Good,
            "neutral" => Neutral,
            "bad" => Bad,
            _ => null  // Unrecognized choice type
        };
    }

    /// <summary> Represents a possible choice in a quest. </summary>
    public class ChoiceOption
    {
        public string Action { get; set; }   // Action description of the choice
        public string Outcome { get; set; }  // Resulting narrative of the choice
        public int Score { get; set; }       // Score or impact of the choice
    }
}

namespace SustainACityMAUI.Models;

public class Quest
{
    public string Description { get; set; }
    public ChoiceOption Good { get; set; }
    public ChoiceOption Neutral { get; set; }
    public ChoiceOption Bad { get; set; }

    public ChoiceOption GetChoice(string choiceType)
    {
        return choiceType.ToLower() switch
        {
            "good" => Good,
            "neutral" => Neutral,
            "bad" => Bad,
            _ => null
        };
    }

    public class ChoiceOption
    {
        public string Action { get; set; }
        public string Outcome { get; set; }
        public int Score { get; set; }
    }
}

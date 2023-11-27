using SustainACityMAUI.Helpers;
using SustainACityMAUI.Models;
using System.Windows.Input;
using static SustainACityMAUI.ViewModels.CompostMinigame.Compost;

namespace SustainACityMAUI.ViewModels;

public class CompostMinigame : BaseViewModel
{
    public struct Compost
    {
        public enum WasteType { Fruit, Vegetable, Coffee, Tea, Grass, Leaves, Eggshell, Paper }

        private Dictionary<WasteType, int> waste;

        public Compost()
        {
            waste = new Dictionary<WasteType, int>();
        }

        public void AddWaste(WasteType type, int quantity)
        {
            if (waste.ContainsKey(type))
            {
                waste[type] += quantity;
            }
            else
            {
                waste.Add(type, quantity);
            }
        }

        public int GetQuality()
        {
            int quality = 0;
            foreach (var pair in waste)
            {
                switch (pair.Key)
                {
                    case WasteType.Fruit:
                        quality += 2 * pair.Value;
                        break;
                    case WasteType.Vegetable:
                        quality += 3 * pair.Value;
                        break;
                    case WasteType.Coffee:
                        quality += 4 * pair.Value;
                        break;
                    case WasteType.Tea:
                        quality += 2 * pair.Value;
                        break;
                    case WasteType.Grass:
                        quality -= 1 * pair.Value;
                        break;
                    case WasteType.Leaves:
                        quality += 1 * pair.Value;
                        break;
                    case WasteType.Eggshell:
                        quality += 5 * pair.Value;
                        break;
                    case WasteType.Paper:
                        quality -= 2 * pair.Value;
                        break;
                }
            }
            return quality;
        }

        public bool HasBeneficialCreatures()
        {
            return waste.Any(p => p.Key == WasteType.Fruit || p.Key == WasteType.Vegetable ||
                                  p.Key == WasteType.Coffee || p.Key == WasteType.Tea) &&
                   waste.Any(p => p.Value > 0);
        }

        public bool HasPests()
        {
            return waste.Any(p => (p.Key == WasteType.Grass && p.Value > 10) ||
                                  (p.Key == WasteType.Paper && p.Value > 10));
        }
    }

    private readonly Player _player;
    private int choiceCount;
    private const int MaxChoices = 6; // Example limit
    private Compost compost;
    private int score;
    private int quality;
    private string wasteOption;
    private string wasteQuantity;
    private string wasteSource;

    public ICommand AddWasteCommand => new Command(AddWaste);
    public ICommand SkipWasteCommand => new Command(SkipWaste);

    public CompostMinigame(Player player)
    {
        _player = player;
        choiceCount = 0;
        compost = new Compost();
        GenerateRandomWasteOption();
    }

    // Properties for Data Binding
    public int Score
    {
        get => score;
        set {
            score = value;
            OnPropertyChanged();
        }
    }

    public int Quality
    {
        get => quality;
        set
        {
            quality = value;
            OnPropertyChanged();

            // Assuming 100 is the max quality, adjust if your max quality is different
            Progress = quality / 100.0;
            OnPropertyChanged(nameof(Progress));
        }
    }

    public string WasteOption
    {
        get => wasteOption;
        set
        {
            wasteOption = value;
            OnPropertyChanged();
        }
    }

    public string WasteQuantity
    {
        get => wasteQuantity;
        set
        {
            wasteQuantity = value;
            OnPropertyChanged();
        }
    }

    public string WasteSource
    {
        get => wasteSource;
        set
        {
            wasteSource = value;
            OnPropertyChanged();
        }
    }
    public double Progress { get; private set; }
    public string EndGameMessage { get; private set; }
    public bool IsGameEnded { get; private set; }

    private void AddWaste()
    {
        // Assuming wasteOption is set to the type of waste to be added
        // and wasteQuantity is the amount of that waste
        Compost.WasteType selectedWasteType = ParseWasteType(wasteOption);
        int selectedQuantity = int.Parse(wasteQuantity);

        compost.AddWaste(selectedWasteType, selectedQuantity);

        // Update quality based on the compost's current state
        Quality = compost.GetQuality();

        // Check for beneficial creatures and pests
        if (compost.HasBeneficialCreatures())
        {
            Score += 10;
        }
        if (compost.HasPests())
        {
            Score -= 10;
        }

        // Generate the next waste option
        GenerateRandomWasteOption();

        UpdateGameState();
    }

    private readonly Random random = new();

    private void GenerateRandomWasteOption()
    {
        WasteType wasteType = GetRandomWasteType();
        int quantity = GetRandomQuantity();
        string source = GetRandomSource();

        WasteOption = wasteType.ToString();
        WasteQuantity = quantity.ToString();
        WasteSource = source;
    }

    private Compost.WasteType GetRandomWasteType()
    {
        Array values = Enum.GetValues(typeof(Compost.WasteType));
        return (Compost.WasteType)values.GetValue(random.Next(values.Length));
    }

    private int GetRandomQuantity()
    {
        return random.Next(1, 6); // Random quantity between 1 and 5
    }

    private string GetRandomSource()
    {
        string[] sources = new string[] { "home", "cafe", "park" };
        return sources[random.Next(sources.Length)];
    }

    private void SkipWaste()
    {
        // Generate the next waste option without modifying the compost
        GenerateRandomWasteOption();

        UpdateGameState();
    }

    private Compost.WasteType ParseWasteType(string wasteTypeString)
    {
        if (Enum.TryParse(wasteTypeString, out Compost.WasteType wasteType))
        {
            return wasteType;
        }

        throw new ArgumentException("Invalid waste type string");
    }

    private void UpdateGameState()
    {
        if (++choiceCount >= MaxChoices)
        {
            OnGameEnded();
        }
        else
        {
            GenerateRandomWasteOption();
        }
    }

    private async void OnGameEnded()
    {
        _player.Score += Score; // Update game score
        IsGameEnded = true;
        EndGameMessage = $"Game Over! Your final score is {Score}.";
        OnPropertyChanged(nameof(EndGameMessage));
        OnPropertyChanged(nameof(IsGameEnded));

        await Task.Delay(3000);

        await NavigationService.NavigateBackAsync(); // Return to Game Page
    }
}
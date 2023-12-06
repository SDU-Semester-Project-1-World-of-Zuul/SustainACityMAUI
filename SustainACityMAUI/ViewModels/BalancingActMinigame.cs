using System.Windows.Input;
using SustainACityMAUI.Helpers;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.ViewModels;

public class BalancingActMinigame : BaseViewModel
{
    private readonly Random random = new();
    private List<int> _values;
    private int _difficultyLevel = 1;
    private int _attemptsLeft = 3;
    private const int _initialNumbersCount = 3;
    private const int _maxAttempts = 5;
    private readonly Player _player;
    private bool _isGameEnded = false;

    private int _firstPosition;
    private int _secondPosition;

    public BalancingActMinigame(Player player)
    {
        _player = player;
        StartNewRoundCommand = new Command(StartNewRound);
        AdjustNumbersCommand = new Command(AdjustNumbers);

        StartNewRound();
    }

    public string NumbersDisplay => string.Join(", ", _values);

    public int FirstPosition
    {
        get => _firstPosition;
        set
        {
            _firstPosition = value;
            OnPropertyChanged();
        }
    }

    public int SecondPosition
    {
        get => _secondPosition;
        set
        {
            _secondPosition = value;
            OnPropertyChanged();
        }
    }

    public int AttemptsLeft
    {
        get => _attemptsLeft;
        set
        {
            _attemptsLeft = value;
            OnPropertyChanged();
        }
    }

    public int DifficultyLevel
    {
        get => _difficultyLevel;
        set
        {
            _difficultyLevel = value;
            OnPropertyChanged();
        }
    }

    public int Score { get; private set; }

    public ICommand StartNewRoundCommand { get; }
    public ICommand AdjustNumbersCommand { get; }

    private void StartNewRound()
    {
        _values = GenerateValues(_initialNumbersCount + DifficultyLevel - 1).ToList();
        OnPropertyChanged(nameof(Score));
        OnPropertyChanged(nameof(NumbersDisplay));
        AttemptsLeft = _maxAttempts;
    }

    private async void AdjustNumbers()
    {
        int pos1 = FirstPosition - 1;
        int pos2 = SecondPosition - 1;

        if (pos1 >= 0 && pos1 < _values.Count && pos2 >= 0 && pos2 < _values.Count)
        {
            int average = (_values[pos1] + _values[pos2]) / 2;
            _values[pos1] = _values[pos2] = average;

            OnPropertyChanged(nameof(NumbersDisplay));

            if (CheckIfBalanced())
            {
                Score += CalculateScore(DifficultyLevel);
                DifficultyLevel++;
                StartNewRound();
            }
            else
            {
                AttemptsLeft--;
                if (AttemptsLeft <= 0)
                {
                    Score -= CalculatePenalty(DifficultyLevel);
                    DifficultyLevel++;
                    await OnGameEnded();
                    StartNewRound();
                }
            }
        }
    }

    private IEnumerable<int> GenerateValues(int count)
    {
        int baseNumber = random.Next(1, 51) * 2; // Ensures baseNumber is even

        var values = Enumerable.Repeat(baseNumber, count).ToList();

        if (count % 2 != 0 && values.Sum() % 2 != 0)
        {
            // If count is odd, make one of the numbers odd to keep the total sum even
            values[count - 1]++;
        }

        for (int i = 0; i < count - 1; i += 2)
        {
            int adjustment = random.Next(-10, 11); // Random adjustment value
            values[i] += adjustment;
            values[i + 1] -= adjustment; // Adjust the pair by the opposite amount
        }

        return values;
    }

    private bool CheckIfBalanced()
    {
        return _values.Distinct().Count() == 1;
    }

    private int CalculateScore(int level)
    {
        return 10 * level;
    }

    private int CalculatePenalty(int level)
    {
        return 5 * level;
    }

    private async Task OnGameEnded()
    {
        if (_isGameEnded) return;

        await Application.Current.MainPage.DisplayAlert("Game Over", $"You failed to balance the numbers.\nFinal Level: {DifficultyLevel}\nFinal score: {Score}", "Go Back");

        _isGameEnded = true;
        _player.Score += Score;
        _ = NavigationService.NavigateBackAsync();
    }
}
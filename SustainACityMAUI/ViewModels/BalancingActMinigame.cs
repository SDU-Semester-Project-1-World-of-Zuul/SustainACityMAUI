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
    private const int _initialNumbersCount = 2;
    private const int _maxAttempts = 5;
    private readonly Player _player;
    private bool _isGameEnded = false;

    private int _firstPosition;
    private int _secondPosition;
    private List<int> _positions;

    public BalancingActMinigame(Player player)
    {
        _player = player;
        StartNewRoundCommand = new Command(StartNewRound);
        AdjustNumbersCommand = new Command(AdjustNumbers);

        StartNewRound();
    }

    public string NumbersDisplay => string.Join(", ", _values);
    
    public List<int> Positions
    {
        get => _positions;
        set
        {
            _positions = value;
            OnPropertyChanged();
        }
    }
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
        _values = GenerateValues(_initialNumbersCount + ((DifficultyLevel - 1) * 2)).ToList();
        Positions = Enumerable.Range(1, _values.Count).ToList();
        OnPropertyChanged(nameof(Score));
        OnPropertyChanged(nameof(NumbersDisplay));
        AttemptsLeft = CalculateMaxAttempts(DifficultyLevel);
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
        // Generate only even numbers to avoid decimals
        return Enumerable.Range(1, count).Select(_ => random.Next(1, 51) * 2);
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

    private int CalculateMaxAttempts(int difficultyLevel)
    {
        // Example logic: Base max attempts plus an additional attempt every 2 levels
        return _maxAttempts + (difficultyLevel / 2);
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
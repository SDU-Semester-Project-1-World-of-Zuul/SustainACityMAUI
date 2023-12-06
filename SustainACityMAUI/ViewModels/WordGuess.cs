using System.Collections.ObjectModel;
using System.Windows.Input;
using SustainACityMAUI.Helpers;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.ViewModels;

public class WordGuess : BaseViewModel
{
    private readonly Player _player;
    private bool _isGameEnded = false;
    public ICommand SubmitAnswerCommand => new Command(SubmitAnswer);
    public List<ShuffledWord> Words { get; }
    private string _currentAnswer;
    private readonly IDispatcher _dispatcher;
    public int _score;
    public int _timeRemaining;
    public ShuffledWord _currentWord;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnPropertyChanged();
        }
    }
    
    public int TimeRemaining
    {
        get => _timeRemaining;
        set
        {
            _timeRemaining = value;
            OnPropertyChanged();
        }
    }
    
    public string CurrentAnswer
    {
        get => _currentAnswer;
        set
        {
            _currentAnswer = value;
            OnPropertyChanged();
        }
    }

    public ShuffledWord CurrentWord
    {
        get => _currentWord;
        private set
        {
            _currentWord = value;
            OnPropertyChanged();
        }
    }
    
    public class ShuffledWord
    {
        public string Word { get; set; }
        public string Explanation { get; set; }
        public int Points { get; set; }
        public string Shuffled { get; set; }

        public ShuffledWord(string word, string explanation, int points)
        {
            Word = word;
            Explanation = explanation;
            Points = points;
            
            Random rnd = new Random();
            string shuffled = null;
            int length = word.Length;
            for (int i = 0; i < length; i++)
            {
                int randomIndex = rnd.Next(word.Length);
                shuffled += word[randomIndex];
                word = word.Remove(randomIndex,1);
            }

            Shuffled = shuffled;
        }
    }
    
    private static List<ShuffledWord> ShuffledWords
    {
        get
        {
            var words = new List<ShuffledWord>()
            {
                new ShuffledWord(
                    "Upgrading",
                    "Improving the quality of housing and infrastructure in slums.",
                    3),
                new ShuffledWord(
                    "Inclusion",
                    "Ensuring that slum residents have access to basic services and amenities.",
                    3),
                new ShuffledWord(
                    "Resilience",
                    "Building slums that can withstand natural disasters and other shocks.",
                    3),
                new ShuffledWord(
                    "Sustainability",
                    "Creating slums that are environmentally friendly and resource-efficient.",
                    3),
                new ShuffledWord(
                    "Empowerment",
                    "Giving slum residents a voice in the decisions that affect their lives.",
                    3),
                new ShuffledWord(
                    "Participation",
                    "Encouraging slum residents to take part in the planning and management of their communities.",
                    3),
                new ShuffledWord(
                    "Security",
                    "Ensuring that slum residents are safe from crime and violence.",
                    3),
                new ShuffledWord(
                    "Health",
                    "Improving the well being of slum residents through better sanitation and facilities.",
                    3),
                new ShuffledWord(
                    "Education",
                    "Providing slum residents with access to quality learning and training.",
                    3),
                new ShuffledWord(
                    "Livelihoods",
                    "Creating opportunities for slum residents to improve their economic situation.",
                    3),
            };
            Random rnd = new Random();
            return words.OrderBy(_ => rnd.Next()).ToList();
        }
    }
    
    private void StartTimer()
    {
        _dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining--;
                return true; // Continue the timer
            }
            else
            {
                // Game ends, handle accordingly
                OnGameEnded();
                return false; // Stop the timer
            }
        });
    }

    public WordGuess(Player player)
    {
        _player = player;
        _dispatcher = Dispatcher.GetForCurrentThread();
        Words = new List<ShuffledWord>(ShuffledWords);
        CurrentWord = Words.FirstOrDefault();
        TimeRemaining = 100;
        StartTimer();
    }

    private void SubmitAnswer()
    {
        if (Words.Count > 0)
        {
            if (_currentAnswer == CurrentWord.Word)
                Score += CurrentWord.Points;

            Words.Remove(CurrentWord);
            CurrentWord = Words.FirstOrDefault();
            OnPropertyChanged(nameof(Words));
            CurrentAnswer = string.Empty;
        }

        if (Words.Count == 0)
            OnGameEnded();
    }

    private void OnGameEnded()
    {
        if (_isGameEnded) return;

        _isGameEnded = true;

        _player.Score += Score;
        _ = NavigationService.NavigateBackAsync();
    }
}
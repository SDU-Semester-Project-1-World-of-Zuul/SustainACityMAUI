using System.Collections.ObjectModel;
using System.Windows.Input;
using SustainACityMAUI.Helpers;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.ViewModels;

public class QuizMinigame : ViewModel
{
    private readonly Player _player;
    public ObservableCollection<TriviaQuestion> Questions { get; }
    public ICommand SubmitAnswerCommand { get; }

    private readonly IDispatcher _dispatcher;
    private int _triviaScore;
    private int _timeRemaining;
    private TriviaQuestion _currentQuestion;

    public struct TriviaQuestion
    {
        public string Query { get; set; }
        public string[] Options { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsBonus { get; set; }

        public TriviaQuestion(string query, string[] options, string correctAnswer, bool isBonus = false)
        {
            Query = query;
            Options = options;
            CorrectAnswer = correctAnswer;
            IsBonus = isBonus;
        }
    }

    private string _currentAnswer;

    public string CurrentAnswer
    {
        get => _currentAnswer;
        set
        {
            _currentAnswer = value;
            OnPropertyChanged();
        }
    }

    public int TriviaScore
    {
        get => _triviaScore;
        set
        {
            _triviaScore = value;
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

    public TriviaQuestion CurrentQuestion
    {
        get => _currentQuestion;
        private set
        {
            _currentQuestion = value;
            OnPropertyChanged();
        }
    }

    public QuizMinigame(Player player)
    {
        _player = player;
        _dispatcher = Dispatcher.GetForCurrentThread();
        Questions = new ObservableCollection<TriviaQuestion>(ShuffledQuestions);
        CurrentQuestion = Questions.FirstOrDefault();
        SubmitAnswerCommand = new Command<string>(SubmitAnswer);
        TimeRemaining = 60;
        StartTimer();
    }

    private static IEnumerable<TriviaQuestion> ShuffledQuestions
    {
        get
        {
            var questions = new List<TriviaQuestion> // Should probably be a json file
        {
            // Populate with school and sustainability-themed questions
            new TriviaQuestion(
                "What percentage of paper can be saved by printing double-sided in schools?",
                new[] {"25%", "50%", "75%", "100%"},
                "50%"),

            new TriviaQuestion(
                "Identify an alternative to driving that can reduce the school's carbon footprint.",
                new[] {"Carpooling", "Walking", "Biking", "All of the above"},
                "All of the above"),

            // Additional school-related questions
            new TriviaQuestion(
                "What is the most energy-efficient way to conduct a class during daytime?",
                new[] {"Using electric lights", "Using natural sunlight", "Using candles", "Classes do not need light"},
                "Using natural sunlight"),

            new TriviaQuestion(
                "Which school supply can be reused to minimize waste?",
                new[] {"Pencil shavings", "Adhesive stickers", "Plastic binders", "Single-use pens"},
                "Plastic binders"),

            // A challenging question that adds a twist
            new TriviaQuestion(
                "Calculate the amount of water saved annually by fixing a dripping faucet in a school bathroom.",
                new[] {"1,000 gallons", "3,000 gallons", "5,000 gallons", "10,000 gallons"},
                "3,000 gallons",
                isBonus: true)
        };

            return questions.OrderBy(_ => Guid.NewGuid());
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

    private void SubmitAnswer(string currentAnswer)
    {
        if (Questions.Count > 0)
        {
            if (currentAnswer == CurrentQuestion.CorrectAnswer)
                TriviaScore += CurrentQuestion.IsBonus ? 2 : 1; // Update score only if correct

            // Proceed to the next question whether the answer was correct or not
            Questions.Remove(CurrentQuestion);
            CurrentQuestion = Questions.FirstOrDefault(); // Move to the next question
            OnPropertyChanged(nameof(Questions));
        }

        // If there are no more questions, end the game
        if (Questions.Count == 0)
            OnGameEnded();
    }

    private void OnGameEnded()
    {
        _player.Score += TriviaScore; // Update game score
        _ = NavigationService.NavigateBackAsync();
    }
}
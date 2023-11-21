using System.Collections.ObjectModel;
using System.Windows.Input;
using SustainACityMAUI.Helpers;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.ViewModels;

public class CrisisManagementMinigame : ViewModel
{
    private readonly Player _player;
    private List<DayDecision> _days;

    private readonly List<string> _decisions = new List<string>
{
    // Good decisions
    "🏗️ Construct a hydroelectric dam",
    "🏃‍♀️ Coordinate a city-wide evacuation",
    "💰 Allocate additional funds to emergency services",
    "🏢 Upgrade city infrastructure to withstand floods",
    "👨‍🔬 Consult with environmental scientists",
    "🎓 Launch a public education campaign about flood safety",
    "💸 Organize a fundraising campaign for flood victims",
    "🔬 Fund research into advanced flood prediction technology",
    "👥 Establish a dedicated disaster response team",
    "🏠 Promote the construction of flood-resistant buildings",

    // Neutral decisions
    "🙏 Hold a city-wide day of prayer",
    "🌤️ Invest in improved weather forecasting technology",
    "🚰 Overhaul the city's drainage systems",
    "💵 Encourage residents to purchase flood insurance",
    "🔄 Develop a comprehensive flood recovery plan",

    // Bad decisions
    "🙈 Ignore the flood warnings",
    "🚨 Prematurely declare a state of emergency",
    "🌊 Construct makeshift flood barriers",
    "⚠️ Implement a basic flood warning system",
    "🌱 Implement new soil management practices without research",
};

    public ICommand GoBackCommand { get; set; }
    public ICommand SubmitDecisionsCommand { get; set; }
    public ICommand SelectDecisionCommand { get; set; }

    public List<DayDecision> Days
    {
        get { return _days; }
        set
        {
            _days = value;
            OnPropertyChanged(nameof(Days));
        }
    }

    public CrisisManagementMinigame(Player player)
    {
        _player = player;
        GoBackCommand = new Command(GoBack);
        SubmitDecisionsCommand = new Command(SubmitDecisions);
        SelectDecisionCommand = new Command<string>(SelectDecision);

        Days = new List<DayDecision>();
        for (int i = 1; i <= 10; i++)
        {
            Days.Add(new DayDecision
            {
                Day = $"Day {i}",
                Decisions = new ObservableCollection<string>(_decisions)
            });
        }
    }
    private void GoBack()
    {
        // Implement going back
    }
    private void SubmitDecisions()
    {
        // Implement decision submission
    }

    private void SelectDecision(string decision)
    {
        // Implement decision selection
    }
    public class DayDecision : ViewModel
    {
        private string _day;
        private string _selectedDecision;
        private ObservableCollection<string> _decisions;

        public string Day
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged(nameof(Day));
            }
        }

        public string SelectedDecision
        {
            get { return _selectedDecision; }
            set
            {
                _selectedDecision = value;
                OnPropertyChanged(nameof(SelectedDecision));
            }
        }

        public ObservableCollection<string> Decisions
        {
            get { return _decisions; }
            set
            {
                _decisions = value;
                OnPropertyChanged(nameof(Decisions));
            }
        }
    }

}
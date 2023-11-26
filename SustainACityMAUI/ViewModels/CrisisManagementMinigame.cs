
using System.Collections.ObjectModel;
using System.Windows.Input;
using SustainACityMAUI.Helpers;
using SustainACityMAUI.Models;

namespace SustainACityMAUI.ViewModels;
public class CrisisManagementMinigame : BaseViewModel
{

    // Fields
    private readonly Player _player;
    private int _currentDay;
    private Action _selectedAction;
    private ObservableCollection<Action> _actions;
    private int _days;
    private Random random = new Random();
    private System.Timers.Timer timer;

    private bool _isStartButtonVisible = true;

    private bool _isListViewVisible = false;
    private bool _isScoreLabelVisible = false;

    private bool _isSubmitButtonVisible = false;
    private int _temperature;
    private int _rainfall;
    private int _drainage;

    private int _waterLevel;

    // Properties

    public int CurrentDay
    {
        get => _currentDay;
        set
        {
            _currentDay = value;
            OnPropertyChanged();
        }
    }

    public int Days
    {
        get => _days;
        set
        {
            _days = value;
            OnPropertyChanged();
        }
    }

    public int Temperature
    {
        get => _temperature;
        set
        {
            _temperature = value;
            OnPropertyChanged();
        }
    }

    public int Rainfall
    {
        get => _rainfall;
        set
        {
            _rainfall = value;
            OnPropertyChanged();
        }
    }

    public int Drainage
    {
        get => _drainage;
        set
        {
            _drainage = value;
            OnPropertyChanged();
        }
    }

    public int WaterLevel
    {
        get => _waterLevel;
        set
        {
            _waterLevel = value;
            OnPropertyChanged();
        }
    }
    public class Action
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
    }

    public Action SelectedAction
    {
        get => _selectedAction;
        set
        {
            _selectedAction = value;
            OnPropertyChanged();
        }
    }

    public bool IsStartButtonVisible
    {
        get { return _isStartButtonVisible; }
        set
        {
            _isStartButtonVisible = value;
            OnPropertyChanged(nameof(IsStartButtonVisible));
        }
    }

    public bool IsListViewVisible
    {
        get { return _isListViewVisible; }
        set
        {
            _isListViewVisible = value;
            OnPropertyChanged(nameof(IsListViewVisible));
        }
    }

    public bool IsScoreLabelVisible
    {
        get { return _isScoreLabelVisible; }
        set
        {
            _isScoreLabelVisible = value;
            OnPropertyChanged(nameof(IsScoreLabelVisible));
        }
    }

    public bool IsSubmitButtonVisible
    {
        get { return _isSubmitButtonVisible; }
        set
        {
            _isSubmitButtonVisible = value;
            OnPropertyChanged(nameof(IsSubmitButtonVisible));
        }
    }
    public ICommand MoveUpCommand { get; }
    public ICommand MoveDownCommand { get; }
    public ICommand ShowDetailsCommand { get; }
    public ICommand NextDayCommand { get; }
    public ICommand SubmitCommand { get; private set; }
    public ICommand StartGameCommand => new Command(() =>
  {
      IsStartButtonVisible = false;
      IsListViewVisible = true;
  });

    public ICommand GoBackCommand => new Command(() =>
    {
        _ = NavigationService.NavigateBackAsync();
    });

    [Obsolete]
    public CrisisManagementMinigame(Player player)
    {
        _player = player;
        CurrentDay = 1;
        Days = 7; // for a week-long game
        Actions = new ObservableCollection<Action>
    {
        new Action { Id = 1, Name = "Plant Trees 🌳",Score= 8, Description = "Planting trees helps to reduce the water level by absorbing water. Trees take up water from the soil and release it into the atmosphere, a process known as transpiration. This not only helps to lower the water level but also contributes to the local climate and biodiversity." },

        new Action { Id = 2, Name = "Build Dams 🏗️", Score = 10, Description = "Building dams can control the flow of water and prevent flooding. Dams store water and release it in a controlled manner, preventing sudden increases in water level downstream. They also provide a source of hydroelectric power, which is a clean and renewable source of energy." },

        new Action { Id = 3, Name = "Improve Drainage 🚧", Score = 9, Description = "Improving the drainage system can effectively reduce the water level. A well-designed drainage system can quickly remove excess water from the surface, preventing waterlogging and flooding. It also helps to manage water resources more effectively and can improve the quality of local water bodies by preventing pollution." },

        new Action { Id = 4, Name = "Promote Water Conservation 💧", Score = 7 , Description = "Promoting water conservation can reduce the demand for water and lower the water level. By encouraging people to use water more efficiently, we can reduce the amount of water that needs to be extracted from natural sources. This not only helps to lower the water level but also ensures a sustainable supply of water for future generations." },

        new Action { Id = 5, Name = "Implement Rainwater Harvesting ☔", Score = 6 , Description = "Implementing rainwater harvesting can reduce the water level and provide a source of water. Rainwater harvesting systems collect and store rainwater for later use, reducing the demand for water from other sources. This not only helps to lower the water level but also provides a reliable and sustainable source of water, especially in areas with limited water resources." },

        new Action { Id = 6, Name = "Restore Wetlands 🌾", Score = 8 ,Description = "Restoring wetlands can help to absorb excess water and reduce the water level. Wetlands act as natural sponges, absorbing and storing excess water and slowly releasing it over time. This not only helps to lower the water level but also improves local biodiversity and provides a habitat for many species." },

        new Action { Id = 7, Name = "Upgrade Infrastructure 🏢", Score = 7 ,Description = "Upgrading infrastructure can improve water management and reduce the water level. This includes improving the design and construction of buildings and roads to make them more water-efficient, and upgrading water supply and sewage systems to reduce water loss and pollution." },

        new Action { Id = 8, Name = "Ignore the Problem 😴",Score = -5, Description = "Ignoring the problem will lead to an increase in the water level. Without taking any action to manage the water level, it will continue to rise due to natural processes and human activities. This can lead to flooding and other water-related problems." },

        new Action { Id = 9, Name = "Increase Water Usage 🚿", Score = -4, Description = "Increasing water usage will lead to an increase in the water level. More water usage means more water is extracted from natural sources, which can lead to a rise in the water level. It also puts more pressure on water resources, leading to potential water shortages in the future." },

        new Action { Id = 10, Name = "Pollute the Water 💀", Score = -10, Description = "Polluting the water can make it unusable and increase the water level. Water pollution can come from various sources, including industrial waste, agricultural runoff, and domestic sewage. Polluted water is not only harmful to human health and the environment, but it also increases the water level as it cannot be used effectively." },

        new Action { Id = 11, Name = "Monitor Situation 👀", Score = 2, Description = "Monitoring the situation doesn't directly affect the water level but keeps you informed. By keeping a close eye on the water level and other related factors, you can make informed decisions about what actions to take. This can help to prevent sudden increases in the water level and mitigate the impact of any potential problems." },

        new Action { Id = 12, Name = "Research Solutions 📚", Score = 3, Description = "Researching solutions doesn't directly affect the water level but can lead to future actions. By studying the problem and exploring potential solutions, you can develop effective strategies for managing the water level. This can lead to more effective actions in the future, helping to keep the water level under control." }
        // Add more actions as needed for now just 12 in total. Maybe I could add more later
    };
        // Initialize the timer with a 12-second interval
        timer = new System.Timers.Timer(12000);

        // Set the method to call when the timer elapses
        timer.Elapsed += (sender, e) => GenerateData();

        MoveUpCommand = new Command<Action>(MoveUp);
        MoveDownCommand = new Command<Action>(MoveDown);
        ShowDetailsCommand = new Command<Action>(ShowDetails);
        SubmitCommand = new Command(Submit);

    }

    public void GenerateData()
    {
        // Generate random values for temperature, rainfall, and drainage system
        // Increase the range of possible values based on the current day
        Temperature = random.Next(1 + CurrentDay * 5, 40); // Temperature in degrees Celsius
        Rainfall = random.Next(50, 100 + CurrentDay * 10); // Rainfall in mm, minimum is 50
        Drainage = random.Next(0, 25 + CurrentDay * 10); // Drainage system capacity in %

        // Calculate the water level based on these values
        // This is a more complex calculation that considers more factors
        double effectiveDrainage = Drainage * Math.Max(0, 1 - Rainfall / 200.0);
        double evaporation = Temperature > 0 ? Temperature / 100.0 : 0;
        int newWaterLevel = (int)(Rainfall - effectiveDrainage - evaporation * WaterLevel);

        // Ensure the new water level is at least 1
        newWaterLevel = Math.Max(newWaterLevel, 1);

        // Add the new water level to the old one
        WaterLevel += newWaterLevel;

        // Ensure the water level doesn't go below zero
        if (WaterLevel < 0)
        {
            WaterLevel = 0;
        }


        // Notify the UI that the WaterLevel has changed
        OnPropertyChanged(nameof(WaterLevel));
    }

    public ObservableCollection<Action> Actions
    {
        get => _actions;
        set
        {
            _actions = value;
            OnPropertyChanged();
        }
    }

    public void StartGame()
    {
        // Start the timer
        timer.Start();
        IsSubmitButtonVisible = true;

        // Generate initial data
        GenerateData();
    }

    public void StopGame()
    {
        // Stop the timer
        timer.Stop();
    }


    // Move the selected action up or down in the list
    private void MoveUp(Action action)
    {
        var index = Actions.IndexOf(action);
        if (index > 0)
        {
            Actions.Move(index, index - 1);
        }
    }

    private void MoveDown(Action action)
    {
        var index = Actions.IndexOf(action);
        if (index < Actions.Count - 1)
        {
            Actions.Move(index, index + 1);
        }
    }

    // Show the details of the selected action
    private async void ShowDetails(Action action)
    {
        await Application.Current.MainPage.DisplayAlert(action.Name, action.Description, "Back to the game");
    }


    // Submit the selected actions and calculate the score
    [Obsolete]
    private async void Submit()
    {
        CalculateScore();
        StopGame();
        await Application.Current.MainPage.DisplayAlert("Score", $"Congrats! 🎉. Your score is {_player.Score}", "Go Back");
        IsSubmitButtonVisible = false;
        IsListViewVisible = false;
        IsScoreLabelVisible = true;

        // Execute the GoBackCommand
        GoBackCommand.Execute(null);
    }

    // Calculate the score based on the current water level, time taken, and selected actions
    public void CalculateScore()
    {
        int waterLevelFactor = 100 - _waterLevel; // Higher score for lower water level
        int timeFactor = (int)timer.Interval; // Higher score for quicker times

        int actionScore = 0;
        for (int i = 0; i < _actions.Count; i++)
        {
            // Assume that the actions are sorted by rank, with the highest-ranked action first
            // Give more points for higher-ranked actions
            actionScore += (12 - i) * _actions[i].Score;
        }

        // Calculate the initial score
        int initialScore = waterLevelFactor + timeFactor + actionScore;

        // Calculate the percentage of the initial score
        int minScore = 10800;
        int maxScore = 13200;
        double percentage = ((initialScore - minScore) * 100.0) / (maxScore - minScore);

        // Ensure the percentage is within 0 to 100
        percentage = Math.Max(0, Math.Min(100, percentage));

        // Add the percentage to the existing score
        _player.Score += (int)percentage;


        // Score cap at 100
        if (_player.Score > 100)
        {
            _player.Score = 100;
        }
    }


}


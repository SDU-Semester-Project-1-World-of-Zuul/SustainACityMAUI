using System.Diagnostics;
using SustainACityMAUI.Models;
using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views
{
    public partial class CrisisManagementMinigamePage : ContentPage
    {
        private Stopwatch _stopwatch;
        private bool _gameStarted = false;

        public CrisisManagementMinigamePage(Player player)
        {
            InitializeComponent();
            BindingContext = new CrisisManagementMinigame(player);
            _stopwatch = new Stopwatch();
            StartGameButton.Clicked += OnStartGameButtonClicked;
        }

        private void OnStartGameButtonClicked(object sender, EventArgs e)
        {
            if (!_gameStarted)
            {
                _gameStarted = true;
                _stopwatch.Start();

                this.Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    // Update the UI on the main thread
                    Dispatcher.Dispatch(() => TimerLabel.Text = $"Time: {(int)_stopwatch.Elapsed.TotalSeconds} seconds");

                    return _gameStarted; // Continue the timer if the game is still going
                });

                var game = (CrisisManagementMinigame)BindingContext;
                game.StartGame();
            }
        }
    }
}
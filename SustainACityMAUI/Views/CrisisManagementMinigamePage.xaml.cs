using System.Diagnostics;
using SustainACityMAUI.Models;
using SustainACityMAUI.ViewModels;

namespace SustainACityMAUI.Views
{
    public partial class CrisisManagementMinigamePage : ContentPage
    {
        private Stopwatch _stopwatch;
        private bool _gameStarted = false;

        [Obsolete]
        public CrisisManagementMinigamePage(Player player)
        {
            InitializeComponent();
            BindingContext = new CrisisManagementMinigame(player);
            _stopwatch = new Stopwatch();
            StartGameButton.Clicked += OnStartGameButtonClicked;

            // Navigation
            MessagingCenter.Subscribe<CrisisManagementMinigame>(this, "GoBack", (sender) =>
   {
       Navigation.PopAsync();

   });
        }

        [Obsolete]
        private void OnStartGameButtonClicked(object sender, EventArgs e)
        {
            if (!_gameStarted)
            {
                _gameStarted = true;
                _stopwatch.Start();
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    TimerLabel.Text = $"Time: {(int)_stopwatch.Elapsed.TotalSeconds} seconds";
                    return true; // keeps the timer running
                });
                var game = (CrisisManagementMinigame)BindingContext;
                game.StartGame();
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<CrisisManagementMinigame>(this, "GoBack");
        }

    }
}
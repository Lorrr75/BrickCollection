using BrickCollection.Models;
using BrickCollection.Services;

namespace BrickCollection.Views
{
    public partial class SetDetailPage : ContentPage
    {
        private readonly BricksetSet _set;
        private readonly CacheService _cacheService = new();
        private bool _isInitializing = true;

        public SetDetailPage(BricksetSet set)
        {
            InitializeComponent();
            _set = set;
            BindingContext = set;
            WantedSwitch.IsToggled = set.Collection?.Wanted ?? false;
            _isInitializing = false;
        }

        private async void OnWantedToggled(object sender, ToggledEventArgs e)
        {
            if (_isInitializing)
                return;

            WriteStatusLabel.Text = "Aggiornamento in corso...";

            var authService = new BricksetAuthService(new HttpClient());
            var loginResult = await authService.LoginAsync(Secrets.BricksetApiKey,
                                                           Secrets.BricksetUsername,
                                                           Secrets.BricksetPassword);

            if (!loginResult.Success)
            {
                WriteStatusLabel.Text = $"Errore login: {loginResult.ErrorMessage}";
                return;
            }

            var writeResult = await authService.SetWantedAsync(Secrets.BricksetApiKey,
                                                               loginResult.UserHash!,
                                                               _set.SetID, e.Value);

            if (writeResult.Success)
            {
                _set.Collection ??= new BricksetCollectionInfo();
                _set.Collection.Wanted = e.Value;
                await _cacheService.UpdateWantedAsync(_set.SetID, e.Value);
            }

            WriteStatusLabel.Text = writeResult.Success
            ? (e.Value ? "Aggiunto alla lista desideri su Brickset" : "Rimosso dalla lista desideri su Brickset")
            : $"Errore: {writeResult.ErrorMessage}";
        }
    }
}

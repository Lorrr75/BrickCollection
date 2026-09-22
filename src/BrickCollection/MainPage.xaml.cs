using BrickCollection.Models;
using BrickCollection.Services;
using BrickCollection.Views;


namespace BrickCollection
{
	public partial class MainPage : ContentPage
	{
		private readonly CacheService _cacheService = new CacheService();
        public MainPage()
		{
			InitializeComponent();
			InitializeAsync();
		}

		private async void InitializeAsync()		
		{
			var cachedSets = await _cacheService.GetCachedSetsAsync();

			if (cachedSets.Count > 0)
            {
                DisplaySets(cachedSets);
				StatusLabel.Text = $"{cachedSets.Count} set (da cache locale)";
            }
            else
            {
				// Prima esecuzione: nessuna cache, scarica dall'API
                await RefreshFromApiAsync();
            }
        }

		private async void OnRefreshClicked(object sender, EventArgs e)
		{
			await RefreshFromApiAsync();
        }

		private async Task RefreshFromApiAsync()
		{
			StatusLabel.Text = "Aggiornamento in corso...";

			var authService = new BricksetAuthService(new HttpClient());
			var loginResult = await authService.LoginAsync(
			Secrets.BricksetApiKey,
			Secrets.BricksetUsername,
			Secrets.BricksetPassword);

			if (!loginResult.Success)
			{
				StatusLabel.Text = $"Errore login: {loginResult.ErrorMessage}";
				return;
			}

			var setsResult = await authService.GetOwnedSetsAsync(Secrets.BricksetApiKey, loginResult.UserHash!);

			if (setsResult.Success)
			{
				await _cacheService.SaveSetsAsync(setsResult.Sets);
				DisplaySets(setsResult.Sets);
				StatusLabel.Text = $"{setsResult.Sets.Count} set (aggiornato ora, totale API: {setsResult.TotalMatches})";
            }
			else
			{
				StatusLabel.Text = $"Errore fetch: {setsResult.ErrorMessage}";
			}
		}

		private void DisplaySets(List<BricksetSet> sets)
		{
			SetsCollectionView.ItemsSource = sets;

			int totalPieces = sets.Sum(s => s.Pieces ?? 0);
			int totalMinifigs = sets.Sum(s => s.Minifigs ?? 0);
            StatsLabel.Text = $"Totale pezzi: {totalPieces:N0}  ·  Totale minifigure: {totalMinifigs:N0}";
        }

		private async void OnSetSelected(object sender, SelectionChangedEventArgs e)
		{
			if (e.CurrentSelection.FirstOrDefault() is BricksetSet selectedSet)
            {
                await Navigation.PushAsync(new SetDetailPage(selectedSet));
                SetsCollectionView.SelectedItem = null; // Deseleziona l'elemento dopo la navigazione
            }
		}
    }

}
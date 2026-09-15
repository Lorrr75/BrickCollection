using BrickCollection.Models;
using BrickCollection.Services;
namespace BrickCollection;

public partial class MainPage : ContentPage
{
	
	public MainPage()
	{
		InitializeComponent();
        LoadCollectionAsync();
	}

	private async void LoadCollectionAsync()
	{
		var authService = new BricksetAuthService(new HttpClient());
		var loginResult = await authService.LoginAsync(Secrets.BricksetApiKey,
													   Secrets.BricksetUsername,
													   Secrets.BricksetPassword);
		if (!loginResult.Success)
		{
			System.Diagnostics.Debug.WriteLine($"[BrickCollection] Login FALLITO: {loginResult.ErrorMessage}");
			return;
		}

		var setsResult = await authService.GetOwnedSetsAsync(Secrets.BricksetApiKey, loginResult.UserHash);

		if (setsResult.Success)
		{
			SetsCollectionView.ItemsSource = setsResult.Sets;
			StatusLabel.Text = $"{setsResult.Sets.Count} set caricati (totale API: {setsResult.TotalMatches})";
		}
		else
		{
			System.Diagnostics.Debug.WriteLine($"[BrickCollection] Errore nel recupero dei set: {setsResult.ErrorMessage}");
		}
	}
}


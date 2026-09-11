using BrickCollection.Services;
//using BrickCollection.Models;
namespace BrickCollection;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
////		InitializeComponent();
		TestBricksetLogin();
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
//		count++;

//		if (count == 1)
//			CounterBtn.Text = $"Clicked {count} time";
//		else
//			CounterBtn.Text = $"Clicked {count} times";

//		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async void TestBricksetLogin()
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

        System.Diagnostics.Debug.WriteLine($"[BrickCollection] Login OK. Hash: {loginResult.UserHash}");

		var setsResults = await authService.GetOwnedSetsAsync(Secrets.BricksetApiKey, loginResult.UserHash!);
    
		if (setsResults.Success)
		{
            System.Diagnostics.Debug.WriteLine($"[BrickCollection] Trovati {setsResults.TotalMatches} set posseduti.");
			foreach (var set in setsResults.Sets.Take(5))
			{
				System.Diagnostics.Debug.WriteLine($"  - {set.Number} {set.Name} ({set.Year}), {set.Pieces} pezzi");
            }
        }
		else
		{
            System.Diagnostics.Debug.WriteLine($"[BrickCollection] Fetch FALLITO: {setsResults.ErrorMessage}");
        }
    }

}
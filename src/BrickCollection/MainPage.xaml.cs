using BrickCollection.Services;

namespace BrickCollection;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		TestBricksetLogin();
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async void TestBricksetLogin()
	{
		var authService = new BricksetAuthService(new HttpClient());
		var result = await authService.LoginAsync(Secrets.BricksetApiKey, Secrets.BricksetUsername, Secrets.BricksetPassword);
		if (result.Success)
		{
			System.Diagnostics.Debug.WriteLine($"[BrickCollection] Login OK. Hash: {result.UserHash}");
        }
		else
		{
			System.Diagnostics.Debug.WriteLine($"[BrickCollection] Login FALLITO: {result.ErrorMessage}");
		}
    }

}
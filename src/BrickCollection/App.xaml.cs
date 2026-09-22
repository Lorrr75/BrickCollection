using Microsoft.Extensions.DependencyInjection;

namespace BrickCollection;

public partial class App : Application
{
	public App()
	{
		SQLitePCL.Batteries_V2.Init();
        InitializeComponent();
    }

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}
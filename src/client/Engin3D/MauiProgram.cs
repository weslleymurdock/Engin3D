using Mopups.Hosting;
using UraniumUI.Icons.MaterialSymbols;
using InputKit.Shared.Controls;
using UraniumUI;
using Engin3D.Composition.Extensions;
using CommunityToolkit.Maui;
using Engin3D.Presentation.Main.ViewModels;
using Engin3D.Presentation.Main.Views;

namespace Engin3D;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{ 
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureMopups()
			.UseUraniumUI()
			.UseUraniumUIMaterial()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

				fonts.AddMaterialSymbolsFonts();

            })
			.AddEngin3D();

		builder.Services.AddMopupsDialogs();

		builder.Services.AddSingletonWithShellRoute<MainPage, MainPageViewModel>();
		return builder.Build();
	}
}

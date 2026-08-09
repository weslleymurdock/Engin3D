using Mopups.Hosting;
using UraniumUI.Icons.MaterialSymbols;
using InputKit.Shared.Controls;
using UraniumUI;

namespace Engin3D;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureMopups()
			.UseUraniumUI()
			.UseUraniumUIMaterial()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

				fonts.AddMaterialSymbolsFonts();

            });

		builder.Services.AddMopupsDialogs();
		return builder.Build();
	}
}

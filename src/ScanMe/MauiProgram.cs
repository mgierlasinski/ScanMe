using ScanMe.Services;
using ScanMe.ViewModels;
using ScanMe.Views;

namespace ScanMe;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        builder.Services.AddTransient<QuickParsePage>();
        builder.Services.AddTransient<QuickParseViewModel>();
        builder.Services.AddTransient<HistoryPage>();
        builder.Services.AddTransient<HistoryViewModel>();

        builder.Services.AddSingleton<IDatabaseProvider, DatabaseProvider>();
        builder.Services.AddSingleton<IBarcodeService, BarcodeService>();

        return builder.Build();
	}
}

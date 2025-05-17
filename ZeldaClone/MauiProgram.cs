using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Plugins.SQLite;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace ZeldaClone;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseSkiaSharp()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		#region dependency injection
		builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
        #endregion
        return builder.Build();
	}
}

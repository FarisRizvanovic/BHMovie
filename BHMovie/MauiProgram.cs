using BHMovie.Service;
using BHMovie.View;
using BHMovie.ViewModel;
using Microsoft.Extensions.Logging;

namespace BHMovie;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddTransient<MovieDetailsPage>();
        builder.Services.AddTransient<MovieDetailsViewModel>();

        builder.Services.AddSingleton<MovieService>();


        return builder.Build();
    }
}


using BHMovie.Service;
using BHMovie.View;
using BHMovie.View.Templates;
using BHMovie.ViewModel;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;

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

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
        {
            //Remove enrty underline for android
            h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddTransient<MovieDetailsPage>();
        builder.Services.AddTransient<MovieDetailsViewModel>();

        builder.Services.AddTransient<CastItemTemplate>();
        builder.Services.AddTransient<CategoryItemTemplate>();
        builder.Services.AddTransient<MovieItemTemplate>();

        builder.Services.AddSingleton<FavouritesPage>();
        builder.Services.AddSingleton<FavouriteMoviesViewModel>();

        builder.Services.AddSingleton<MovieService>();
        builder.Services.AddSingleton<DatabaseService>();

        return builder.Build();
    }
}


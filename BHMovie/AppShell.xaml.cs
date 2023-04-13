using BHMovie.View;

namespace BHMovie;

public partial class AppShell : Shell
{

    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MovieDetailsPage), typeof(MovieDetailsPage));
        Routing.RegisterRoute(nameof(FavouritesPage), typeof(FavouritesPage));
    }
}


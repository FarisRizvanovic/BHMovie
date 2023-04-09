namespace BHMovie.View;

public partial class FavouriteMoviesPage : ContentPage
{
    public FavouriteMoviesPage()
    {
        InitializeComponent();
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}

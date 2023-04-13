using BHMovie.ViewModel;

namespace BHMovie.View;

public partial class FavouritesPage : ContentPage
{
    private readonly FavouriteMoviesViewModel _vm;

    public FavouritesPage(FavouriteMoviesViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.Refresh();
    }

    async void RefreshButton_Clicked(System.Object sender, System.EventArgs e)
    {
        await _vm.Refresh();
    }
}

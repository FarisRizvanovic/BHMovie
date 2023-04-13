using BHMovie.ViewModel;

namespace BHMovie.View;

public partial class MovieDetailsPage : ContentPage
{
    private readonly MovieDetailsViewModel _vm;

    public MovieDetailsPage(MovieDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadDataAsync();
        await _vm.CheckIfMovieIsInDatabase();
        if (_vm.IsMovieFavourited)
            FavouriteButtonLabel.Text = "-";
    }

}

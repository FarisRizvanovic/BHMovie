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
            FavouriteButtonImage.Source = "ic_full_heart.png";


        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += async (s, e) =>
        {
            await FavouriteButton_Clicked();
        };
        FavouriteButton.GestureRecognizers.Add(tapGestureRecognizer);
    }

    async Task FavouriteButton_Clicked()
    {
        if (_vm.IsMovieFavourited)
        {
            FavouriteButtonImage.Source = "ic_empty_heart.png";
            await _vm.RemoveMovieFromFavourites();
            await _vm.CheckIfMovieIsInDatabase();
        }
        else
        {
            FavouriteButtonImage.Source = "ic_full_heart.png";
            await _vm.AddMovieToFavourites();
            await _vm.CheckIfMovieIsInDatabase();
        }
    }
}

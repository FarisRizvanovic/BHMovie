using System;
using BHMovie.Model;
using BHMovie.Other;
using BHMovie.Service;
using BHMovie.View;
using CommunityToolkit.Mvvm.Input;

namespace BHMovie.ViewModel
{
    public partial class FavouriteMoviesViewModel : BaseViewModel
    {
        IDatabaseService databaseService;

        public ObservableCollection<Movie> Movies { get; } = new();

        public FavouriteMoviesViewModel(DatabaseService databaseService)
        {
            this.databaseService = databaseService;

        }

        [RelayCommand]
        async Task GoToDetails(Movie movie)
        {
            if (movie == null)
                return;

            await Shell.Current.GoToAsync(nameof(MovieDetailsPage), true, new Dictionary<string, object>
            {
                {"Movie", movie }
            });
        }

        [RelayCommand]
        public async Task RemoveMovieFromFavourites(Movie movie)
        {
            await databaseService.RemoveMovie(movie.id);
            await Refresh();
        }

        [RelayCommand]
        public async Task Refresh()
        {
            IsBusy = true;

            if (Movies.Count() > 0)
                Movies.Clear();

            var newMovies = await databaseService.GetMovies();

            if (newMovies.Count() > 0)
                foreach (var movie in newMovies)
                {
                    Movies.Add(movie);
                }
        }
    }
}


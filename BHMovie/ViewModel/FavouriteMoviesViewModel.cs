using System;
using BHMovie.Model;
using BHMovie.Other;
using BHMovie.Service;
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


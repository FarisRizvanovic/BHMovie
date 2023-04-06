using System;
using Android.Util;
using BHMovie.Model;
using BHMovie.Service;
using BHMovie.View;
using CommunityToolkit.Mvvm.Input;

namespace BHMovie.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        MovieService movieService;

        public ObservableCollection<Movie> Movies { get; } = new();
        public ObservableCollection<string> GenresAsStrings { get; } = new();
        public ObservableCollection<Genre> Genres { get; } = new();

        [ObservableProperty]
        string selectedGenre;

        public MainViewModel(MovieService movieService)
        {
            this.movieService = movieService;
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
        public async Task LoadDataAsync()
        {
            await GetMoviesAsync();
            await GetMovieGenresAsync();
        }

        [RelayCommand]
        async Task GetMoviesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var movieResponse = await movieService.GetMovies();
                if (Movies.Count != 0)
                {
                    Movies.Clear();
                }

                foreach (var movie in movieResponse.results)
                {
                    movie.poster_path = "https://image.tmdb.org/t/p/w500" + movie.poster_path;
                    Movies.Add(movie);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to  get movies: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task GetMovieGenresAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var genreResponse = await movieService.GetMovieGenres();
                if (Genres.Count != 0)
                {
                    Genres.Clear();
                }

                Genre basicGenre = new();
                basicGenre.name = "All Genres";

                GenresAsStrings.Add(basicGenre.name);
                Genres.Add(basicGenre);

                foreach (var genre in genreResponse.genres)
                {
                    Genres.Add(genre);
                    GenresAsStrings.Add(genre.name);
                }
                SelectedGenre = basicGenre.name;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to  get movies: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}


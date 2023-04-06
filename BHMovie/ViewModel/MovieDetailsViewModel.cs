using System;
using BHMovie.Model;
using BHMovie.Service;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace BHMovie.ViewModel
{
    [QueryProperty(nameof(Movie), "Movie")]
    public partial class MovieDetailsViewModel : BaseViewModel
    {
        MovieService movieService;

        public MovieDetailsViewModel(MovieService movieService)
        {
            this.movieService = movieService;
        }

        [ObservableProperty]
        Movie movie;

        [ObservableProperty]
        MovieDetailsResponse movieDetailsResponse;

        public ObservableCollection<Cast> Cast { get; } = new();
        public ObservableCollection<Genre> Genres { get; } = new();

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }


        [RelayCommand]
        public async Task LoadDataAsync()
        {
            await GetMovieDetailsResponseAsync();

        }

        [RelayCommand]
        async Task GetMovieDetailsResponseAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var movieResponse = await movieService.GetMovieDetailsResponse(Movie.id);
                MovieDetailsResponse = movieResponse;

                if (Cast.Count > 0)
                    return;
                foreach (var cast in movieResponse.credits.cast)
                {
                    cast.profile_path = "https://image.tmdb.org/t/p/w500" + cast.profile_path;
                    Cast.Add(cast);
                }

                if (Genres.Count > 0)
                    return;
                foreach (var genre in movieResponse.genres)
                {
                    Genres.Add(genre);
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
    }
}


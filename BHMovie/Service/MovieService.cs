using System;
using System.Net.Http.Json;
using BHMovie.Model;

namespace BHMovie.Service
{
    public class MovieService
    {
        HttpClient httpClient;

        public MovieService()
        {
            httpClient = new HttpClient();
        }

        MovieResponse movieResponse = new();
        GenreResponse genreResponse = new();
        MovieDetailsResponse movieDetailsResponse = new();


        public async Task<MovieResponse> GetMovies()
        {
            if (movieResponse.results.Count > 0)
                return movieResponse;

            var url = "https://api.themoviedb.org/3/discover/movie?with_original_language=bs&api_key=ceb3c376b1b48087d525af510ae3c248";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                movieResponse = await response.Content.ReadFromJsonAsync<MovieResponse>();
            }

            return movieResponse;

        }

        public async Task<GenreResponse> GetMovieGenres()
        {
            if (genreResponse.genres.Count > 0)
                return genreResponse;

            var url = "https://api.themoviedb.org/3/genre/movie/list?api_key=ceb3c376b1b48087d525af510ae3c248";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                genreResponse = await response.Content.ReadFromJsonAsync<GenreResponse>();

            }
            return genreResponse;
        }

        public async Task<MovieDetailsResponse> GetMovieDetailsResponse(int movieId)
        {
            if (movieDetailsResponse.id != -1)
                return movieDetailsResponse;

            var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key=ceb3c376b1b48087d525af510ae3c248&language=en-US&append_to_response=credits";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                movieDetailsResponse = await response.Content.ReadFromJsonAsync<MovieDetailsResponse>();

            }
            return movieDetailsResponse;
        }

    }
}


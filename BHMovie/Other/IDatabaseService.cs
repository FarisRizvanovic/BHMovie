using System;
using BHMovie.Model;

namespace BHMovie.Other
{
    public interface IDatabaseService
    {
        Task AddMovie(Movie movie);
        Task<IEnumerable<Movie>> GetMovies();
        //Task<Movie> GetMovie(int id);
        Task RemoveMovie(int id);
        Task<Boolean> CheckIfMovieIsFavourited(Movie movie);
    }
}


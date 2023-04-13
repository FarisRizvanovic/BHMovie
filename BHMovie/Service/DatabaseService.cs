using System;
using Android.Hardware.Camera2;
using BHMovie.Model;
using BHMovie.Other;
using SQLite;

namespace BHMovie.Service
{

    public class DatabaseService : IDatabaseService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "BHMovie.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Movie>();
        }

        public async Task AddMovie(Movie movie)
        {
            await Init();
            var id = await db.InsertOrReplaceAsync(movie);
        }

        public async Task RemoveMovie(int id)
        {
            await Init();
            await db.DeleteAsync<Movie>(id);
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            await Init();
            var movies = await db.Table<Movie>().ToListAsync();
            return movies;
        }

        public async Task<Boolean> CheckIfMovieIsFavourited(Movie movie)
        {
            await Init();

            var isInDatabase = await db.FindAsync<Movie>(movie.id);

            return isInDatabase != null;
        }
    }
}


using System;
namespace BHMovie.Model
{
    public class MovieResponse
    {
        public int page { get; set; }
        public List<Movie> results { get; set; } = new List<Movie>();
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}


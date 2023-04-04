using System;
namespace BHMovie.Model
{
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}


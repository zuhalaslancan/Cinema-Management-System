using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE_Cinema
{
    class Director : Person
    {
        private List<Movie> directedMovies;

        public List<Movie> DirectedMovies
        {
            get { return directedMovies; }
            set { directedMovies = value; }
        }

        public Director(string name, string birthDay, string birthPlace) : base(name, birthDay, birthPlace)
        {
            this.directedMovies = new List<Movie>();
        }

        public override string getClass()
        {
            return "Director";
        }
        
        public override string ToString()
        {
            string movies = "[ ";

            foreach (Movie movie in this.directedMovies)
            {
                movies += movie.Id.ToString() + " ";
            }

            movies += "]";

            return base.ToString() + "Movies: " + movies + "\n";
        }
    }
}

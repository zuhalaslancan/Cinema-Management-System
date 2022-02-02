using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE_Cinema
{
    class Star : Person
    {

        private List<Movie> playedMovies;
        private bool isAvailable;

        public List<Movie> PlayedMovies
        {
            get { return playedMovies; }
            set { playedMovies = value; }
        }
        public bool IsAvailable
        {   
            get { return isAvailable; }
            set { isAvailable = value; }
        }

        public Star(string name, string birthDay, string birthPlace) : base(name,birthDay,birthPlace)
        {
            this.playedMovies = new List<Movie>();
            this.isAvailable = true;
        }

        public override string getClass()
        {
            return "Star";
        }

        public override string ToString()
        {
            string movies = "[ ";

            foreach(Movie movie in this.playedMovies)
            {
                movies += movie.Id.ToString() + " ";
            }

            movies += "]";

            return base.ToString()+"Movies: "+movies+"\nIs Available: "+this.isAvailable+"\n";
        }
    }
}

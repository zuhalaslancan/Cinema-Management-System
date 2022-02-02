using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE_Cinema
{
    class Movie
    {
        private static int s_ID = 1;

        private int ID;
        private string name;
        private string genre;
        private string duration;
        private List<Person> stars;
        private Person director;
        private int revenue;

        public int Id
        {
            get { return ID; }
            set { ID = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }
        public string Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        public List<Person> Stars
        {
            get { return stars; }
            set { stars = value; }
        }
        public Person Director
        {
            get { return director; }
            set { director = value; }
        }
        public int Revenue
        {
            get { return revenue; }
            set { revenue = value; }
        }

        public Movie(string name, string genre, string duration, List<Person> stars, Person director)
        {
            this.name = name;
            this.genre = genre;
            this.duration = duration;
            this.stars = stars;
            this.director = director;
            this.revenue = 0;
            this.ID = s_ID;
            s_ID++;
        }

        public void addStar(Star s)
        {
            stars.Add(s);
        }

        public override string ToString()
        {
            string stars = "[ ";

            foreach (Star star in this.stars)
            {
                stars += star.Id.ToString() + " ";
            }

            stars += "]";

            return "ID:"+this.ID+"\nName: "+this.name+"\nGenre: "+this.genre+"\nDuration: "+this.duration +"\nDirector:"+this.director.Id+ 
                   "\nStars: " + stars + "\nRevenue: "+this.revenue+ "\n";
        }


    }
}

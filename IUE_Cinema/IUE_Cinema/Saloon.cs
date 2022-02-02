using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE_Cinema
{
    public enum Time { MORNING, NOON, EVENING }
    class Saloon
    {
        private static int s_ID = 1;
        private readonly int DEFAULT_TICKET_PRICE = 10;

        private string ID;
        private Movie movie;
        private Dictionary<Time, Showtime> showtimes;

        public string Id
        {
            get { return ID; }
            set { ID = value; }
        }
        public Movie Movie
        {
            get { return movie; }
            set { movie = value; }
        }
        public Dictionary<Time, Showtime> Showtimes
        {
            get { return showtimes; }
            set { showtimes = value; }
        }
        
        public Saloon()
        {
            this.movie = null;
            this.showtimes = new Dictionary<Time, Showtime>();
            showtimes[Time.MORNING] = new Showtime(DEFAULT_TICKET_PRICE, Time.MORNING);
            showtimes[Time.NOON] = new Showtime(DEFAULT_TICKET_PRICE, Time.NOON);
            showtimes[Time.EVENING] = new Showtime(DEFAULT_TICKET_PRICE, Time.EVENING);

            this.ID = "s"+ s_ID.ToString();
            s_ID++;
        }

        public double getSaloonRevenue()
        {
            double result = 0;

            result = showtimes[Time.MORNING].getShowtimeRevenue() + showtimes[Time.NOON].getShowtimeRevenue() + showtimes[Time.EVENING].getShowtimeRevenue();

            return result;
        }

        public override string ToString()
        {
            return "SaloonID: " + this.ID + "\nMovieID: " + this.movie.Name+"\n";
        }
    }
}

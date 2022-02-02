using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE_Cinema
{
    class Cinema
    {
        private string name;
        private List<Saloon> saloons;
        private List<Person> people;
        private List<Movie> movies;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public List<Saloon> Saloons
        {
            get { return saloons; }
            set { saloons = value; }
        }
        public List<Person> People
        {
            get { return people; }
            set { people = value; }
        }
        public List<Movie> Movies
        {
            get { return movies; }
            set { movies = value; }
        }

        public Cinema(string name)
        {
            this.name = name;
            this.saloons = new List<Saloon>();
            this.people = new List<Person>();
            this.movies = new List<Movie>();

            saloons.Add(new Saloon());
            saloons.Add(new Saloon());
            saloons.Add(new Saloon());
        }

        public int getPersonIndex(int personID, string type)
        {
            int result = -1;

            for(int i = 0; i < this.people.Count; i++)
            {
                if(this.people[i].Id == personID && this.people[i].GetType().Name.Equals(type))
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        public int getMovieIndex(int movieID)
        {
            int result = -1;

            for (int i = 0; i < this.movies.Count; i++)
            {
                if (this.movies[i].Id == movieID)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        public int getSaloonIndex(string saloonID)
        {
            int result = -1;

            for (int i = 0; i < this.saloons.Count; i++)
            {
                if (this.saloons[i].Id == saloonID)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        
        public bool controlParameters(string input)
        {
            bool result = true;

            string[] command = input.Split(";");

            if (command[0].Equals("load"))
            {
                if(command.Length != 2)
                {
                    throw new InvalidParametersException();
                }
                else if (!File.Exists(command[1]))
                {
                    throw new FileNotFoundException();
                }
            }
            else if (command[0].Equals("addStar"))
            {
                if (command.Length != 4)
                {
                    throw new InvalidParametersException();
                }
            }
            else if (command[0].Equals("addDirector"))
            {
                if (command.Length != 4)
                {
                    throw new InvalidParametersException();
                }
            }
            else if (command[0].Equals("addMovie"))
            {
                if (command.Length < 6)
                {
                    throw new InvalidParametersException();
                }

                if (getPersonIndex(Convert.ToInt32(command[4]), "Director") == -1)
                {
                    throw new InvalidParametersException();
                }

                for (int i = 5; i < command.Length; i++)
                {
                    if (getPersonIndex(Convert.ToInt32(command[i]),"Star") == -1)
                    {
                        throw new InvalidParametersException();
                    }
                }
            }
            else if (command[0].Equals("updateSaloon"))
            {
                if(command.Length != 3)
                {
                    throw new InvalidParametersException();
                }
                bool isSaloonExist = false;

                for (int i = 0; i < saloons.Count; i++)
                {
                    if (saloons[i].Id.Equals(command[1]))
                    {
                        isSaloonExist = true;
                        break;
                    }
                }

                if(!isSaloonExist)
                    throw new InvalidParametersException();

                if(getMovieIndex(Convert.ToInt32(command[2])) == -1)
                    throw new InvalidParametersException();

            }
            else if (command[0].Equals("updateShowtime"))
            {
                if (command.Length != 4)
                {
                    throw new InvalidParametersException();
                }
                bool isSaloonExist = false;

                for (int i = 0; i < saloons.Count; i++)
                {
                    if (saloons[i].Id.Equals(command[1]))
                    {
                        isSaloonExist = true;
                        break;
                    }
                }

                if (!isSaloonExist)
                    throw new InvalidParametersException();

            }
            else if (command[0].Equals("sellTicket"))
            {
                if (command.Length != 5)
                {
                    throw new InvalidParametersException();
                }
                bool isSaloonExist = false;

                for (int i = 0; i < saloons.Count; i++)
                {
                    if (saloons[i].Id.Equals(command[1]))
                    {
                        isSaloonExist = true;
                        break;
                    }
                }

                if (!isSaloonExist)
                    throw new InvalidParametersException();

                if (!command[2].Equals("morning") && !command[2].Equals("noon") && !command[2].Equals("evening"))
                    throw new InvalidParametersException();

                if (!command[4].Equals("student") && !command[4].Equals("regular"))
                    throw new InvalidParametersException();


            }
            else if (command[0].Equals("cancelTicket"))
            {
                if (command.Length != 4)
                {
                    throw new InvalidParametersException();
                }
                bool isSaloonExist = false;

                for (int i = 0; i < saloons.Count; i++)
                {
                    if (saloons[i].Id.Equals(command[1]))
                    {
                        isSaloonExist = true;
                        break;
                    }
                }

                if (!isSaloonExist)
                    throw new InvalidParametersException();

                if (!command[2].Equals("morning") && !command[2].Equals("noon") && !command[2].Equals("evening"))
                    throw new InvalidParametersException();
            }
            else if (command[0].Equals("displaySeats"))
            {
                if (command.Length != 3)
                {
                    throw new InvalidParametersException();
                }
                bool isSaloonExist = false;

                for (int i = 0; i < saloons.Count; i++)
                {
                    if (saloons[i].Id.Equals(command[1]))
                    {
                        isSaloonExist = true;
                        break;
                    }
                }

                if (!isSaloonExist)
                    throw new InvalidParametersException();

                if (!command[2].Equals("morning") && !command[2].Equals("noon") && !command[2].Equals("evening"))
                    throw new InvalidParametersException();
            }

            return result;
        }
        
        public void start()
        {
            Console.WriteLine("One day of " + this.name + " has started. Please enter your commands or type 'help' to learn commands.");
            string input = "";
            string[] command = null;


            while (!input.Equals("endTheDay"))
            {
                try
                {
                    Console.Write("Command: ");
                    input = Console.ReadLine();
                    command = input.Split(";");

                    if (input.Equals("help"))
                    {
                        string help_commands = "load;input.txt\n" +
                            "addStar;name;bdate;bplace\n" +
                            "addDirector;name;bdate;bplace\n" +
                            "addMovie;title;genre;duration;directorId;actorId1;actorId2 (...)\n" +
                            "updateSaloon;saloonId;movieId\n" +
                            "updateShowtime;saloonId;showtime;price\n" +
                            "sellTicket;saloonId;showtime;seat;personType\n" +
                            "cancelTicket;saloonId;showtime;seat\n" +
                            "displayActors\n" +
                            "displayDirectors" +
                            "displayMovies\n" +
                            "displaySeats;saloonId;showtime\n" +
                            "endTheDay\n" +
                            "help\n";
                        Console.WriteLine("---------------Command List:---------------\n" + help_commands);
                    }
                    else if (input.Equals("endTheDay"))
                    {
                        endTheDay();
                    }
                    else if (command[0].Equals("load"))
                    {
                        controlParameters(input);
                        load(command[1]);
                    }
                    else if (command[0].Equals("addStar"))
                    {
                        controlParameters(input);
                        addStar(command[1], command[2], command[3]);
                    }
                    else if (command[0].Equals("addDirector"))
                    {
                        controlParameters(input);
                        addDirector(command[1], command[2], command[3]);
                    }
                    else if (command[0].Equals("addMovie"))
                    {
                        controlParameters(input);

                        List <Person> tmp_stars = new List<Person>();

                        for(int i = 5; i < command.Length; i++)
                        {
                            tmp_stars.Add(this.people[getPersonIndex(Convert.ToInt32(command[i]),"Star")]);
                        }

                        addMovie(command[1], command[2], command[3], tmp_stars, this.people[getPersonIndex(Convert.ToInt32(command[4]),"Director")]);
                    }
                    else if (command[0].Equals("updateSaloon"))
                    {
                        controlParameters(input);
                        updateSaloon(getSaloonIndex(command[1]),getMovieIndex(Convert.ToInt32(command[2])));
                    }
                    else if (command[0].Equals("updateShowtime"))
                    {
                        controlParameters(input);

                        Time t = Time.MORNING;

                        if (command[2].Equals("morning"))
                            t = Time.MORNING;
                        else if (command[2].Equals("noon"))
                            t = Time.NOON;
                        else if (command[2].Equals("evening"))
                            t = Time.EVENING;

                        updateShowtime(getSaloonIndex(command[1]),t, Convert.ToDouble(command[3]));
                    }
                    else if (command[0].Equals("sellTicket"))
                    {
                        controlParameters(input);

                        Time t = Time.MORNING;

                        if (command[2].Equals("morning"))
                            t = Time.MORNING;
                        else if (command[2].Equals("noon"))
                            t = Time.NOON;
                        else if (command[2].Equals("evening"))
                            t = Time.EVENING;

                        Type ty = Type.ADULT;

                        if (command[4].Equals("regular"))
                            ty = Type.ADULT;
                        else if (command[4].Equals("student"))
                            ty = Type.STUDENT;

                        sellTicket(getSaloonIndex(command[1]), t, command[3], ty);
                    }
                    else if (command[0].Equals("cancelTicket"))
                    {
                        controlParameters(input);

                        Time t = Time.MORNING;

                        if (command[2].Equals("morning"))
                            t = Time.MORNING;
                        else if (command[2].Equals("noon"))
                            t = Time.NOON;
                        else if (command[2].Equals("evening"))
                            t = Time.EVENING;

                        cancelTicket(getSaloonIndex(command[1]), t, command[3]);
                    }
                    else if (command[0].Equals("displayActors"))
                    {
                        displayActors();
                    }
                    else if (command[0].Equals("displayDirectors"))
                    {
                        displayDirectors();
                    }
                    else if (command[0].Equals("displayMovies"))
                    {
                        displayMovies();
                    }
                    else if (command[0].Equals("displaySeats"))
                    {
                        controlParameters(input);

                        Time t = Time.MORNING;

                        if (command[2].Equals("morning"))
                            t = Time.MORNING;
                        else if (command[2].Equals("noon"))
                            t = Time.NOON;
                        else if (command[2].Equals("evening"))
                            t = Time.EVENING;

                        displaySeats(getSaloonIndex(command[1]), t);
                    }
                    else
                    {
                        throw new UndefinedCommandException();
                    }
                }
                catch (UndefinedCommandException)
                {
                    Console.WriteLine("EXCEPTION -> UndefinedCommand: '" + command[0] + "' is not defined.");
                }
                catch (InvalidParametersException)
                {
                    Console.WriteLine("EXCEPTION -> InvalidParameters: " + input);
                }
                catch (SeatFullException)
                {
                    Console.WriteLine("EXCEPTION -> SeatFull: " + input);
                }
                catch (SeatAlreadyEmptyException)
                {
                    Console.WriteLine("EXCEPTION -> SeatAlreadyEmpty: " + input);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("EXCEPTION -> FileNotFound: " + input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("EXCEPTION -> FormatException: " + input);
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("EXCEPTION -> InvalidCastException: " + input);
                }
            }        
        }

        public void load(string file_name)
        {
            string[] lines = File.ReadAllLines(file_name);

            string input = "";
            string[] command = null;


            foreach (string line in lines)
            {
                input = line;

                try
                {
                    command = input.Split(";");

                    if (input.Equals("help"))
                    {
                        string help_commands = "load;input.txt\n" +
                            "addStar;name;bdate;bplace\n" +
                            "addDirector;name;bdate;bplace\n" +
                            "addMovie;title;genre;duration;directorId;actorId1;actorId2 (...)\n" +
                            "updateSaloon;saloonId;movieId\n" +
                            "updateShowtime;saloonId;showtime;price\n" +
                            "sellTicket;saloonId;showtime;seat;personType\n" +
                            "cancelTicket;saloonId;showtime;seat\n" +
                            "displayActors\n" +
                            "displayDirectors" +
                            "displayMovies\n" +
                            "displaySeats;saloonId;showtime\n" +
                            "endTheDay\n" +
                            "help\n";
                        Console.WriteLine("---------------Command List:---------------\n" + help_commands);
                    }
                    else if (input.Equals("endTheDay"))
                    {
                        Console.WriteLine("Command 'endTheDay' skipped.");
                    }
                    else if (command[0].Equals("load"))
                    {
                        Console.WriteLine("Command 'load' skipped.");
                    }
                    else if (command[0].Equals("addStar"))
                    {
                        controlParameters(input);
                        addStar(command[1], command[2], command[3]);
                    }
                    else if (command[0].Equals("addDirector"))
                    {
                        controlParameters(input);
                        addDirector(command[1], command[2], command[3]);
                    }
                    else if (command[0].Equals("addMovie"))
                    {
                        controlParameters(input);

                        List<Person> tmp_stars = new List<Person>();

                        for (int i = 5; i < command.Length; i++)
                        {
                            tmp_stars.Add(this.people[getPersonIndex(Convert.ToInt32(command[i]),"Star")]);
                        }

                        addMovie(command[1], command[2], command[3], tmp_stars, this.people[getPersonIndex(Convert.ToInt32(command[4]),"Director")]);
                    }
                    else if (command[0].Equals("updateSaloon"))
                    {
                        controlParameters(input);
                        updateSaloon(getSaloonIndex(command[1]), getMovieIndex(Convert.ToInt32(command[2])));
                    }
                    else if (command[0].Equals("updateShowtime"))
                    {
                        controlParameters(input);

                        Time t = Time.MORNING;

                        if (command[2].Equals("morning"))
                            t = Time.MORNING;
                        else if (command[2].Equals("noon"))
                            t = Time.NOON;
                        else if (command[2].Equals("evening"))
                            t = Time.EVENING;

                        updateShowtime(getSaloonIndex(command[1]), t, Convert.ToDouble(command[3]));
                    }
                    else if (command[0].Equals("sellTicket"))
                    {
                        controlParameters(input);

                        Time t = Time.MORNING;

                        if (command[2].Equals("morning"))
                            t = Time.MORNING;
                        else if (command[2].Equals("noon"))
                            t = Time.NOON;
                        else if (command[2].Equals("evening"))
                            t = Time.EVENING;

                        Type ty = Type.ADULT;

                        if (command[4].Equals("regular"))
                            ty = Type.ADULT;
                        else if (command[4].Equals("student"))
                            ty = Type.STUDENT;

                        sellTicket(getSaloonIndex(command[1]), t, command[3], ty);
                    }
                    else if (command[0].Equals("cancelTicket"))
                    {
                        controlParameters(input);

                        Time t = Time.MORNING;

                        if (command[2].Equals("morning"))
                            t = Time.MORNING;
                        else if (command[2].Equals("noon"))
                            t = Time.NOON;
                        else if (command[2].Equals("evening"))
                            t = Time.EVENING;

                        cancelTicket(getSaloonIndex(command[1]), t, command[3]);
                    }
                    else if (command[0].Equals("displayActors"))
                    {
                        displayActors();
                    }
                    else if (command[0].Equals("displayDirectors"))
                    {
                        displayDirectors();
                    }
                    else if (command[0].Equals("displayMovies"))
                    {
                        displayMovies();
                    }
                    else if (command[0].Equals("displaySeats"))
                    {
                        controlParameters(input);

                        Time t = Time.MORNING;

                        if (command[2].Equals("morning"))
                            t = Time.MORNING;
                        else if (command[2].Equals("noon"))
                            t = Time.NOON;
                        else if (command[2].Equals("evening"))
                            t = Time.EVENING;

                        displaySeats(getSaloonIndex(command[1]), t);
                    }
                    else
                    {
                        throw new UndefinedCommandException();
                    }
                }
                catch (UndefinedCommandException)
                {
                    Console.WriteLine("EXCEPTION -> UndefinedCommand: '" + command[0] + "' is not defined.");
                }
                catch (InvalidParametersException)
                {
                    Console.WriteLine("EXCEPTION -> InvalidParameters: " + input);
                }
                catch (SeatFullException)
                {
                    Console.WriteLine("EXCEPTION -> SeatFull: " + input);
                }
                catch (SeatAlreadyEmptyException)
                {
                    Console.WriteLine("EXCEPTION -> SeatAlreadyEmpty: " + input);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("EXCEPTION -> FileNotFound: " + input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("EXCEPTION -> FormatException: " + input);
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("EXCEPTION -> InvalidCastException: " + input);
                }
            }
        }

        public void addStar(string name, string birthDay, string birthPlace)
        {
            this.people.Add(new Star(name, birthDay, birthPlace));
            Console.WriteLine("Star with ID=" + this.people.Last().Id + " added successfully.");
        }

        public void addDirector(string name, string birthDay, string birthPlace)
        {
            this.people.Add(new Director(name, birthDay, birthPlace));
            Console.WriteLine("Director with ID=" + this.people.Last().Id + " added successfully.");
        }

        public void addMovie(string name, string genre, string duration, List<Person> stars, Person director)
        {
            Movie m1 = new Movie(name, genre, duration, stars, director);
            this.movies.Add(m1);
            ((Director)director).DirectedMovies.Add(m1);

            for(int i = 0; i < stars.Count; i++)
            {
                ((Star)stars[i]).PlayedMovies.Add(m1);
            }

            Console.WriteLine("Movie with ID=" + this.movies.Last().Id + " added successfully.");
        }

        public void updateSaloon(int saloonIndex, int movieIndex)
        {
            this.saloons[saloonIndex].Movie = this.movies[movieIndex];
            Console.WriteLine("Saloon "+ this.saloons[saloonIndex].Id+ " updated with Movie "+ this.movies[movieIndex].Id);
        }

        public void updateShowtime(int saloonIndex, Time showtime, double price)
        {
            this.saloons[saloonIndex].Showtimes[showtime].Price = price;
            Console.WriteLine("Saloon " + this.saloons[saloonIndex].Id +" Showtime "+showtime +" updated with price " + price);
        }

        public void sellTicket(int saloonIndex,Time showtime, string seat,Type type)
        {
            this.saloons[saloonIndex].Showtimes[showtime].sellTicket(seat, type);
        }

        public void cancelTicket(int saloonIndex, Time showtime, string seat)
        {
            this.saloons[saloonIndex].Showtimes[showtime].cancelTicket(seat);
        }

        public void displayActors()
        {
            bool flag = false;

            for(int i = 0; i < this.people.Count; i++)
            {
                if (this.people[i].GetType().Name == "Star")
                {
                    Console.WriteLine(this.people[i]);
                    flag = true;
                }
            }

            if (flag)
                Console.WriteLine("Actors successfully displayed.");
            else
            {
                Console.WriteLine("No actors have been added yet.");
            }

        }

        public void displayDirectors()
        {
            bool flag = false;

            for (int i = 0; i < this.people.Count; i++)
            {
                if (this.people[i].GetType().Name == "Director")
                {
                    Console.WriteLine(this.people[i]);
                    flag = true;
                }
            }

            if(flag)
                Console.WriteLine("Directors successfully displayed.");
            else
            {
                Console.WriteLine("No directors have been added yet.");
            }
        }

        public void displayMovies()
        {
            if(this.movies.Count > 0)
            {
                for (int i = 0; i < this.movies.Count; i++)
                {
                    Console.WriteLine(this.movies[i]);
                }

                Console.WriteLine("Movies successfully displayed.");
            }
            else
            {
                Console.WriteLine("No movies have been added yet.");
            }
            
        }

        public void displaySeats(int saloonIndex, Time showtime)
        {
            Console.WriteLine(this.saloons[saloonIndex].Showtimes[showtime]);
            Console.WriteLine("Saloon "+ this.saloons[saloonIndex].Id +" Time "+ showtime+" successfully displayed.");
        }
    
        public void endTheDay()
        {
            Console.Clear();
            Console.WriteLine("Console cleared. Day has been ended. Statistics are shown:");
            displayStatistics();
        }

        public void displayStatistics()
        {
            mostWatchedMovie();
            totalRevenue();
            mostFilledShowtime();
            highestIncomeMovie();
            printCinema();
        }
    
        public void mostWatchedMovie()
        {
            
            int s1 = saloons[0].Showtimes[Time.MORNING].getPersonCount() + saloons[0].Showtimes[Time.NOON].getPersonCount() + saloons[0].Showtimes[Time.EVENING].getPersonCount();
            int s2 = saloons[1].Showtimes[Time.MORNING].getPersonCount() + saloons[1].Showtimes[Time.NOON].getPersonCount() + saloons[1].Showtimes[Time.EVENING].getPersonCount();
            int s3 = saloons[2].Showtimes[Time.MORNING].getPersonCount() + saloons[2].Showtimes[Time.NOON].getPersonCount() + saloons[2].Showtimes[Time.EVENING].getPersonCount();

            if(s1 >= s2 && s1 > s3)
            {
                Console.WriteLine("The most-watched movie : " + saloons[0].Movie.Name);
            }
            else if (s2 >= s1 && s2 > s3)
            {
                Console.WriteLine("The most-watched movie : " + saloons[1].Movie.Name);
            }
            else if (s3 >= s1 && s3 > s2)
            {
                Console.WriteLine("The most-watched movie : " + saloons[2].Movie.Name);
            }
        }
    
        public void totalRevenue()
        {
            double revenue = saloons[0].getSaloonRevenue() + saloons[1].getSaloonRevenue() + saloons[2].getSaloonRevenue();
            Console.WriteLine("Total revenue of IEU Cinema : "+revenue);

        }

        public void mostFilledShowtime()
        {
            Dictionary<string, int> mfs = new Dictionary<string, int>();

            for(int i = 0; i < 3; i++)
            {
                mfs["s"+(i+1).ToString()+"-morning"] = saloons[i].Showtimes[Time.MORNING].getPersonCount();
                mfs["s" + (i + 1).ToString() + "-noon"] = saloons[i].Showtimes[Time.NOON].getPersonCount();
                mfs["s" + (i + 1).ToString() + "-evening"] = saloons[i].Showtimes[Time.EVENING].getPersonCount();
            }

            int c = 1;

            foreach (KeyValuePair<string, int> item in mfs.OrderBy(key => key.Value))
            {
                if (c == mfs.Count)
                {
                    Console.WriteLine("The most-filled showtime : "+item.Key);
                }
                else
                {
                    c++;
                }
                
            }
        }

        public void highestIncomeMovie()
        {
            double s1 = saloons[0].getSaloonRevenue();
            double s2 = saloons[1].getSaloonRevenue();
            double s3 = saloons[2].getSaloonRevenue();

            if (s1 >= s2 && s1 > s3)
            {
                Console.WriteLine("The highest-income movie : " + saloons[0].Movie.Name+" - "+s1);
            }
            else if (s2 >= s1 && s2 > s3)
            {
                Console.WriteLine("The highest-income movie : " + saloons[1].Movie.Name + " - " + s2);
            }
            else if (s3 >= s1 && s3 > s2)
            {
                Console.WriteLine("The highest-income movie : " + saloons[2].Movie.Name + " - " + s3);
            }
        }
    
        public void printCinema()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("s" + (i + 1).ToString()+ " Movie: "+ saloons[i].Movie.Name +" "+ saloons[i].Showtimes[Time.MORNING]);
                Console.WriteLine("s" + (i + 1).ToString() + " Movie: " + saloons[i].Movie.Name + " " + saloons[i].Showtimes[Time.NOON]);
                Console.WriteLine("s" + (i + 1).ToString() + " Movie: " + saloons[i].Movie.Name + " " + saloons[i].Showtimes[Time.EVENING]);
            }
        }
    }

    public class UndefinedCommandException : Exception
    {
        public UndefinedCommandException(){}
    }

    public class InvalidParametersException : Exception
    {
        public InvalidParametersException() { }
    }

    public class SeatFullException : Exception
    {
        public SeatFullException() { }
    }
    
    public class SeatAlreadyEmptyException : Exception
    {
        public SeatAlreadyEmptyException() { }
    }
}
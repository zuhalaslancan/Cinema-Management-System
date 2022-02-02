using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE_Cinema
{
    class Showtime : IControlable
    {

        private static int s_ID = 0;

        private int ID;
        private double price;
        private Seat[,] seats;
        private Time time;

        public int Id
        {
            get { return ID; }
            set { ID = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public Seat[,] Seats
        {
            get { return seats; }
            set { seats = value; }
        }
        public Time Time
        {
            get { return time; }
            set { time = value; }
        }

        public Showtime(double price, Time time)
        {
            this.price = price;

            this.seats = new Seat[5,5];

            this.time = time;

            this.ID = s_ID;
            s_ID++;

            for(int i = 0; i < 5; i++)
            {
                string row;

                if (i == 0)
                    row = "A";
                else if (i == 1)
                    row = "B";
                else if (i == 2)
                    row = "C";
                else if (i == 3)
                    row = "D";
                else
                    row = "E";

                for (int j = 0; j < 5; j++)
                {
                    this.seats[i, j] = new Seat(row, (j+1).ToString());
                }
            }
        }
        
        public int getPersonCount()
        {
            int count = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!this.seats[i, j].isEmpty())
                        count++;
                }
            }

            return count;
        }
        
        public bool isEmpty()
        {
            bool result = true;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!seats[i, j].isEmpty())
                    {
                        result = false;
                        break;
                    }
                }

                if (!result)
                    break;
            }

            return result;
        }

        public bool reserve()
        {
            bool success = false;

            if (this.isEmpty())
            {
                success = true;

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (!this.seats[i, j].reserve())
                            success = false;
                    }
                }
            }
            return success;
        }

        public void sellTicket(string seat, Type type)
        {
            if(seat.Length == 2)
            {
                string row = seat[0].ToString();
                string column = seat[1].ToString();

                int row_int = -1;
                int column_int = -1;

                if (row == "A")
                    row_int = 0;
                else if (row == "B")
                    row_int = 1;
                else if (row == "C")
                    row_int = 2;
                else if (row == "D")
                    row_int = 3;
                else if (row == "E")
                    row_int = 4;

                column_int = Convert.ToInt32(column);
                column_int--;

                if (row_int > -1 & row_int < 5 & column_int > -1 & column_int < 5)
                {
                    bool reserved = seats[row_int, column_int].reserve();

                    if (reserved)
                    {
                        seats[row_int, column_int].Type = type;
                        Console.WriteLine("Seat " + seat + " is successfully reserved as "+type+".");
                    }
                    else
                    {
                        throw new SeatFullException();
                    }
                }
                else
                {
                    throw new InvalidParametersException();
                }

            }
            else
            {
                throw new InvalidParametersException();
            }
        }
        
        public void cancelTicket(string seat)
        {
            if (seat.Length == 2)
            {
                string row = seat[0].ToString();
                string column = seat[1].ToString();

                int row_int = -1;
                int column_int = -1;

                if (row == "A")
                    row_int = 0;
                else if (row == "B")
                    row_int = 1;
                else if (row == "C")
                    row_int = 2;
                else if (row == "D")
                    row_int = 3;
                else if (row == "E")
                    row_int = 4;


                column_int = Convert.ToInt32(column);
                column_int--;

                if (row_int > -1 & row_int < 5 & column_int > -1 & column_int < 5)
                {
                    bool reserved = seats[row_int, column_int].isEmpty();

                    if (!reserved)
                    {
                        seats[row_int, column_int].Type = Type.ADULT;
                        seats[row_int, column_int].Status = Status.EMPTY;
                        Console.WriteLine("Seat " + seat + " is successfully cancelled.");
                    }
                    else
                    {
                        throw new SeatAlreadyEmptyException();
                    }
                }
                else
                {
                    throw new InvalidParametersException();
                }

            }
            else
            {
                throw new InvalidParametersException();
            }
        }

        public double getShowtimeRevenue()
        {
            double result = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!seats[i, j].isEmpty())
                    {
                        if (seats[i, j].Type == Type.STUDENT)
                        {
                            result += price / 2;
                        }
                        else
                        {
                            result += price;
                        }
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            string part_1 = "Time: " + this.time + " Price: " + this.price+"\n-------Showtime-------\n";

            string part_2 = "";

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    part_2 += this.seats[i, j].printForStatistics();
                }

                part_2 += "\n";
            }

            return part_1+part_2;
        }
    }
}

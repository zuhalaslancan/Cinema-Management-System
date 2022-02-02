using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE_Cinema
{

    public enum Type { STUDENT, ADULT }
    public enum Status { EMPTY, FULL }
    class Seat : IControlable
    {
        private Type type;
        private Status status;
        private string row;
        private string column;

        public Type Type
        {
            get { return type; }
            set { type = value; }
        }
        public Status Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Row
        {
            get { return row; }
            set { row = value; }
        }
        public string Column
        {
            get { return column; }
            set { column = value; }
        }

        public Seat(string row, string column)
        {
            this.row = row;
            this.column = column;
            this.status = Status.EMPTY;
            this.type = Type.ADULT;
        }

        public string getID()
        {
            return row + column;
        }

        public string printForStatistics()
        {
            if (this.status == Status.FULL)
                return "X";     
            else     
                return "O";     
        }

        public bool isEmpty()
        {
            if (this.status == Status.FULL)
                return false;
            else
                return true;
        }

        public bool reserve()
        {
            bool success = false;

            if (this.status != Status.FULL)
            {
                this.status = Status.FULL;
                success = true;
            }

            return success;
        }
        public override string ToString()
        {
            return "Type: " + this.GetType().Name + "\nID: " + this.getID() + "Status: " + this.status + "Type: " + this.type + "\n";
        }
    }
}

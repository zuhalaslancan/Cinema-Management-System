using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE_Cinema
{
    abstract class Person
    {
        private static int s_ID_d = 1;
        private static int s_ID_s = 1;

        private string name;
        private string birthDay;
        private string birthPlace;
        private int ID;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string BirthDay
        {
            get { return birthDay; }
            set { birthDay = value; }
        }
        public string BirthPlace
        {
            get { return birthPlace; }
            set { birthPlace = value; }
        }
        public int Id
        {
            get { return ID; }
            set { ID = value; }
        }

        public Person(string name, string birthDay, string birthPlace)
        {
            this.name = name;
            this.birthDay = birthDay;
            this.birthPlace = birthPlace;
            if (this.GetType().Name.Equals("Director"))
            {

                this.ID = s_ID_d;
                s_ID_d++;
            }
            else if (this.GetType().Name.Equals("Star"))
            {
                this.ID = s_ID_s;
                s_ID_s++;
            }
            
            
        }


        public virtual string getClass()
        {
            return "Person";
        }

        public override string ToString()
        {
            return "Type: "+this.GetType().Name+"\nID: " + this.ID + "\nName: " + this.name + "\nBirth Day: " + this.birthDay + "\nBirth Place: " + this.birthPlace+"\n";
        }

    }
}

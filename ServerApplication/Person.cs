using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerApplication
{
    public class Person
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string surname;

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        string info;

        public string Info
        {
            get { return info; }
            set { info = value; }
        }
        long lastUpdate;

        public long LastUpdate
        {
            get { return lastUpdate; }
            set { lastUpdate = value; }
        }

        public Person(string _name, string _surname, string _info, long _lastUpdate)
        {
            name = _name;
            surname = _surname;
            info = _info;
            lastUpdate = _lastUpdate;
        }
    }
}
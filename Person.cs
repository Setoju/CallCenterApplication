using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterApplication
{
    public abstract class Person
    {
        protected string _name;        

        public Person(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
